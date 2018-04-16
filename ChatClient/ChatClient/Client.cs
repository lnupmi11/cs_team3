using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient
{
    public class Client
    {
        private TcpClient tcpClient;
        private NetworkStream stream;

        public void Connect(User user)
        {
            try
            {
                tcpClient = new TcpClient();
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                int port = 1234;
                tcpClient.Connect(ipAddress, port);
                stream = tcpClient.GetStream();

                Thread getMessageThread = new Thread(new ThreadStart(GetMessage));
                getMessageThread.Start();
                Console.WriteLine($"Wellcome, {user.UserName}");
                SendMessage(user.UserName);
                ReadMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }

        public void ReadMessage()
        {
            Console.WriteLine("Input message:");
            while(true)
            {
                string message = Console.ReadLine();
                SendMessage(message);
            }
        }

        public void SendMessage(string message)
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public void GetMessage()
        {
            while(true)
            {
                try
                {
                    byte[] data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    Console.WriteLine(message);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Connection lost!"); 
                    Console.ReadLine();
                    Disconnect();
                }
            }
        }

        public void Disconnect()
        {
            stream.Close();
            tcpClient.Close();
        }
    }
}
