from pyModbusTCP.client import ModbusClient
import struct
import MySQLdb
import time
import datetime
from decimal import *

global _modbusClient,db

def ParseWord (byteval):
    for idx in range(16) :
       print("{0} : {1} ".format(idx,((byteval&(1<<idx))!=0)))
    return

"""
Modbus address	Default value	Function	Description	Value
1		Actual/Setpoint	Outdoor temperature, (Only read)	283
3		Actual/Setpoint	"0=Stopped,1=Starting up,2=Starting reduced speed,3=Starting full speed ,4=Starting normal run,5=Normal run,6=Support control heating,7=Support control cooling,8=CO2 run
											9=Night cooling,10=Full speed stop,,11=Stopping fan,12=Fire mode,13=Recirculation run,14=Defrosting"	5
7		Supply,Extract and Room temperatures	Supply air temperature	281
9		Supply,Extract and Room temperatures	Extract air temp	267
13		SAF/EAF Pressure and Flow	Supply air fan pressure 	962
14		SAF/EAF Pressure and Flow	Extract air fan pressure 	2285
15		SAF/EAF Pressure and Flow	Supply air fan flow. Scale factor = 1	991
16		SAF/EAF Pressure and Flow	Extract air fan flow  Scale factor = 1	1527
122		SAF/EAF Pressure and Flow	Control signal supply air fan 	64
123		SAF/EAF Pressure and Flow	Control signal extract air fan 	55
331		Actual/Setpoint	Total calculated Power consumtion (kW)	39
"""
def GetVmcData():
	regs = _modbusClient.read_input_registers(0, 10)
	if not regs:
		return "";
	print(regs[0])
	T_Ext = float(regs[0])/10	#registre 1-1=0 
	print(T_Ext)
	RunningMode =  regs[2] 	#registre 3-1=2
	T_Reprise = float(regs[8])/10		#registre 9-1=8
	T_Soufflage = float(regs[6])/10	#registre 7-1=6
	
	regs = _modbusClient.read_input_registers(10, 10)
	if not regs:
		return "";
	P_Reprise = float(regs[3])/10		#registre 14-10-1=3
	P_Soufflage = float(regs[2])/10	#registre 13-10-1=2
	V_Reprise = 0
	regs = _modbusClient.read_input_registers(120, 10)
	if not regs:
		return "";
	Fan_Reprise = regs[2]	#registre 123-120-1=2
	Fan_Soufflage = regs[1]	#registre 122-10-1=1

	
	regs = _modbusClient.read_input_registers(150, 10)
	if not regs:
		return "";
	Volume = float(regs[3])	#registre 154-150-1=0
	
	regs = _modbusClient.read_input_registers(330, 10)
	if not regs:
		return "";
	Power = float(regs[0])/10	#registre 331-330-1=0

	Results = ""
	Results+=str(T_Reprise)+","+str(T_Soufflage)+","+str(T_Ext)+","
	Results+=str(P_Reprise)+","+str(P_Soufflage)+","
	Results+=str(Volume)+","
	Results+=str(Fan_Reprise)+","+str(Fan_Soufflage)+","
	Results+=str(Power)+","+str(RunningMode)

	return Results
	
	
def RecordData(TableName, CapteurData):
	requete="INSERT INTO {0} VALUES (NULL,NOW(),{1})".format(TableName,CapteurData)
	print(requete)
	
	try:
		cur = db.cursor()		
		cur.execute(requete)
		db.commit()
		cur.close()
	except:
		print("unable to record data")
	
	return
	
	
db = MySQLdb.connect(host="localhost", user="ssegard", passwd="cesi", db="TEST")

_modbusClient = ModbusClient(host="10.192.16.1", auto_open=True, auto_close=True)

PreviousCapteurData=""
PreviousMinute=-1
PreviousHour=-1



while 1:
	
	Now = datetime.datetime.now()
	
     

	if(Now.minute==PreviousMinute):
		time.sleep(1)
		continue
	
	PreviousMinute = Now.minute
	
	print(Now)
	VmcData = GetVmcData()
	
	if(VmcData!=""):
		RecordData("VmcMinutes",VmcData)
	
	
	if(Now.minute==0 or PreviousHour==-1):
		PreviousHour = Now.hour		
		RecordData("VmcHours",VmcData)

	