
#import requests
#from requests.auth import HTTPDigestAuth




# Replace with the correct URL
#url = "https://nrbdfpem.viacesi.fr/daa-webservice/services/soap/daaService"



#response = requests.post(url,data=body,headers=headers)

# It is a good practice not to hardcode the credentials. So ask the user to enter credentials at runtime
#myResponse = requests.post(url,data=body,headers=headers,auth=('ssegard','C3siN@nterre'), verify=True)
from requests.auth import HTTPBasicAuth  # or HTTPDigestAuth, or OAuth1, etc.
from zeep import Client
from zeep.transports import Transport

from requests import Session
from dateutil import tz
import os
from datetime import time,datetime,timedelta


os.system('cls')
LocalZone =  tz.tzlocal()
session = Session()
session.auth = HTTPBasicAuth('ssegard','C3siN@nterre')


client = Client('https://nrbdfpem.viacesi.fr/daa-webservice/services/soap/daaService?wsdl',transport=Transport(session=session))

#result = client.service.getSupportedMetricTypes();

#print(result)

#ALL BUILDING
#00000000-0000-0002-0000-000000000000
result = client.service.getBuildingNavigation("Building");
print(result)
#w3date=mydate.strftime("%Y-%m-%dT%H:%M:%S%z")
#StartDate = '2017-12-19T16:00:00'
#EndDate = '2017-12-19T16:30:00'



def GetOccupancyStat(IdRoom, RoomName):
    EndDate = datetime.utcnow()
    EndDate=EndDate.replace(minute = 0,second=0,microsecond=0)
    StartDate =EndDate + timedelta(hours=-1)
    
    
    print("")
    print("***********************************************************************")
    print("")
    print("Occupancy report - minute")
    print(StartDate.strftime('%Y-%m-%d %H:%M:%S'))
    print(EndDate.strftime('%Y-%m-%d %H:%M:%S'))

    rec = {
        'locationID': IdRoom,
        'fromDateTime': StartDate,
        'toDateTime': EndDate,
        'period': 'Hour',
        'metricType':'Occupancy'
    }



    #print(rec);
    results = client.service.getMetricReport(rec);
    print(results);
    print("\n");
    print("Stat for Room",RoomName,"Id",IdRoom)
    



    for metricreport in results['metricReportList'] :
        #print(metricreport)
      
        reporttime = metricreport['fromDate']
        occupiedPercentage =     metricreport['occupiedPercentage']
        unOccupiedPercentage =     metricreport['unOccupiedPercentage']
        unknownPercentage = metricreport['unknownPercentage']

        print("occupiedPercentage :",occupiedPercentage,"unOccupiedPercentage :",unOccupiedPercentage,"unknownPercentage :",unknownPercentage) 
        IdSensor=0
        for sensor in metricreport['childOccupancyList']:
            #    sensorId = sensor['sensorId']
            IdSensor+=1
            occupation=sensor['occupiedPercentage']
        
            line ="{0}:{1}".format(IdSensor,occupation)
            print(line)


def GetOccupancyDetailled(IdRoom, NbMinutes, RoomName):
    EndDate = datetime.utcnow()
    EndDate=EndDate.replace(second=0,microsecond=0)
    StartDate =EndDate + timedelta(minutes=-NbMinutes)
    StartDate=StartDate.replace(second=0,microsecond=0)
    
    print("")
    print("***********************************************************************")
    print("")
    print("Occupancy report - minute")
    print(StartDate.strftime('%Y-%m-%d %H:%M:%S'))
    print(EndDate.strftime('%Y-%m-%d %H:%M:%S'))

    rec = {
        'locationID': IdRoom,
        'fromDateTime': StartDate,
        'toDateTime': EndDate,
        'period': 'Minute',
        'metricType':'Occupancy'
    }



    #print(rec);
    results = client.service.getMetricReport(rec);
    #print(results);
    print("\n");
    print("Stat for Room",RoomName,"Id",IdRoom)
    



    for metricreport in results['metricReportList'] :
        #print(metricreport)
      
        reporttime = metricreport['fromDate']
        occupiedPercentage =     metricreport['occupiedPercentage']
        unOccupiedPercentage =     metricreport['unOccupiedPercentage']
        unknownPercentage = metricreport['unknownPercentage']

        print("occupiedPercentage :",occupiedPercentage,"unOccupiedPercentage :",unOccupiedPercentage,"unknownPercentage :",unknownPercentage) 
        IdSensor=0
        for sensor in metricreport['childOccupancyList']:
            #    sensorId = sensor['sensorId']
            IdSensor+=1
            occupation=sensor['occupiedPercentage']
        
            line ="{0}:{1}".format(IdSensor,occupation)
            print(line)



#GetOccupancyStat('00000000-0000-0001-0000-0000000007D7',"TESLA")
#GetOccupancyStat('00000000-0000-0001-0000-0000000007D2',"NOBEL")
#GetOccupancyStat('00000000-0000-0001-0000-0000000007D5',"TURING")
#GetOccupancyStat('00000000-0000-0001-0000-0000000007D8',"LUMIERE")


GetOccupancyDetailled('00000000-0000-0001-0000-0000000007D7', 10, 'TESLA')
GetOccupancyDetailled('00000000-0000-0001-0000-0000000007D2', 10, 'NOBEL')

exit()
EndDate = datetime.utcnow()
StartDate =EndDate + timedelta(minutes=-3)
StartDate=StartDate.replace(second=0,microsecond=0)
EndDate=EndDate.replace(second=0,microsecond=0)
print("")
print("***********************************************************************")
print("")
print("Occupancy report - minute")
print(StartDate.strftime('%Y-%m-%d %H:%M:%S'))
print(EndDate.strftime('%Y-%m-%d %H:%M:%S'))

rec = {
    'locationID': '00000000-0000-0002-0000-000000000000',
    'fromDateTime': StartDate,
    'toDateTime': EndDate,
    'period': 'Quarter',
    'metricType':'Energy'
}



print(rec);
results = client.service.getMetricReport(rec);
print(results);




for metricreport in results['metricReportList'] :
    #print(metricreport)
  
    reporttime = metricreport['fromDate']
    Global =     metricreport['occupiedPercentage']
        #lineglobal="{0};{1};{2}\n".format(reporttime,Room,Global)
        #fichierGlobal.write(lineglobal)
    IdSensor=0
    for sensor in metricreport['childOccupancyList']:
        #    sensorId = sensor['sensorId']
        IdSensor+=1
        occupation=sensor['occupiedPercentage']
    
        line ="{0}:{1}".format(IdSensor,occupation)
        print(line)










exit()

EndDate = datetime.utcnow()
StartDate =EndDate + timedelta(hours=-1)
StartDate=StartDate.replace(second=0,microsecond=0)
EndDate=EndDate.replace(second=0,microsecond=0)
print("")
print("***********************************************************************")
print("")
print("Occupancy report - hour")
print(StartDate.strftime('%Y-%m-%d %H:%M:%S'))
print(EndDate.strftime('%Y-%m-%d %H:%M:%S'))

rec = {
    'locationID': '00000000-0000-0001-0000-0000000007D2',
    'fromDateTime': StartDate,
    'toDateTime': EndDate,
    'period': 'Hour',
    'metricType':'Occupancy'
}



print(rec);
results = client.service.getMetricReport(rec);
print(results);

EndDate = datetime.utcnow()
StartDate =EndDate + timedelta(hours=-1)
StartDate=StartDate.replace(second=0,microsecond=0)
EndDate=EndDate.replace(second=0,microsecond=0)
print("")
print("***********************************************************************")
print("")
print("Occupancy report : Latest Area")
print(StartDate.strftime('%Y-%m-%d %H:%M:%S'))
print(EndDate.strftime('%Y-%m-%d %H:%M:%S'))

rec = {
    'locationID': 'FFCD13BD-AFAD-46AD-9FE6-17904FC91D41',

    'metricType':'Occupancy'
}



print(rec);
results = client.service.getMetricReport(rec);
print(results);

EndDate = datetime.utcnow()
StartDate =EndDate + timedelta(hours=-1)
StartDate=StartDate.replace(second=0,microsecond=0)
EndDate=EndDate.replace(second=0,microsecond=0)
print("")
print("***********************************************************************")
print("")
print("Occupancy report : Latest Room")
print(StartDate.strftime('%Y-%m-%d %H:%M:%S'))
print(EndDate.strftime('%Y-%m-%d %H:%M:%S'))

rec = {
    'locationID': '00000000-0000-0001-0000-0000000007D2',
    'metricType':'Occupancy'
}



print(rec);
results = client.service.getMetricReport(rec);
print(results);




EndDate = datetime.utcnow()
StartDate =EndDate + timedelta(hours=-1)
StartDate=StartDate.replace(second=0,microsecond=0)
EndDate=EndDate.replace(second=0,microsecond=0)
print("")
print("***********************************************************************")
print("")
print("Occupancy report : Latest Sensor")
print(StartDate.strftime('%Y-%m-%d %H:%M:%S'))
print(EndDate.strftime('%Y-%m-%d %H:%M:%S'))

rec = {
    'locationID': 'FFA8DF60-59A5-4B84-9E77-93BF3FC5C43A',
    'metricType':'Occupancy'
}



#print(rec);
#results = client.service.getMetricReport(rec);
#print(results);



EndDate = datetime.utcnow()
StartDate =EndDate + timedelta(hours=-1)
StartDate=StartDate.replace(minute=0,second=0,microsecond=0)
EndDate=EndDate.replace(minute=0,second=0,microsecond=0)
print("")
print("***********************************************************************")
print("")
print("Energy report :  hour - all")
print(StartDate.strftime('%Y-%m-%d %H:%M:%S'))
print(EndDate.strftime('%Y-%m-%d %H:%M:%S'))

rec = {
    'locationID': '00000000-0000-0002-0000-000000000000',
    'fromDateTime': StartDate,
    'toDateTime': EndDate,
    'period': 'Hour',
    'metricType':'Energy'
}
print(rec);
results = client.service.getMetricReport(rec);
print(results);








EndDate = datetime.utcnow()
StartDate =EndDate + timedelta(hours=-1)
StartDate=StartDate.replace(minute=0,second=0,microsecond=0)
EndDate=EndDate.replace(minute=0,second=0,microsecond=0)
print("")
print("***********************************************************************")
print("")
print("Energy report :  hour - 2002")
print(StartDate.strftime('%Y-%m-%d %H:%M:%S'))
print(EndDate.strftime('%Y-%m-%d %H:%M:%S'))

rec = {
    'locationID': '00000000-0000-0001-0000-0000000007D2',
    'fromDateTime': StartDate,
    'toDateTime': EndDate,
    'period': 'Hour',
    'metricType':'Energy'
}

print(rec);
results = client.service.getMetricReport(rec);
print(results);


EndDate = datetime.now()
StartDate =EndDate + timedelta(hours=-1)
StartDate=StartDate.replace(minute=0,second=0,microsecond=0)
EndDate=EndDate.replace(minute=0,second=0,microsecond=0)
print("")
print("***********************************************************************")
print("")
print("Energy report :  hour - 2002")
print(StartDate.strftime('%Y-%m-%d %H:%M:%S'))
print(EndDate.strftime('%Y-%m-%d %H:%M:%S'))

rec = {
    'locationID': '00000000-0000-0001-0000-0000000007D2',
    'fromDateTime': StartDate,
    'toDateTime': EndDate,
    'period': 'Quarter',
    'metricType':'Energy'
}

print(rec);
results = client.service.getMetricReport(rec);
print(results);


for metricreport in results['metricReportList']:
    FromDate=metricreport['fromDateTime'];
    ToDate=metricreport['toDateTime'];
    if(FromDate==ToDate):
        continue
    FromDateLocal = FromDate.astimezone(LocalZone);
    avgKW = metricreport['avgKW'];
    maxKW = metricreport['maxKW']; 
    minKW= metricreport['minKW'];
    startAccKWH= metricreport['startAccKWH'];
    endAccKWH= metricreport['endAccKWH'];
    AccKWH = endAccKWH-startAccKWH;
    chaine = "{0},{1},{2},{3},{4},{5}".format(FromDateLocal.strftime('%Y-%m-%d %H:%M:%S'), avgKW,minKW,maxKW,startAccKWH,endAccKWH,AccKWH)
    print(chaine)





EndDate = datetime.utcnow()
StartDate =EndDate + timedelta(hours=-1)
StartDate=StartDate.replace(minute=0,second=0,microsecond=0)
EndDate=EndDate.replace(minute=0,second=0,microsecond=0)
print("")
print("***********************************************************************")
print("")
print("Energy report :  LAST")
print(StartDate.strftime('%Y-%m-%d %H:%M:%S'))
print(EndDate.strftime('%Y-%m-%d %H:%M:%S'))

rec = {
    'locationID': '00000000-0000-0001-0000-0000000007D2',
    'metricType':'Energy'
}
print(rec);
#results = client.service.getMetricReport(rec);
#print(results);

exit()









#line="";
#lineglobal=""

#fichierGlobal=open("d:\\GlobalOccupancy.csv","a")
#fichierdetail=open("d:\\detailledOccupancy.csv","a")


            #print(line)



# whole 1st Floor : FFCD13BD-AFAD-46AD-9FE6-17904FC91D41
#nobel : 00000000-0000-0001-0000-0000000007D2
#response = incident.service.insert(**rec)
#result = client.service.getMetricReport('00000000-0000-0001-0000-0000000007D7',StartDate,EndDate,'MINUTE','Occupancy');



StartDate = datetime(2017,12,31,23,55,0,0)
print(StartDate.strftime('%Y-%m-%d %H:%M:%S'))

DateNow = datetime.now()
while(StartDate<DateNow):
    EndDate=StartDate+timedelta(minutes=+9)
    print(EndDate.strftime('%Y-%m-%d %H:%M:%S'))

    rec = {
        'locationID': '00000000-0000-0001-0000-0000000007D2',
        'fromDateTime': StartDate,
        'toDateTime': EndDate,
        'period': 'Minute',
        'metricType':'Occupancy'
    }
    print(rec);
    results = client.service.getMetricReport(rec);
    print(results)
    StartDate = StartDate+timedelta(minutes=+10)
    continue;
    #for metricreport in results['metricReportList'] :
    #print(oneresult)
    #for oner#esult in oneresult['metricReportList']:
        #print(metricreport)
        #reporttime = metricreport['fromDate']
        #Room = 2002
        #Global =     metricreport['occupiedPercentage']
        #lineglobal="{0};{1};{2}\n".format(reporttime,Room,Global)
        #fichierGlobal.write(lineglobal)
        #for sensor in metricreport['childOccupancyList']:
        #    sensorId = sensor['sensorId']
        #    occupation=sensor['occupiedPercentage']
    
        #    line ="{0};{1};{2};{3};{4}\n".format(reporttime,Room,Global,sensorId,occupation)
        #    fichierdetail.write(line)
    

    StartDate = StartDate+timedelta(minutes=+10)
    
fichierGlobal.close()
fichierdetail.close()

exit()





