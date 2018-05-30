using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartUdpServer
{
    class Program
    {
        static bool OffLine = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            Console.WriteLine("Press O to go in OFFLINE mode, any key for online mode");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.O)
                OffLine = true;


            SmartUdpCom.Server srv = new SmartUdpCom.Server();
            srv.NewMessage += Srv_NewMessage;
            srv.Run();
        }

        private static void Srv_NewMessage(string Msg)
        {
            string[] tab= Msg.Split(new char[] { ';' });
            int RoomId = Convert.ToInt32(tab[0]);
            int FixId = Convert.ToInt32(tab[1]);
            int Level = Convert.ToInt32(tab[2]);

            
            if (!OffLine)
            {
                Console.WriteLine("Send {0} to Room {1} / fixture {2}", Level, RoomId, FixId);
                UnityApps.LightManagement.Init();
                UnityApps.LightManagement.SetLevel(RoomId, FixId, Level);
            }
            else
                Console.WriteLine("Receive {0} to Room {1} / fixture {2}", Level, RoomId, FixId);
        }
    }
}
