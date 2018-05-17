import urllib3
import xmltodict
from requests.auth import HTTPDigestAuth
import json
import os
import subprocess
import re
import threading
import MySQLdb
import time
import datetime


global db, cursor, lock, http

class InternalCapteur:


    def __init__(self, Ip, TempId,HydroId):
        self.IP = Ip
	self.TempId = TempId
	self.HydroId = HydroId
	self.Validity=0
	self.Temp = 0
	self.Hydro=0
        
    def GetTemp(self):
        return self.Temp

    def GetHydro(self):
	return self.Hydro

	

    def GetData(self):
    	requete = "http://{0}/status.xml".format(self.IP)
    	#print(requete)
    	try:
		file = http.request('GET',requete,timeout=4.0, retries=False)
		data = file.data
	except:
		print("connection failed to {0}".format(self.IP))
		data = 0

	if(data==0):
		return data   
	data = file.data
	
	try:
    		dataxml = xmltodict.parse(data)
	except:
		dataxml=0
	#print(dataxml)
    	return dataxml

    def GetSensorData(self,Data, sensorId):
    	noeud="s{0}".format(sensorId)
    	return Data['r'][noeud]


    def Work(self):
	data=self.GetData()
	if(data==0):
		self.validity=0
		return "{0},{1},{2}".format(self.Temp, self.Hydro, self.validity)

	Temp=self.Temp
	Hydro=self.Hydro
	try:
		Temp = self.GetSensorData(data,self.TempId).split(' ')[0]
		Hydro = self.GetSensorData(data,self.HydroId).split(' ')[0]
		self.Temp= Temp
		self.Hydro = Hydro
		self.validity=1
	except:
		self.validity=0
	
	return "{0},{1},{2}".format(self.Temp, self.Hydro, self.validity)



class MeteoData:
	


	def __init__(self):
		self.url="https://www.prevision-meteo.ch/services/json/nanterre"
		self.Temp=0
		self.Hydro=0
		self.Validity=0


	def ComputeMeteoData(self):
	

		try:
			file = http.request('GET',self.url,timeout=10.0, retries=False)
			data = file.data
			#print(data)
			jData = json.loads(data)


			CurrentMeteo = jData['current_condition']
			print CurrentMeteo
			TempExt = CurrentMeteo['tmp']
			HumExt =  CurrentMeteo['humidity']
			self.Temp=TempExt
			self.Hydro=HumExt
			self.Validity=1
		except:
			print("Unable to retrieve meteo data")
			Validity=0

			return

	def Work(self):
		return "{0},{1},{2}".format(self.Temp, self.Hydro, self.Validity)




http = urllib3.PoolManager()

db = MySQLdb.connect(host="localhost", user="ssegard", passwd="cesi", db="TEST")

Capteur1=InternalCapteur("10.192.16.21",62234,2971)#lumiere
Capteur2=InternalCapteur("10.192.16.22",2237,2975)#Turing
Capteur3=InternalCapteur("10.192.16.23",11143,2987)#Nobel
Capteur4=InternalCapteur("10.192.16.24",54085,2954)#Tesla
Meteo = MeteoData()
PreviousMinute=-1;
PreviousHour=-1;

Meteo.ComputeMeteoData()

while(1):
	Now = datetime.datetime.now()
	print(Now)
	if(Now.minute==PreviousMinute):
		time.sleep(1)
		continue

	

	PreviousMinute=Now.minute
	#Capteur1.Work()
	#Capteur2.Work()
	#Capteur3.Work()
	#Capteur4.Work()
	

	if(Now.minute==0):
		Meteo.ComputeMeteoData()



	valueToRecord ="{0},{1},{2},{3},{4}".format(Capteur1.Work(),Capteur2.Work(),Capteur3.Work(),Capteur4.Work(),Meteo.Work())

	requete ="INSERT INTO Temp2 VALUE (NULL,NOW(),{0})".format(valueToRecord)
	print requete

	try:
		cur = db.cursor()		
		cur.execute(requete)
		db.commit()
		cur.close()
	except:
		print("unable to record data")

	if(Now.minute==0 or PreviousHour==-1):
		PreviousHour = Now.hour		

		requete ="INSERT INTO TempHour VALUE (NULL,NOW(),{0})".format(valueToRecord)
		print requete
		try:
			cur = db.cursor()		
			cur.execute(requete)
			db.commit()
			cur.close()
		except:
			print("unable to record data")


