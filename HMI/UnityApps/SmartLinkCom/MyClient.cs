using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamedPipeWrapper;


namespace SmartLinkCom
{

    public class MyClient
    {


        NamedPipeClient<MyMessage> client;

        public MyClient(string pipeName)
        {
            client = new NamedPipeClient<MyMessage>(pipeName);
            client.ServerMessage += OnServerMessage;
            client.Error += OnError;
            client.Start();

          
        }

        public void Stop()
        {
            client.Stop();
        }


        private void OnServerMessage(NamedPipeConnection<MyMessage, MyMessage> connection, MyMessage message)
        {
          
        }

        public void SendOrder(int RoomId, int FixId, int Level)
        {
            MyMessage msg = new MyMessage { RoomId = RoomId, FixId = FixId, Level = Level };

            client.PushMessage(msg);
        }

        private void OnError(Exception exception)
        {
            Console.Error.WriteLine("ERROR: {0}", exception);
        }
    }
}
