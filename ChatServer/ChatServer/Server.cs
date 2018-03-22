using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Server
    {
        private TcpListener tcpListener;

        public void Start()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 1234;
            tcpListener = new TcpListener(ipAddress, port);
            tcpListener.Start();
            Console.WriteLine("Server started.");

            TcpClient tcpClient = tcpListener.AcceptTcpClient(); 
            Console.WriteLine("New client connected.");
            ReadMessage(tcpClient);
        }

        public void ReadMessage(TcpClient tcpClient)
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length); 
            string message = Encoding.UTF8.GetString(data, 0, bytes);
            Console.WriteLine(message);
        }
        
        public void Stop()
        {
            tcpListener.Stop();
            Console.WriteLine("Server stoped.");
        }
    }
}
