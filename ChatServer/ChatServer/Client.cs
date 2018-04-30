using System;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    public class Client
    {
        private TcpClient tcpClient;
        private Server server;
        public string Id { get; private set; }
        public string UserName { get; private set; }
        public NetworkStream Stream { get; private set; }

        public Client(TcpClient tcpClient, Server server)
        {
            this.Id = Guid.NewGuid().ToString();
            this.tcpClient = tcpClient;
            this.server = server;
        }

        public void Listen()
        {
            try
            {
                Stream = tcpClient.GetStream();
                string message = GetMessage();
                UserName = message;

                message = UserName + " join to chat";
                server.SendMessageToAllClients(message, this.Id);
                Console.WriteLine(message);
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        message = $"{UserName}: {message}";
                        Console.WriteLine(message);
                        server.SendMessageToAllClients(message, this.Id);
                    }
                    catch
                    {
                        message = $"{UserName}: left the chat";
                        Console.WriteLine(message);
                        server.SendMessageToAllClients(message, this.Id);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                server.DeleteConnection(this.Id);
                Close();
            }
        }

        private string GetMessage()
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }

        public void Close()
        {
            if (Stream != null)
            {
                Stream.Close();
            }
            if (tcpClient != null)
            {
                tcpClient.Close();
            }
        }
    }
}
