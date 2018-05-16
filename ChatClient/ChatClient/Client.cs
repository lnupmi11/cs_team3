using ChatClient.Configuration;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatClient
{
    public class Client
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private ConfigurationProvider configurationProvider;

        public Client()
        {
            configurationProvider = new ConfigurationProvider();
        }

        public void Connect(User user)
        {
            try
            {
                tcpClient = new TcpClient();
                ConfigurationModel configurationModel = configurationProvider.Get();
                IPAddress ipAddress = IPAddress.Parse(configurationModel.IpAddress);
                tcpClient.Connect(ipAddress, configurationModel.Port);
                stream = tcpClient.GetStream();
                
                ReadUserName();
            
                Thread getMessageThread = new Thread(new ThreadStart(GetMessage));
                getMessageThread.Start();

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

        public void ReadUserName()
        {
            bool userNameIsOk;
            string name;
            do
            {
                Console.WriteLine("Input name: ");

                name = Console.ReadLine();
                SendMessage(name);
                string message = ReadMessageFromServer();
                userNameIsOk = message == "200";
                if (!userNameIsOk)
                {
                    Console.WriteLine("The wrong name, such username already exists");
                }
            } while (!userNameIsOk);
            Console.WriteLine($"Welcome, {name}");
        }

        public void ReadMessage()
        {
            Console.WriteLine("Input message (if you want to sent private message add @username):");
            while (true)
            {
                string message = Console.ReadLine();
                if (message == "exit")
                {
                    Environment.Exit(0);
                }
                SendMessage(message);
            }
        }

        public void SendMessage(string message)
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public string ReadMessageFromServer()
        {
            byte[] data = new byte[8];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);

            return builder.ToString();
        }

        public void GetMessage()
        {
            while (true)
            {
                try
                {
                    string message = ReadMessageFromServer();
                    Console.WriteLine(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The connection is lost!");
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
