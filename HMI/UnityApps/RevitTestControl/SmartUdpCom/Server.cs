using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace SmartUdpCom
{
    public delegate void delegate_NewMessage(string Msg);
    public class Server
    {
        public event delegate_NewMessage NewMessage;
        UdpClient udpServer;

        public Server()
        {
            udpServer = new UdpClient(11285);
           
        }

        public void Run()
        {
            while (true)
            {
                var remoteEP = new IPEndPoint(IPAddress.Any, 11285);
                var data = udpServer.Receive(ref remoteEP); // listen on port 11000
                string strdata = System.Text.ASCIIEncoding.ASCII.GetString(data);
                Console.WriteLine("receive data from " + remoteEP.ToString() + "-"+ strdata);
                if (this.NewMessage != null)
                    NewMessage(strdata);
            }
        }

    }
}
