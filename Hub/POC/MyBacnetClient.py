
import SmartBuildingBacnet
import MyBacnetPropertyRW
import time
import MySQLdb
import datetime

#init communication
MyBacnetPropertyRW.Init("10.192.16.40")


#init Smart building objects
BacNet = SmartBuildingBacnet.MySmartBuildingBacnet()
PointsToRead=BacNet.GetPointListToRead()

#connect to db
db = MySQLdb.connect(host="localhost", user="ssegard", passwd="cesi", db="TEST")
PreviousMinute=-1;
PreviousHour=-1;

#exit

#Points = BacNet.GetSetPointsBuildingValues(20.0,"Run","Cool","Prohibit","High")
#Points = BacNet.GetSetPointsRoomValues("Nobel",22.0,"Run","Cool","Permit","Permit","Permit","High")
#MyBacnetPropertyRW.WriteValues(Points)
	


LastRecordedSetpoint =""
while 1:
	Now = datetime.datetime.now()
	
	##here check if  new setpoints
	#Temperature, OnOffState,Mode, Prohibition, FanSpeedSetup
	requete = "SELECT * FROM BacnetConsigne ORDER BY TimeStamp DESC LIMIT 1"
	row = ""
	#try:
	if  1:
		cur = db.cursor()		
		cur.execute(requete)
		
      		row=cur.fetchone()

		
		db.commit()
		cur.close()
	#except:
		#print("unable to read setpoint data")
		
	
	if row!="" :
		if row[1] != LastRecordedSetpoint:
			LastRecordedSetpoint = row[1]
			Room = row[2]
			RoomStr=""
			if(Room == 1):
				RoomStr = "TESLA"
			elif(Room == 2):
				RoomStr = "LUMIERE"
			elif(Room == 3):
				RoomStr = "NOBEL"
			elif(Room == 4):
				RoomStr = "TURING"
			elif(Room == 6):
				RoomStr = "NOBEL2"
			elif(Room == 7):
				RoomStr = "METRO"
			elif(Room == 8):
				RoomStr = "LOCAL"	
			PointsToWrite = []
			if(RoomStr !=""):
				PointsToWrite = BacNet.GetSetPointsRoomValues(RoomStr,row[7], row[3],row[4],row[6],row[6],row[6],row[5])
			else:				
				PointsToWrite = BacNet.GetSetPointsBuildingValues(row[7], row[3],row[4],row[6],row[6],row[6],row[5])
			MyBacnetPropertyRW.WriteValues(PointsToWrite)
	
	
	
	
	
	
	
	
	
	if(Now.minute==PreviousMinute):
		time.sleep(1)
		continue
	
	PreviousMinute=Now.minute

	#if(Now.minute==0 or PreviousHour==-1):
	#	PreviousHour = Now.hour	
	#else:
	#	continue

	print(Now)

	try :
		#read values in bacnet
		results = MyBacnetPropertyRW.ReadValues(PointsToRead);
	except:
		print("unable to read bacnet data")
		continue

	#update results in my bacnet
	BacNet.UpdateValues(results)

	for room in BacNet.Rooms:

		requete ="INSERT INTO BacnetValues VALUE (NULL,NOW(),{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})".format(
			room.IdRoom,
			room.OnOffSetup.GetNumericalValue(),
			room.OnOffState.GetNumericalValue(),
			room.ErrorCode.GetNumericalValue(),
			room.OperationalModeSetup.GetNumericalValue(),
			room.OperationalModeState.GetNumericalValue(),
			room.RoomTemp.GetValue(),
			room.SetTemp.GetValue(),
			room.ProhibitionOnOff.GetNumericalValue(),
			room.ProhibitionMode.GetNumericalValue(),
			room.ProhibitionTemp.GetNumericalValue(),
			room.FanSpeedState.GetNumericalValue(),
			room.FanSpeedSetup.GetNumericalValue())
		print(requete)

		try:
			cur = db.cursor()		
			cur.execute(requete)
			db.commit()
			cur.close()
		except:
			print("unable to record data")

