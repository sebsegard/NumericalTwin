using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace SmartUdpCom
{
    public class Client
    {
        UdpClient udpClient;

        public Client()
        {
            udpClient = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11285); // endpoint where server is listening
            udpClient.Connect(ep);

        }

        //public void SendData(byte[] data)
        //{
        //    udpClient.Send(data, data.Length);
        //}

        public void SendData(string Chaine)
        {
            byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(Chaine);
            udpClient.Send(data, data.Length);
        }
    }
}
