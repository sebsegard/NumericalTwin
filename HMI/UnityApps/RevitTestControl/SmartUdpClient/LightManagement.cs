using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartUdpCLient
{
    public class LightManagement
    {
        static SmartUdpCom.Client Myclient=null;

        public static void Init()
        {
          
            Myclient = new SmartUdpCom.Client();
        }

        public static void SwichOn(int pAreaId, int pLuminaireId)
        {
            if (Myclient == null)
                Init();
            string data = string.Format("{0};{1};{2}", pAreaId, pLuminaireId, 100);

            Myclient.SendData(data);
        }

        public static void SwichOff(int pAreaId, int pLuminaireId)
        {
            if (Myclient == null)
                Init();
            string data = string.Format("{0};{1};{2}", pAreaId, pLuminaireId, 0);

            Myclient.SendData(data);
        }

        public static void SetLevel(int pAreaId, int pLuminaireId, int pLevel)
        {
            if (Myclient == null)
                Init();
            string data = string.Format("{0};{1};{2}", pAreaId, pLuminaireId, pLevel);

            Myclient.SendData(data);
        }
    }
}
