using System;
using System.Threading;
using log4net;
using log4net.Config;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.InitLogger();
            Server server = new Server();
            Thread thread = new Thread(new ThreadStart(server.Start));
            thread.Start();
            ListenCommands(server);
        }

        public static void ListenCommands(Server server)
        {
            Console.WriteLine();
            Console.WriteLine("Press [S] to show client list. Press [K] to kill client. Press [E] to exit");
            ConsoleKeyInfo cki = Console.ReadKey(true);
            while (cki.Key != ConsoleKey.F)
            {
                if (cki.Key == ConsoleKey.S)
                {
                    Console.WriteLine("List of clients:");
                    server.ShowClientList();
                }
                else if (cki.Key == ConsoleKey.K)
                {
                    Console.WriteLine("Killing client:");
                    KillClient(server);
                }
                else if (cki.Key == ConsoleKey.E)
                {             
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("You have pressed the wrong key");
                }
                cki = Console.ReadKey(true);
            }          
        }
        
        public static void KillClient(Server server)
        {
            Console.WriteLine("Enter user name: ");
            string userName = Console.ReadLine();
            if (server.Exist(userName))
            {
                server.DeleteConnection(userName);
                Console.WriteLine($"Client {userName} has been deleted!");
            }
            else
            {
                Console.WriteLine("Client with this user name does not exist");
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
