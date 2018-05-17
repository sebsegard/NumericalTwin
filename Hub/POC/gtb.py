from pyModbusTCP.client import ModbusClient
import struct
import MySQLdb
import time
import datetime


global _modbusClient,db

def ParseWord (byteval):
    for idx in range(16) :
       print("{0} : {1} ".format(idx,((byteval&(1<<idx))!=0)))
    return



def getCapteurFenetreData():
	s=""
	regs = _modbusClient.read_holding_registers(256, 2)
	if regs:
		#print(regs[0])
		for i in range(16):
			if(i !=0):			
				s+=","
			registre=regs[0]		
			CapteurData = (registre&(1<<i))!=0
			if(CapteurData):
				s+="1"
			else:
				s+="0"
		for i in range(2):	
			s+=","
			registre=regs[1]		
			CapteurData = (registre&(1<<i))!=0
			if(CapteurData):
				s+="1"
			else:
				s+="0"
		#print s

	else:
	    print("Could not get capteur data")
	return s



def GetMeteoData():
	regs = _modbusClient.read_holding_registers(12288, 11)
	s=""	
	if regs:
		for i in range(10):
			if(i!=0):
				s+=","
			s+=str(regs[i])
	else:
	 	print("Could not get meteo data")
		return ""

	regs = _modbusClient.read_holding_registers(12298, 2)
	if regs:
		s+=","	    	
		b = struct.pack('2H', *regs)
	    	f=struct.unpack("f",b)[0]
		s+=str(f)
	else:
		print("Could not get meteo data")
		return ""
	return s


def RecordData(TableName, CapteurData):
	requete="INSERT INTO {0} VALUES (NULL,NOW(),{1},1)".format(TableName,CapteurData)
	print requete
	try:
		cur = db.cursor()		
		cur.execute(requete)
		db.commit()
		cur.close()
	except:
		print("unable to record data")
	return




#db = MySQLdb.connect(host="localhost", user="ssegard", passwd="cesi", db="TEST")

_modbusClient = ModbusClient(host="10.192.16.1", auto_open=True, auto_close=True)
regs = _modbusClient.read_holding_registers(101, 21)
print(regs)

for i in range (0,1000):
	regs2 = _modbusClient.read_coils(i*10, 10)
	#print(i*10,"")		
	#print(regs)
	#print(regs2)

	try:
		for j in range(0,10): 	
			print(i*10+j,",cois status,",regs2[j])	
	except:
		print(i*10,",")	


print("")
print("")

print("")
print("")
for i in range (0,1000):
	regs2 = _modbusClient.read_discrete_inputs(i*10, 10)
	#print(i*10,"")		
	#print(regs)
	#print(regs2)

	try:
		for j in range(0,10): 	
			print(i*10+j,",input status,",regs2[j])	
	except:
		print(i*10,",")	


print("")
print("")

for i in range (0,1000):
	regs = _modbusClient.read_holding_registers(i*10, 10)
	
	#print(i*10,"")		
	#print(regs)
	#print(regs2)

	try:
		for j in range(0,10): 	
			print(i*10+j,",holding,",regs[j])	
	except:
		print(i*10,",")	
	#print("")
print("")
print("")
for i in range (0,1000):
	regs2 = _modbusClient.read_input_registers(i*10, 10)
	#print(i*10,"")		
	#print(regs)
	#print(regs2)

	try:
		for j in range(0,10): 	
			print(i*10+j,",input,",regs2[j])	
	except:
		print(i*10,",")	

exit()

PreviousCapteurData=""
PreviousMinute=-1
PreviousHour=-1


