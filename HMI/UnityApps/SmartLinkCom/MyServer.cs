using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamedPipeWrapper;
using System.Threading;

namespace SmartLinkCom
{
    public delegate void delegate_NewLightMngtOrder(int RoomId, int FixId, int Level);

    public class MyServer
    {
        public event delegate_NewLightMngtOrder NewLightMngtOrder;

        public bool Enabled {get; set;}

        NamedPipeServer<MyMessage> server;

        public MyServer(string pipeName)
        {
            server = new NamedPipeServer<MyMessage>(pipeName);
            server.ClientConnected += OnClientConnected;
            server.ClientDisconnected += OnClientDisconnected;
            server.ClientMessage += OnClientMessage;
            server.Error += OnError;
            
        }

        private bool KeepRunning
        {
            get
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q)
                    return false;
                return true;
            }
        }


        public void Start()
        {
            server.Start();
            Enabled = true;
            while (KeepRunning)
            {
                // Do nothing - wait for user to press 'q' key
            }
            server.Stop();
        }

        public void Stop()
        {
            
        }

        private void OnClientConnected(NamedPipeConnection<MyMessage, MyMessage> connection)
        {
            Console.WriteLine("Client {0} is now connected!", connection.Id);
            /*connection.PushMessage(new MyMessage
            {
                //Id = new Random().Next(),
                //Text = "Welcome!"
            });*/
        }

        private void OnClientDisconnected(NamedPipeConnection<MyMessage, MyMessage> connection)
        {
            Console.WriteLine("Client {0} disconnected", connection.Id);
        }

        private void OnClientMessage(NamedPipeConnection<MyMessage, MyMessage> connection, MyMessage message)
        {
            if (NewLightMngtOrder != null)
                NewLightMngtOrder(message.RoomId, message.FixId, message.Level);
        }

        private void OnError(Exception exception)
        {
            Console.Error.WriteLine("ERROR: {0}", exception);
        }
    }
}

