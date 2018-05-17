from requests.auth import HTTPBasicAuth  # or HTTPDigestAuth, or OAuth1, etc.
from zeep import Client
from zeep.transports import Transport

from requests import Session
import MySQLdb
import time
import datetime

import os
from datetime import datetime,timedelta
from dateutil import tz


global db, cursor, lock, session,client,requeteRoom,LocalZone,UtcZone

LocalZone =  tz.tzlocal()
UtcZone = tz.tzutc()


class Room:

	def __init__(self, pIdBdd, pIdWeb):
		self.IdBdd = pIdBdd;
		self.IdWeb = pIdWeb;


	def EnergyReport(self):
		EndDate = datetime.utcnow()
		EndDate=EndDate.replace(minute=0,second=0,microsecond=0)
		StartDate =EndDate + timedelta(hours=-1)
		
		rec = {
		    'locationID': self.IdWeb,
		    'fromDateTime': StartDate,
		    'toDateTime': EndDate,
		    'period': 'Quarter',
		    'metricType':'Energy'
		}

		results = client.service.getMetricReport(rec);

		for metricreport in results['metricReportList']:
		    FromDate=metricreport['fromDateTime'];
		    ToDate=metricreport['toDateTime'];
		    if(FromDate==ToDate):
			continue
		    FromDate = FromDate.replace(tzinfo=UtcZone)
		    FromDateLocal = FromDate.astimezone(LocalZone);
		    avgKW = metricreport['avgKW'];
		    maxKW = metricreport['maxKW']; 
		    minKW= metricreport['minKW'];
		    startAccKWH= metricreport['startAccKWH'];
		    endAccKWH= metricreport['endAccKWH'];
		    AccKWH = endAccKWH-startAccKWH;

		    requeteRoom  ="INSERT INTO LightEnergyQuarter VALUE (NULL,'{0}',{1},{2},{3},{4},{5},{6},{7})"
		    requete = requeteRoom.format(FromDateLocal.strftime('%Y-%m-%d %H:%M:%S'),self.IdBdd, avgKW,minKW,maxKW,startAccKWH,endAccKWH,AccKWH)
		    print(requete)

		    try:
			cur = db.cursor()		
			cur.execute(requete)
			db.commit()
			cur.close()
		    except:
			print("unable to record data {0}".format(requete))









# CODE

session = Session()
session.auth = HTTPBasicAuth('ssegard','C3siN@nterre')


client = Client('https://nrbdfpem.viacesi.fr/daa-webservice/services/soap/daaService?wsdl',transport=Transport(session=session))

db = MySQLdb.connect(host="localhost", user="ssegard", passwd="cesi", db="TEST")


PreviousMinute = -1

RoomTesla=Room(1,'00000000-0000-0001-0000-0000000007D7');
RoomLumiere=Room(2,'00000000-0000-0001-0000-0000000007D8');
RoomNobel=Room(3,'00000000-0000-0001-0000-0000000007D2');
RoomTuring=Room(4,'00000000-0000-0001-0000-0000000007D5');

while(1):
	Now = datetime.now()
	print(Now)
	if(Now.minute==PreviousMinute):
		time.sleep(1)
		continue

	PreviousMinute = Now.minute

	if(PreviousMinute==0):
		RoomTesla.EnergyReport()
		RoomLumiere.EnergyReport();
		RoomNobel.EnergyReport();
		RoomTuring.EnergyReport();

	
