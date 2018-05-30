#!/usr/bin/env python

"""
Threaded Read Property

This application has a static list of points that it would like to read.  It
starts a thread for each unique device address and reads the points for that
device.
"""

from threading import Thread

from bacpypes.debugging import bacpypes_debugging, ModuleLogger
from bacpypes.consolelogging import ConfigArgumentParser

from bacpypes.core import run, stop, deferred
from bacpypes.iocb import IOCB

from bacpypes.pdu import Address
from bacpypes.object import get_datatype

from bacpypes.apdu import ReadPropertyRequest
from bacpypes.primitivedata import Unsigned
from bacpypes.constructeddata import Array

from bacpypes.app import BIPSimpleApplication
from bacpypes.service.device import LocalDeviceObject
from bacpypes.apdu import SimpleAckPDU, \
    ReadPropertyRequest, ReadPropertyACK, WritePropertyRequest
from bacpypes.primitivedata import Null, Atomic, Boolean, Unsigned, Integer, \
    Real, Double, OctetString, CharacterString, BitString, Date, Time
from bacpypes.constructeddata import Array, Any, AnyAtomic
# some debugging
_debug = 0
_log = ModuleLogger(globals())

# globals
this_application = None

ModeOnOff = "active" #active/inactive
ModeChaleur = 1 #1-cool 2-heat, 
Temperature = 20
Prohibition = "active" #active/inactive


# point list, set according to your devices
point_list = [
	('10.192.16.40', [
		('binaryOutput', 1,ModeOnOff),
		('multiStateOutput', 5, ModeChaleur),
		('binaryValue', 13, Prohibition),
		('binaryValue',14, Prohibition),
		('binaryValue', 15, Prohibition),
		('binaryValue', 16,Prohibition),
		('analogValue', 24,Temperature),
		('analogValue', 25,Temperature),
		]),
	]

#
#   ReadPointListThread
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
		for RoomId in range(1,8):
			for obj_type, obj_inst, value in self.point_list:
				
				obj_inst = 10000+RoomId*100+obj_inst
				
				datatype = get_datatype(obj_type, prop_id)
				if not datatype:
					raise ValueError("invalid property for object type")
					
				# build a request
				 # change atomic values into something encodeable, null is a special case
				if (value == 'null'):
					value = Null()
				elif issubclass(datatype, AnyAtomic):
					dtype, dvalue = value.split(':')
					if _debug: ReadWritePropertyConsoleCmd._debug("    - dtype, dvalue: %r, %r", dtype, dvalue)

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
					if _debug: ReadWritePropertyConsoleCmd._debug("    - datatype: %r", datatype)

					value = datatype(dvalue)
					if _debug: ReadWritePropertyConsoleCmd._debug("    - value: %r", value)

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
				if _debug: ReadWritePropertyConsoleCmd._debug("    - encodeable value: %r %s", value, type(value))

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
				

				if _debug: ReadWritePropertyConsoleCmd._debug("    - request: %r", request)

				# make an IOCB
				iocb = IOCB(request)
				if _debug: ReadWritePropertyConsoleCmd._debug("    - iocb: %r", iocb)

				# give it to the application
				this_application.request_io(iocb)

				# wait for it to complete
				iocb.wait()

				# do something for success
				if iocb.ioResponse:
					# should be an ack
					if not isinstance(iocb.ioResponse, SimpleAckPDU):
						if _debug: ReadWritePropertyConsoleCmd._debug("    - not an ack")
						return

					print(obj_inst,"ack\n")

				# do something for error/reject/abort
				if iocb.ioError:
					print(obj_inst,str(iocb.ioError))

			if _debug: ReadPointListThread._debug("    - fini")


#
#   ThreadSupervisor
#

@bacpypes_debugging
class ThreadSupervisor(Thread):

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

#
#   __main__
#

def main():
	global this_application

	# parse the command line arguments
	args = ConfigArgumentParser(description=__doc__).parse_args()

	if _debug: _log.debug("initialization")
	if _debug: _log.debug("    - args: %r", args)

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
	if _debug: _log.debug("    - services_supported: %r", services_supported)

	# let the device object know
	this_device.protocolServicesSupported = services_supported.value

	thread_list = []

	# loop through the address and point lists
	for addr, points in point_list:
		# create a thread
		read_thread = WritePointListThread(addr, points)
		if _debug: _log.debug("    - read_thread: %r", read_thread)
		thread_list.append(read_thread)

	# create a thread supervisor
	thread_supervisor = ThreadSupervisor(thread_list)

	# start it running when the core is running
	deferred(thread_supervisor.start)

	_log.debug("running")

	run()

	# dump out the results
	for read_thread in thread_list:
		for request, response in zip(read_thread.point_list, read_thread.response_values):
			print(request, response)

	_log.debug("fini")


if __name__ == "__main__":
	main()

