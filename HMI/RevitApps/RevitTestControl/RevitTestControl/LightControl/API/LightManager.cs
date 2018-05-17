using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace RevitTestControl.LightControl.API
{
    public static class LightManager
    {
        private static Dictionary<string, LightFixture> _Lights;
        private const string XmlConfigFileName = "RevitLighMngtConfig.xml";
        

        private static string XmlFileName
        {
            get
            {
                return "D:\\" + XmlConfigFileName;
            }
        }


        public static LightFixture GetLight(string GUID)
        {
            if (_Lights == null)
            {
                _Lights = new Dictionary<string, LightFixture>();

                LoadXml();
            }

            LightFixture light = new LightFixture(GUID);
            if (_Lights.TryGetValue(GUID, out light))
                return light;
            else
            {
                light = new LightFixture(GUID);
                _Lights.Add(GUID, light);
            }
                
            return light;
        }

        static void LoadXml()
        {
            if (!File.Exists(XmlFileName))
                return;
            XmlDocument doc = new XmlDocument();
            doc.Load(XmlFileName);

            foreach (XmlNode nd in doc.DocumentElement)
            {
                string GUID = nd.Attributes["GUID"].Value;
                int AreaId = Convert.ToInt32(nd.Attributes["AreaId"].Value);
                int LuminaireId = Convert.ToInt32(nd.Attributes["LuminaireId"].Value);
                if (LuminaireId == LightFixture.UNKNOW_LIGHT_ID)
                {
                    int OnLevelForArea = Convert.ToInt32(nd.Attributes["OnLevelForArea"].Value);
                    int OffLevelForArea = Convert.ToInt32(nd.Attributes["OffLevelForArea"].Value);
                }
                _Lights.Add(GUID, new LightFixture(GUID, AreaId, LuminaireId));
            }
        }

        static public void Clear()
        {
            if (_Lights != null && _Lights.Count != 0)
                _Lights.Clear();
        }

        static public void SaveXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<XmlLightMngt></XmlLightMngt>");

            foreach (var item in _Lights)
            {
                LightFixture fix = item.Value;

                XmlElement ele = doc.CreateElement("Light");
                ele.SetAttribute("GUID", fix.GUID);
                ele.SetAttribute("AreaId", fix.AreaId.ToString());
                ele.SetAttribute("LuminaireId", fix.LuminaireId.ToString());
                if (fix.LuminaireId == LightFixture.UNKNOW_LIGHT_ID)
                {
                    ele.SetAttribute("OnLevelForArea", fix.LuminaireId.ToString());
                    ele.SetAttribute("OffLevelForArea", fix.LuminaireId.ToString());
                }
                doc.DocumentElement.AppendChild(ele);
            }
            doc.Save(XmlFileName);
        }

    }

    public class LightFixture
    {
        public const int UNKNOW_LIGHT_ID = -1;

        public string GUID { get; set; }
        public int AreaId { get; set; }
        public int LuminaireId { get; set; }

        public int OnLevelForArea { get; set; }
        public int OffLevelForArea { get; set; }

        public LightFixture(string pGUID, int pAreaId, int pLuminaireId )
        {
            GUID = pGUID;
            AreaId = pAreaId;
            LuminaireId = pLuminaireId;
            OnLevelForArea = 1;
            OffLevelForArea = 4;
        }

        public LightFixture(string pGUID, int pAreaId, int pLuminaireId, int pOnLevelForArea, int pOffLevelForArea)
        {
            GUID = pGUID;
            AreaId = pAreaId;
            LuminaireId = pLuminaireId;
            OnLevelForArea = pOnLevelForArea;
            OffLevelForArea = pOffLevelForArea;
        }

        public LightFixture(string pGUID)
        {
            GUID = pGUID;
            AreaId = UNKNOW_LIGHT_ID;
            LuminaireId = UNKNOW_LIGHT_ID;
            OnLevelForArea = 1;
            OffLevelForArea = 4;
        }

        public void SetLevel(int pLevel)
        {
            if(LuminaireId != UNKNOW_LIGHT_ID)
                API.PhilipsRestApi.SetLuminerLevel(AreaId, LuminaireId, pLevel);
        }

        public int GetLevel()
        {
            if(LuminaireId != UNKNOW_LIGHT_ID)
                return API.PhilipsRestApi.GetLuminerLevel(AreaId, LuminaireId);
            return 0;
        }

        public void On()
        {
            if (LuminaireId != UNKNOW_LIGHT_ID)
                SetLevel(100);
            else
                API.PhilipsRestApi.SetAreaLevel(AreaId, OnLevelForArea);

        }

        public void Off()
        {
            if (LuminaireId != UNKNOW_LIGHT_ID)
                SetLevel(0);
            else
                API.PhilipsRestApi.SetAreaLevel(AreaId,OffLevelForArea);
        }
    }
}
