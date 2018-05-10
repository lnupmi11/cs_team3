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
                HandleUserName();
                string message = UserName + " join to chat";
                Logger.Log.Info(message);
                server.SendMessageToAllClients(message, this.Id);
                Console.WriteLine(message);
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        HandleMessage(message);
                    }
                    catch
                    {
                        message = $"{UserName}: left the chat";
                        Logger.Log.Info(message);
                        Console.WriteLine(message);
                        server.SendMessageToAllClients(message, this.Id);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
                Console.WriteLine(e.Message);
            }
            finally
            {
                server.DeleteConnection(this.Id);
                Close();
            }
        }

        private void HandleUserName()
        {
            string message = GetMessage();
            bool exist = server.Exist(message);
            if (exist)
            {
                do
                {
                    byte[] data = Encoding.Unicode.GetBytes("409");
                    Stream.Write(data, 0, data.Length);
                    message = GetMessage();
                    exist = server.Exist(message);
                } while (exist);
            }
            byte[] successfulMassage = Encoding.Unicode.GetBytes("200");
            Stream.Write(successfulMassage, 0, successfulMassage.Length);
            UserName = message;
        }

        private void HandleMessage(string message)
        {
            bool isPrivateMessage = message.StartsWith("@");
            string consoleMessage = $"{UserName}: {message}";
            if (isPrivateMessage)
            {
                HandlePrivateMassage(UserName, message);
            }
            else
            {
                server.SendMessageToAllClients(consoleMessage, this.Id);
            }
            Console.WriteLine(consoleMessage);
            //Logger.Log.Info(consoleMessage);
        }

        private void HandlePrivateMassage(string username, string message)
        {

            string[] parts = message.Split(new[] { ' ' }, 2);
            string clientName = parts[0].Remove(0, 1);
            if (parts.Length == 1)
            {
                message = $"{UserName}:";
            }
            else
            {
                message = $"{UserName}: {parts[1]}";
            }
            server.SendMessageToClient(message, clientName, this.Id);
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
