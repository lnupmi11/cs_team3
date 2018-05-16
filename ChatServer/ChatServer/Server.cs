using ChatServer.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatServer
{
    public class Server
    {
        private TcpListener tcpListener;
        private List<Client> clients = new List<Client>();
        private ConfigurationProvider configurationProvider;

        public Server()
        {
            configurationProvider = new ConfigurationProvider();
        }

        public void Start()
        {
            ConfigurationModel configurationModel = configurationProvider.Get();
            IPAddress ipAddress = IPAddress.Parse(configurationModel.IpAddress);
            tcpListener = new TcpListener(ipAddress, configurationModel.Port);
            tcpListener.Start();
            Console.WriteLine("Server started.");
            Logger.Log.Info("Server started.");

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Client client = new Client(tcpClient, this);
                clients.Add(client);
                Thread clientThread = new Thread(new ThreadStart(client.Listen));
                clientThread.Start();
            }
        }

        public void ShowClientList()
        {
            foreach (Client a in clients)
            {
                Console.WriteLine(a.UserName);
            }
        }

        public void SendMessageToClient(string message, string clientName, string senderId)
        {
            byte[] data;
            Client client;
            if (Exist(clientName))
            {
                data = Encoding.Unicode.GetBytes(message);
                client = clients.First(c => c.UserName == clientName);
            }
            else
            {
                data = Encoding.Unicode.GetBytes("Wrong destination user name");
                client = clients.First(c => c.Id == senderId);
            }
            client.Stream.Write(data, 0, data.Length);
        }

        public void SendMessageToAllClients(string message, string senderId)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Id != senderId)
                {
                    clients[i].Stream.Write(data, 0, data.Length);
                }
            }
        }

        public bool Exist(string userName)
        {
            bool exist = clients.Any(c => c.UserName == userName);

            return exist;
        }

        public void DeleteConnection(string userName)
        {
            Client client = clients.FirstOrDefault(c => c.UserName == userName);
            if (client != null)
            {
                clients.Remove(client);
                client.Close();
            }
        }

        public void Stop()
        {
            if (tcpListener != null)
            {
                tcpListener.Stop();
            }
            Console.WriteLine("Server stoped.");
            Logger.Log.Info("Server stoped.");
        }
    }
}
