import urllib3
import xmltodict

import os
import subprocess
import re
import threading
import MySQLdb
import time

global db, cursor, lock

def GetData(http, ip):
    	requete = "http://{0}/status.xml".format(ip)
    	#print(requete)
    	try:
		file = http.request('GET',requete,timeout=4.0, retries=False)
		data = file.data
	except:
		print("connection failed to {0}".format(ip))
		data = 0

	if(data==0):
		return data

	#if(file.    
	data = file.data
	
    	#print(data)
    	dataxml = xmltodict.parse(data)
    	#print(dataxml)
    	return dataxml


def GetSensorData(Data, sensorId):
    noeud="s{0}".format(sensorId)
    return Data['r'][noeud]


def RecordSensorData(CapteurId,CapteurIp, TempId,HydroId):
	http = urllib3.PoolManager()
	Temp = 0
	Hydro=0	
	while 1:

		data=GetData(http,CapteurIp)
		validity=0;
		if(data!=0):
			Temp = GetSensorData(data,TempId).split(' ')[0]
			Hydro = GetSensorData(data,HydroId).split(' ')[0]
			validity=1
		#print(Temp)
		#print(Hydro)
		requete ="INSERT INTO Temperatures VALUE (NULL,{0},NOW(),{1},{2},{3})".format(CapteurId,Temp,Hydro,validity)
		
	
		print(requete)
		lock.acquire()
		try:
			cur = db.cursor()		
			cur.execute(requete)
			db.commit()
			cur.close()
		finally:
			lock.release()
		time.sleep(60)
	return

def ThreadRecordSensorData(CapteurId,CapteurIp, TempId,HydroId):
	threading.Thread(target=RecordSensorData,args=(CapteurId,CapteurIp,TempId,HydroId)).start()


print("Bonjour :)")


db = MySQLdb.connect(host="localhost", user="ssegard", passwd="cesi", db="TEST")


lock = threading.Lock()

#turing
ThreadRecordSensorData(2,"10.192.16.22",2237,2975)
#NOBEL
ThreadRecordSensorData(3,"10.192.16.23",11143,2987)
#LUMIERE
ThreadRecordSensorData(1,"10.192.16.21",62234,2971)
#TESLA
ThreadRecordSensorData(4,"10.192.16.24",54085,2954)


cursor.close()
db.close()
