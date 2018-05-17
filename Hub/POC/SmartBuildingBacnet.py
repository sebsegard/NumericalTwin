


class BacnetPoint:
	def __init__(self, Name, GroupId, PropId, PropType):
		self.Name=Name
		self.GroupId = GroupId
		self.PropId = PropId
		self.Id = GroupId*100+PropId
		self.Value="UnRead"
		self.PropType=PropType
		MySmartBuildingBacnet.PointDict[self.Id] = self
		
	def __str__(self):
		return "{0} :  {1} : {2}".format(self.Id, self.Name,self.PropType)
		
	def GetRawValue(self):
		return self.Value
	
	def GetValue(self):
		return self.Value
	
	def GetStrValue(self):
		return self.Value
		
	

		
class binaryOutput(BacnetPoint):
	def __init__(self, Name, GroupId, PropId):
		BacnetPoint.__init__(self, Name, GroupId, PropId,'binaryOutput')
		self.InactiveLabel="inactive"
		self.activeLabel="active"
		
	def __init__(self, Name, GroupId, PropId, InactiveLabel, ActibeLabel):
		BacnetPoint.__init__(self, Name, GroupId, PropId,'binaryOutput')
		self.InactiveLabel=InactiveLabel
		self.activeLabel=ActibeLabel
	
	def GetStrValue(self):
		return self.GetValue()
	
	def GetValue(self):
		if self.Value == "inactive":
			return self.InactiveLabel
		elif self.Value == "active":
			return self.activeLabel
		return "Unknown"	
	

class binaryInput(BacnetPoint):
	def __init__(self, Name, GroupId, PropId):
		BacnetPoint.__init__(self, Name, GroupId, PropId,'binaryInput')
		self.InactiveLabel="inactive"
		self.activeLabel="active"
		
	def __init__(self, Name, GroupId, PropId, InactiveLabel, ActibeLabel):
		BacnetPoint.__init__(self, Name, GroupId, PropId,'binaryInput')
		self.InactiveLabel=InactiveLabel
		self.activeLabel=ActibeLabel
	
	def GetStrValue(self):
		return self.GetValue()
	
	def GetValue(self):
		if self.Value == "inactive":
			return self.InactiveLabel
		elif self.Value == "active":
			return self.activeLabel
		return "Unknown"	
		
		
class binaryValue(BacnetPoint):
	def __init__(self, Name, GroupId, PropId):
		BacnetPoint.__init__(self, Name, GroupId, PropId,'binaryValue')
		self.InactiveLabel="inactive"
		self.activeLabel="active"
		
	def __init__(self, Name, GroupId, PropId, InactiveLabel, ActibeLabel):
		BacnetPoint.__init__(self, Name, GroupId, PropId,'binaryValue')
		self.InactiveLabel=InactiveLabel
		self.activeLabel=ActibeLabel
	
	def GetStrValue(self):
		return self.GetValue()
	
	def GetValue(self):
		if self.Value == "inactive":
			return self.InactiveLabel
		elif self.Value == "active":
			return self.activeLabel
		return "Unknown"	

class analogValue(BacnetPoint):
	def __init__(self, Name, GroupId, PropId):
		BacnetPoint.__init__(self, Name, GroupId, PropId,'analogValue')


	def GetStrValue(self):
		return "{0} C".format(self.Value)

class analogInput(BacnetPoint):
	def __init__(self, Name, GroupId, PropId):
		BacnetPoint.__init__(self, Name, GroupId, PropId,'analogInput')

	def GetStrValue(self):
		return "{0} C".format(self.Value)
		
class multiStateInput(BacnetPoint):
	def __init__(self, Name, GroupId, PropId, Labels):
		BacnetPoint.__init__(self, Name, GroupId, PropId,'multiStateInput')
		self.Labels = Labels
		
	def GetStrValue(self):
		return self.GetValue()
		
	def GetValue(self):
		str="ExceptionDuringReading"
		try:
			str =  self.Labels[self.Value]
		except:
			str="bug"
		return str
	
class multiStateOutput(BacnetPoint):
	def __init__(self, Name, GroupId, PropId, Labels):
		BacnetPoint.__init__(self, Name, GroupId, PropId,'multiStateOutput')
		self.Labels = Labels
		
	def GetStrValue(self):
		return self.GetValue()
		
	def GetValue(self):
		str="ExceptionDuringReading"
		try:
			str =  self.Labels[self.Value]
		except:
			str="bug"
		return str	


class BacnetRoom:
	def __init__(self, Name, GroupId, IdRoom):
		self.Name=Name
		self.GroupId = GroupId
		self.Points = []
		self.IdRoom = IdRoom		
		self.OnOffSetup = binaryOutput("On Off Setup",GroupId,1,"Stop","Run")
		self.Points.append(self.OnOffSetup)
		
		self.OnOffState = binaryInput("On Off state",GroupId,2,"Stop","Run")
		self.Points.append(self.OnOffState)
		
		self.ErrorCode = multiStateInput("Error Code",GroupId,4,["","Normal","Other Erros","Refrigeration system fault","Water ysstem error","air system error","Electronic system error","sensor fault","communication error", "system error"])
		self.Points.append(self.ErrorCode)
		
		self.OperationalModeSetup = multiStateOutput("Operational Mode Setup",GroupId,5,["","Cool","Heat","Fan","Auto","Dry","Setback"])
		self.Points.append(self.OperationalModeSetup)
		
		self.OperationalModeState = multiStateInput("Operational Mode State",GroupId,6,["","Cool","Heat","Fan","Auto","Dry","Setback"])
		self.Points.append(self.OperationalModeState)
		
		self.RoomTemp = analogInput("Room Temp",GroupId,9)
		self.Points.append(self.RoomTemp)
		
		self.SetTemp = analogValue("Set Temp",GroupId,10)
		self.Points.append(self.SetTemp)
		

class MySmartBuildingBacnet:
	PointDict = {}
	
	
	def __init__(self):
		self.Rooms = []
		
		self.Tesla = BacnetRoom("Tesla",106,1)
		self.Rooms.append(self.Tesla)
		
		self.Lumiere = BacnetRoom("Lumiere",107,2)
		self.Rooms.append(self.Lumiere)
		
	def GetPointListToRead(self):
		PointList=[]
		for key,value in MySmartBuildingBacnet.PointDict.items():
			PointList.append((value.PropType,value.Id,"presentValue"))
		return PointList
		
	def UpdateValues(self,Values):
		for key, value in Values.items():
			MySmartBuildingBacnet.PointDict[key].Value = value
			Point = MySmartBuildingBacnet.PointDict[key]
			print(Point.Id,"raw : ",Point.GetRawValue()," str Value : ",Point.GetStrValue())
		
	
		
		
		
		
