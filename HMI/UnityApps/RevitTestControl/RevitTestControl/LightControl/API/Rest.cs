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

    public static class PhilipsRestApi
    {
        private static string UserName = "ssegard";
        private static string UserPass = "C3siN@nterre";
        private static string ServiceUrl = "https://nrbdfpem.viacesi.fr/services/rest/control_restservice";

        private static  bool _IsConnected = false;

        public static void Init()
        {
            if(!_IsConnected)
            {

            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
               | SecurityProtocolType.Tls11
               | SecurityProtocolType.Tls12
               | SecurityProtocolType.Ssl3;
            }
        }


        public static Areas GetAreas()
        {
            Init();
            string url = string.Format(@"{0}/getAllLocations", ServiceUrl);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(string.Format("{0}:{1}", UserName, UserPass)));
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Areas));

                System.IO.Stream str = response.GetResponseStream();
                object objResponse = jsonSerializer.ReadObject(str);
                Areas jsonResponse
                = objResponse as Areas;

                return jsonResponse;
            }
        }

        public static AreaLevel[] GetAreaLevels(int pAreaId)
        {
            Init();
            string url = string.Format(@"{0}/getAreaLevelsForArea/{1}", ServiceUrl, pAreaId);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(string.Format("{0}:{1}", UserName, UserPass)));
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(AreaLevels));

                System.IO.Stream str = response.GetResponseStream();
                object objResponse = jsonSerializer.ReadObject(str);
                AreaLevels jsonResponse
                = objResponse as AreaLevels;

                return jsonResponse.areaLevels;
            }
        }

        public static Luminaire[] GetLuminerLevels(Area pArea)
        {
            Init();
            //string url = @"https://nrbdfpem.viacesi.fr/services/rest/control_restservice/getCurrentStatus/"+pArea.areaID;
            //string url = @"https://nrbdfpem.viacesi.fr/services/rest/control_restservice/getCurrentStatus/" + pArea.areaID;
            string url = string.Format(@"{0}/getCurrentStatus/{1}", ServiceUrl, pArea.areaID);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(string.Format("{0}:{1}", UserName, UserPass)));
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Luminaires));

                System.IO.Stream str = response.GetResponseStream();
                object objResponse = jsonSerializer.ReadObject(str);
                Luminaires jsonResponse
                = objResponse as Luminaires;

                return jsonResponse.luminaireLevels;
            }
        }

        public static Luminaire[] GetLuminerLevels(int pAreaId)
        {
            Init();
            //string url = @"https://nrbdfpem.viacesi.fr/services/rest/control_restservice/getCurrentStatus/"+pArea.areaID;
            //string url = @"https://nrbdfpem.viacesi.fr/services/rest/control_restservice/getCurrentStatus/" + pArea.areaID;
            string url = string.Format(@"{0}/getCurrentStatus/{1}", ServiceUrl, pAreaId);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(string.Format("{0}:{1}", UserName, UserPass)));
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Luminaires));

                System.IO.Stream str = response.GetResponseStream();
                object objResponse = jsonSerializer.ReadObject(str);
                Luminaires jsonResponse
                = objResponse as Luminaires;

                return jsonResponse.luminaireLevels;
            }
        }

        public static int GetLuminerLevel(int pAreaId, int pLuminaire)
        {
            Luminaire[] luminaire = GetLuminerLevels(pAreaId);

            for (int i = 0; i < luminaire.Length; i++)
            {
                if (luminaire[i].luminaireID == pLuminaire)
                    return luminaire[i].luminaireLevel;
            }

            return -1;
        }

        public static void SetLuminerLevel(Area pArea, Luminaire pLuminaire, int pLevel)
        {
            Init();
            string url = string.Format(
                @"{0}/applyLuminaireLevel/{1}/{2}/{3}",
                ServiceUrl,
                pArea.areaID,
                pLuminaire.luminaireID,
                pLevel);



            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(string.Format("{0}:{1}", UserName, UserPass)));
            request.Method = "PUT";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));


            }
        }

        public static void SetLuminerLevel(int pAreaId, int pLuminaireId, int pLevel)
        {
            Init();
            string url = string.Format(
                @"{0}/applyLuminaireLevel/{1}/{2}/{3}",
                ServiceUrl,
                pAreaId,
                pLuminaireId,
                pLevel);



            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(string.Format("{0}:{1}", UserName, UserPass)));
            request.Method = "PUT";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));


            }
        }

        public static void SetAreaLevel(int pAreaId, int pLevel)
        {
            Init();
            string url = string.Format(
                @"{0}/applyAreaLevel/{1}/{2}",
                ServiceUrl,
                pAreaId,
                pLevel);



            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(string.Format("{0}:{1}", UserName, UserPass)));
            request.Method = "PUT";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));


            }
        }
    }
}
