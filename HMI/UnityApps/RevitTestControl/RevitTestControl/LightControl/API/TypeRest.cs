using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ComponentModel;

namespace UnityApps.LightControl.API
{
    [DataContract]
    public class Area
    {
        [DataMember]
        public string name;
        [DataMember]
        public bool isUserAuthorized;
        [DataMember]
        public int Category;
        [DataMember]
        public bool temperatureSetPointAvailable;
        [DataMember]
        public int areaID;
    }

    [DataContract]
    public class AreaLevels
    {
        [DataMember]
        public AreaLevel[] areaLevels;
    }

    [DataContract]
    public class AreaLevel
    {
        [DataMember]
        public string name;
        [DataMember]
        public int areaLevel;
        [DataMember]
        public int areaID;

        public override string ToString()
        {
            return name;
        }
    }

    [DataContract]
    public class Areas
    {
        [DataMember]
        public Area[] Area_List;

    }

    [DataContract]
    public class Luminaires
    {
        [DataMember]
        public int areaID;

        [DataMember]
        public int areaLevel;

        [DataMember]
        public Luminaire[] luminaireLevels;

       

    }


    [DataContract]
    public class Luminaire
    {
        [DataMember]
        public int luminaireID;

        [DataMember]
        public int luminaireLevel;

        //[DataMember]
        //public int luminaireType;
    }
}
