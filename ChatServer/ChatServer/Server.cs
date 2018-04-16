using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    public class Server
    {
        private TcpListener tcpListener;
        private List<Client> clients = new List<Client>();

        public void Start()
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 1234;
            tcpListener = new TcpListener(ipAddress, port);
            tcpListener.Start();
            Console.WriteLine("Server started.");

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Client client = new Client(tcpClient, this);
                clients.Add(client);
                Thread clientThread = new Thread(new ThreadStart(client.Listen));
                clientThread.Start();
            }
        }

        public void SendMessageToAllClients(string message, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Id != id)
                {
                    clients[i].Stream.Write(data, 0, data.Length);
                }
            }
        }

        public void DeleteConnection(string id)
        {
            Client client = clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
            {
                clients.Remove(client);
            }
        }

        public void Stop()
        {
            if (tcpListener != null)
            {
                tcpListener.Stop();
            }
            Console.WriteLine("Server stoped.");
        }
    }
}
