using System;
using System.Threading;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            Thread thread = new Thread(new ThreadStart(server.Start));
            thread.Start();
            ListenCommands(server);
        }

        public static void ListenCommands(Server server)
        {
            while (true)
            {
                string line = Console.ReadLine();
                if (line == "show client list")
                {
                    server.ShowClientList();
                }
                else if (line == "kill client")
                {
                    KillClient(server);

                }
                else if (line == "exit")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Not exist such command");
                }
            }
        }

        public static void KillClient(Server server)
        {
            Console.WriteLine("Enter user name: ");
            string userName = Console.ReadLine();
            if (server.Exist(userName))
            {
                server.DeleteConnection(userName);
            }
            else
            {
                Console.WriteLine("Not exist client with this user name");
            }
        }

        public static void ShowHeader()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine(" ______     __  __     ______     ______");
            Console.WriteLine("/\\  ___\\   /\\ \\_\\ \\   /\\  __ \\   /\\__  _\\");
            Console.WriteLine("\\ \\ \\____  \\ \\  __ \\  \\ \\  __ \\  \\/_/\\ \\/");
            Console.WriteLine(" \\ \\_____\\  \\ \\_\\ \\_\\  \\ \\_\\ \\_\\    \\ \\_\\");
            Console.WriteLine("  \\/_____/   \\/_/\\/_/   \\/_/\\/_/     \\/_/");

            Console.ResetColor();
        }
    }
}
