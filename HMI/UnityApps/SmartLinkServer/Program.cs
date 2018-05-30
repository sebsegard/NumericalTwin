using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamedPipeWrapper;

namespace SmartLinkServer
{
  
    class Program
    {


        static void Main(string[] args)
        {
            SmartLinkCom.MyServer srv = new SmartLinkCom.MyServer("SmarttLinkCom");

            srv.NewLightMngtOrder += Srv_NewLightMngtOrder;
            srv.Start();
        }

        private static void Srv_NewLightMngtOrder(int RoomId, int FixId, int Level)
        {
            Console.WriteLine("Msg - " + RoomId + "-" + FixId + "-" + Level);
            UnityApps.LightManagement.SetLevel(RoomId, FixId, Level);
        }
    }
}
