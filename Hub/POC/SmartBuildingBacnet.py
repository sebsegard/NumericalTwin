


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
		
	def GetNumericalValue(self):
		return self.Value
		
	def GetSetPoint(self,value):
		return (self.PropType,self.Id,value)

		
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
		
	def GetNumericalValue(self):
		if self.Value == "inactive":
			return 0
		return 1

	def GetSetPoint(self,value):
		ValueToSend = "inactive"
		if(value==self.activeLabel):
			ValueToSend = "active"
		return (self.PropType,self.Id,ValueToSend)
	
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
	
	def GetNumericalValue(self):
		if self.Value == "inactive":
			return 0
		return 1
	
	def GetValue(self):
		if self.Value == "inactive":
			return self.InactiveLabel
		elif self.Value == "active":
			return self.activeLabel
		return "Unknown"

	def GetSetPoint(self,value):
		ValueToSend = "inactive"
		if(value==self.activeLabel):
			ValueToSend = "active"
		return (self.PropType,self.Id,ValueToSend)

class binaryInput(BacnetPoint):
	def __init__(self, Name, GroupId, PropId):
		BacnetPoint.__init__(self, Name, GroupId, PropId,'binaryInput')
		self.InactiveLabel="inactive"
		self.activeLabel="active"
		
	def __init__(self, Name, GroupId, PropId, InactiveLabel, ActibeLabel):
		BacnetPoint.__init__(self, Name, GroupId, PropId,'binaryInput')
		self.InactiveLabel=InactiveLabel
		self.activeLabel=ActibeLabel
	
	def GetNumericalValue(self):
		if self.Value == "inactive":
			return 0
		return 1
	
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

	def GetSetPoint(self,value):
		ValueToSend = 0
		if(value.isdigit()):
			ValueToSend = value
		i=0
		for label in self.Labels:
			if(value == label):
				ValueToSend = i
				break
			i+=1
	
		return (self.PropType,self.Id,ValueToSend)


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
		
		self.SetTempCool = analogValue("Set Temp Cool",GroupId,24)
		self.Points.append(self.SetTempCool)
		
		self.SetTemp = analogValue("Set Temp",GroupId,10)
		self.Points.append(self.SetTemp)
		
		self.SetTempHeat = analogValue("Set Temp Heat",GroupId,25)
		self.Points.append(self.SetTempHeat)
		
		self.FanSpeedSetup = multiStateOutput("Fan Speed Setup",GroupId,7,["","Low","High","Mid2","Mid1","Auto"])
		self.Points.append(self.FanSpeedSetup)
		
		self.FanSpeedState = multiStateInput("Fan Speed State",GroupId,8,["","Low","High","Mid2","Mid1","Auto"])
		self.Points.append(self.FanSpeedState)
		
		self.ProhibitionOnOff = binaryValue("Prohibition On Off",GroupId,13,"Permit","Prohibit")
		self.Points.append(self.ProhibitionOnOff)
		
		self.ProhibitionMode = binaryValue("Prohibition Mode",GroupId,14,"Permit","Prohibit")
		self.Points.append(self.ProhibitionMode)
		
		self.ProhibitionTemp = binaryValue("Prohibition Temp",GroupId,16,"Permit","Prohibit")
		self.Points.append(self.ProhibitionMode)
		
	def ComputeSetTemp(self):
		Mode = self.OperationalModeSetup.GetStrValue()
		if(Mode =="Cool" or Mode =="Dry"): 
			self.SetTemp.Value = self.SetTempCool.Value
		elif(Mode =="Heat"): 
			self.SetTemp.Value = self.SetTempHeat.Value
			
class MySmartBuildingBacnet:
	PointDict = {}
	
	
	def __init__(self):
		self.Rooms = []
		
		self.Tesla = BacnetRoom("TESLA",106,1)
		self.Rooms.append(self.Tesla)
		
		self.Nobel = BacnetRoom("NOBEL",107,3)
		self.Rooms.append(self.Nobel)
		
		self.Nobel2 = BacnetRoom("NOBEL2",105,6)
		self.Rooms.append(self.Nobel2)
		
		self.Lumiere = BacnetRoom("LUMIERE",102,2)
		self.Rooms.append(self.Lumiere)
		
		self.Turing = BacnetRoom("TURING",101,4)
		self.Rooms.append(self.Turing)
		
		self.Metro = BacnetRoom("METRO",104,7)
		self.Rooms.append(self.Metro)
		
		self.Local = BacnetRoom("LOCAL",103,8)
		self.Rooms.append(self.Local)
		
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
		for Room in self.Rooms:
			Room.ComputeSetTemp()
	
	def GetSetPointsBuildingValues(self,Temperature, OnOffState,Mode, ProhibitionMode,ProhibitionOnOff,ProhibitionTemp, FanSpeedSetup):
		PointList = []
		for Room in self.Rooms:
			PointList.append(Room.SetTempCool.GetSetPoint(Temperature))
			PointList.append(Room.SetTempHeat.GetSetPoint(Temperature))
			PointList.append(Room.OnOffSetup.GetSetPoint(OnOffState))
			PointList.append(Room.OperationalModeSetup.GetSetPoint(Mode))
			PointList.append(Room.FanSpeedSetup.GetSetPoint(FanSpeedSetup))
			PointList.append(Room.ProhibitionMode.GetSetPoint(ProhibitionMode))
			PointList.append(Room.ProhibitionOnOff.GetSetPoint(ProhibitionOnOff))
			PointList.append(Room.ProhibitionTemp.GetSetPoint(ProhibitionTemp))
		return PointList
		
	def GetSetPointsRoomValues(self,RoomName,Temperature, OnOffState,Mode, ProhibitionMode,ProhibitionOnOff,ProhibitionTemp, FanSpeedSetup):
		PointList = []
		for Room in self.Rooms:
			if(Room.Name== RoomName):
				PointList.append(Room.SetTempCool.GetSetPoint(Temperature))
				PointList.append(Room.SetTempHeat.GetSetPoint(Temperature))
				PointList.append(Room.OnOffSetup.GetSetPoint(OnOffState))
				PointList.append(Room.OperationalModeSetup.GetSetPoint(Mode))
				PointList.append(Room.FanSpeedSetup.GetSetPoint(FanSpeedSetup))
				PointList.append(Room.ProhibitionMode.GetSetPoint(ProhibitionMode))
				PointList.append(Room.ProhibitionOnOff.GetSetPoint(ProhibitionOnOff))
				PointList.append(Room.ProhibitionTemp.GetSetPoint(ProhibitionTemp))
		return PointList
