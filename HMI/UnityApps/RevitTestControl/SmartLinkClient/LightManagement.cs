using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLinkClient
{
    public class LightManagement
    {
        static SmartLinkCom.MyClient Myclient=null;

        public static void Init()
        {
          
            Myclient = new SmartLinkCom.MyClient("SmarttLinkCom");
        }

        public static void SwichOn(int pAreaId, int pLuminaireId)
        {
            if (Myclient == null)
                Myclient = new SmartLinkCom.MyClient("SmarttLinkCom");
            Myclient.SendOrder(pAreaId, pLuminaireId, 100);
        }

        public static void SwichOff(int pAreaId, int pLuminaireId)
        {
            if (Myclient == null)
                Myclient = new SmartLinkCom.MyClient("SmarttLinkCom");
            Myclient.SendOrder(pAreaId, pLuminaireId, 0);
        }

        public static void SetLevel(int pAreaId, int pLuminaireId, int pLevel)
        {
            if (Myclient == null)
                Myclient = new SmartLinkCom.MyClient("SmarttLinkCom");
            Myclient.SendOrder(pAreaId, pLuminaireId, pLevel);
        }
    }
}
