
import SmartBuildingBacnet
import MyBacnetPropertyReader
import time
import MySQLdb
import datetime

#init communication
MyBacnetPropertyReader.Init("10.192.16.40")


#init Smart building objects
BacNet = SmartBuildingBacnet.MySmartBuildingBacnet()
Points=BacNet.GetPointListToRead()

#connect to db
db = MySQLdb.connect(host="localhost", user="ssegard", passwd="cesi", db="TEST")
PreviousMinute=-1;
PreviousHour=-1;

while 1:
	Now = datetime.datetime.now()
	
	if(Now.minute==PreviousMinute):
		time.sleep(1)
		continue
	
	PreviousMinute=Now.minute
	
	if(Now.minute==0 or PreviousHour==-1):
		PreviousHour = Now.hour	
	else:
		continue

	print(Now)

	try :
		#read values in bacnet
		results = MyBacnetPropertyReader.ReadValues(Points);
	except:
		print("unable to read bacnet data")
		continue

	#update results in my bacnet
	BacNet.UpdateValues(results)

	for room in BacNet.Rooms:
	
		requete ="INSERT INTO BACNET_1 VALUE (NULL,NOW(),{0},'{1}','{2}','{3}','{4}','{5}',{6},{7})".format(
			room.IdRoom,
			room.OnOffSetup.GetValue(),
			room.OnOffState.GetValue(),
			room.ErrorCode.GetValue(),
			room.OperationalModeSetup.GetValue(),
			room.OperationalModeState.GetValue(),
			room.RoomTemp.GetValue(),
			room.SetTemp.GetValue())
		print requete

		try:
			cur = db.cursor()		
			cur.execute(requete)
			db.commit()
			cur.close()
		except:
			print("unable to record data")


