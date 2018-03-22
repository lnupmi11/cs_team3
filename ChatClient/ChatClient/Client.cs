using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public class Client
    {
        private TcpClient tcpClient;

        public void Connect()
        {
            tcpClient = new TcpClient();
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 1234;
            tcpClient.Connect(ipAddress, port);
        }

        public void SendMessage(string message)
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public void Disconnect()
        {
            tcpClient.Close();
        }
    }
}
