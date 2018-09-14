from threading import Thread

from bacpypes.debugging import bacpypes_debugging, ModuleLogger
from bacpypes.consolelogging import ConfigArgumentParser

from bacpypes.core import run, stop, deferred
from bacpypes.iocb import IOCB

from bacpypes.pdu import Address
from bacpypes.object import get_datatype

from bacpypes.apdu import ReadPropertyRequest, WritePropertyRequest,SimpleAckPDU
from bacpypes.primitivedata import Unsigned
from bacpypes.constructeddata import Array

from bacpypes.app import BIPSimpleApplication
from bacpypes.service.device import LocalDeviceObject

from bacpypes.primitivedata import Null, Atomic, Boolean, Unsigned, Integer, \
    Real, Double, OctetString, CharacterString, BitString, Date, Time
from bacpypes.constructeddata import Array, Any, AnyAtomic

# some debugging
_debug = 0
_log = ModuleLogger(globals())

# globals
this_application = None
this_ServerAdress = None

#
#	ReadPointListThread
#

@bacpypes_debugging
class ReadPointListThread(Thread):

	def __init__(self, device_address, point_list):
		if _debug: ReadPointListThread._debug("__init__ %r %r", device_address, point_list)
		Thread.__init__(self)

		# save the address
		self.device_address = Address(device_address)

		# turn the point list into a queue
		self.point_list = point_list

		# make a list of the response values
		self.response_values = []

	def run(self):
		if _debug: ReadPointListThread._debug("run")
		global this_application

		# loop through the points
		for obj_type, obj_inst, prop_id in self.point_list:
			# build a request
			request = ReadPropertyRequest(
				destination=self.device_address,
				objectIdentifier=(obj_type, obj_inst),
				propertyIdentifier=prop_id,
				)
			print("request: ", request)	
			if _debug: ReadPointListThread._debug("	   - request: %r", request)

			# make an IOCB
			iocb = IOCB(request)
			if _debug: ReadPointListThread._debug("	   - iocb: %r", iocb)

			# give it to the application
			this_application.request_io(iocb)

			# wait for the response
			iocb.wait()

			if iocb.ioResponse:
				apdu = iocb.ioResponse

				# find the datatype
				datatype = get_datatype(apdu.objectIdentifier[0], apdu.propertyIdentifier)
				if _debug: ReadPointListThread._debug("	   - datatype: %r", datatype)
				if not datatype:
					raise TypeError("unknown datatype")

				# special case for array parts, others are managed by cast_out
				if issubclass(datatype, Array) and (apdu.propertyArrayIndex is not None):
					if apdu.propertyArrayIndex == 0:
						value = apdu.propertyValue.cast_out(Unsigned)
					else:
						value = apdu.propertyValue.cast_out(datatype.subtype)
				else:
					value = apdu.propertyValue.cast_out(datatype)
				if _debug: ReadPointListThread._debug("	   - value: %r", value)

				# save the value
				self.response_values.append(value)

			if iocb.ioError:
				if _debug: ReadPointListThread._debug("	   - error: %r", iocb.ioError)
				self.response_values.append(iocb.ioError)

		if _debug: ReadPointListThread._debug("	   - fini")


#
#	ThreadSupervisor
#


@bacpypes_debugging
class WritePointListThread(Thread):

	def __init__(self, device_address, point_list):
		if _debug: WritePointListThread._debug("__init__ %r %r", device_address, point_list)
		Thread.__init__(self)

		# save the address
		self.device_address = Address(device_address)

		# turn the point list into a queue
		self.point_list = point_list

		# make a list of the response values
		self.response_values = []

	def run(self):
		if _debug: WritePointListThread._debug("run")
		global this_application
		prop_id = "presentValue"
		# loop through the points

		print("points in thread :", self.point_list)
		for obj_type, obj_inst, value in self.point_list:
			
			datatype = get_datatype(obj_type, prop_id)
			if not datatype:
				raise ValueError("invalid property for object type")
				
			# build a request
			 # change atomic values into something encodeable, null is a special case
			if (value == 'null'):
				value = Null()
			elif issubclass(datatype, AnyAtomic):
				dtype, dvalue = value.split(':')
				if _debug: ReadWritePropertyConsoleCmd._debug("	   - dtype, dvalue: %r, %r", dtype, dvalue)

				datatype = {
					'b': Boolean,
					'u': lambda x: Unsigned(int(x)),
					'i': lambda x: Integer(int(x)),
					'r': lambda x: Real(float(x)),
					'd': lambda x: Double(float(x)),
					'o': OctetString,
					'c': CharacterString,
					'bs': BitString,
					'date': Date,
					'time': Time,
					}[dtype]
				if _debug: ReadWritePropertyConsoleCmd._debug("	   - datatype: %r", datatype)

				value = datatype(dvalue)
				if _debug: ReadWritePropertyConsoleCmd._debug("	   - value: %r", value)

			elif issubclass(datatype, Atomic):
				if datatype is Integer:
					value = int(value)
				elif datatype is Real:
					value = float(value)
				elif datatype is Unsigned:
					value = int(value)
				value = datatype(value)
			elif issubclass(datatype, Array) and (indx is not None):
				if indx == 0:
					value = Integer(value)
				elif issubclass(datatype.subtype, Atomic):
					value = datatype.subtype(value)
				elif not isinstance(value, datatype.subtype):
					raise TypeError("invalid result datatype, expecting %s" % (datatype.subtype.__name__,))
			elif not isinstance(value, datatype):
				raise TypeError("invalid result datatype, expecting %s" % (datatype.__name__,))
			if _debug: ReadWritePropertyConsoleCmd._debug("	   - encodeable value: %r %s", value, type(value))

			# build a request
			request = WritePropertyRequest(
				destination=self.device_address,
				objectIdentifier=(obj_type, obj_inst),
				propertyIdentifier=prop_id
				)
			#request.pduDestination = Address(addr)

			# save the value
			request.propertyValue = Any()
			try:
				request.propertyValue.cast_in(value)
			except Exception as error:
				ReadWritePropertyConsoleCmd._exception("WriteProperty cast error: %r", error)

			# optional array index
			

			if _debug: ReadWritePropertyConsoleCmd._debug("	   - request: %r", request)

			# make an IOCB
			iocb = IOCB(request)
			if _debug: ReadWritePropertyConsoleCmd._debug("	   - iocb: %r", iocb)

			# give it to the application
			this_application.request_io(iocb)

			# wait for it to complete
			iocb.wait()

			# do something for success
			if iocb.ioResponse:
				# should be an ack
				if not isinstance(iocb.ioResponse, SimpleAckPDU):
					if _debug: ReadWritePropertyConsoleCmd._debug("	   - not an ack")
					return

				print(obj_inst,"ack\n")

			# do something for error/reject/abort
			if iocb.ioError:
				print(obj_inst,str(iocb.ioError))

			if _debug: ReadPointListThread._debug("	   - fini")



@bacpypes_debugging
class ReadWriteThreadSupervisor(Thread):

	def __init__(self, thread_list):
		if _debug: ThreadSupervisor._debug("__init__ ...")
		Thread.__init__(self)

		self.thread_list = thread_list

	def run(self):
		if _debug: ThreadSupervisor._debug("run")

		# start them up
		for read_thread in self.thread_list:
			read_thread.start()
		if _debug: ThreadSupervisor._debug("    - all started")

		# wait for them to finish
		for read_thread in self.thread_list:
			read_thread.join()
		if _debug: ThreadSupervisor._debug("    - all finished")

		# stop the core
		stop()
		
def Init(pAddress):
	global this_application, this_ServerAdress
	
	this_ServerAdress = pAddress
	# parse the command line arguments
	args = ConfigArgumentParser(description=__doc__).parse_args()

	if _debug: _log.debug("initialization")
	if _debug: _log.debug("	   - args: %r", args)

	# make a device object
	this_device = LocalDeviceObject(
		objectName=args.ini.objectname,
		objectIdentifier=int(args.ini.objectidentifier),
		maxApduLengthAccepted=int(args.ini.maxapdulengthaccepted),
		segmentationSupported=args.ini.segmentationsupported,
		vendorIdentifier=int(args.ini.vendoridentifier),
		)

	# make a simple application
	this_application = BIPSimpleApplication(this_device, args.ini.address)

	# get the services supported
	services_supported = this_application.get_services_supported()
	if _debug: _log.debug("	   - services_supported: %r", services_supported)

	# let the device object know
	this_device.protocolServicesSupported = services_supported.value
	

def ReadValues(PointList):
	global this_application, this_ServerAdress
	# create a thread
	read_thread = ReadPointListThread(this_ServerAdress, PointList)
	thread_list = []
	thread_list.append(read_thread)
	# create a thread supervisor
	thread_supervisor = ReadWriteThreadSupervisor(thread_list)

	# start it running when the core is running
	deferred(thread_supervisor.start)

	_log.debug("running")

	run()

	GlobalResponse = {}
	# return out the results
	for request, response in zip(read_thread.point_list, read_thread.response_values):
		obj_type, obj_inst, prop_id = request
		GlobalResponse[obj_inst] = response
	
	return GlobalResponse

def WriteValues(PointList):
	global this_application, this_ServerAdress
	# create a thread
	thread_list = []

	print(PointList)
	# loop through the address and point lists
	#for points in PointList:
		# create a thread
		#print(points)
	read_thread = WritePointListThread(this_ServerAdress, PointList)
	if _debug: _log.debug("	   - read_thread: %r", read_thread)
	thread_list.append(read_thread)

	# create a thread supervisor
	thread_supervisor = ReadWriteThreadSupervisor(thread_list)

	# start it running when the core is running
	deferred(thread_supervisor.start)

	_log.debug("running")

	run()

	# dump out the results
	for read_thread in thread_list:
		for request, response in zip(read_thread.point_list, read_thread.response_values):
			print(request, response)
	# start it running when the core is running

	
	
