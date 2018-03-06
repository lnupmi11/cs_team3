using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {
        private static bool serverRunning = false;

        static void Main(string[] args)
        {
            ShowHeader();

            Console.WriteLine();
            Console.WriteLine("Press [S] to start server. Press [Q] to stop server. Press [E] to exit");

            ConsoleKeyInfo cki = Console.ReadKey(true);
            while (cki.Key != ConsoleKey.E)
            {
                if (cki.Key == ConsoleKey.S)
                {
                    StartServer();
                }
                else if (cki.Key == ConsoleKey.Q)
                {
                    StopServer();
                }
                else
                {
                    Console.WriteLine("You have pressed wrong key");
                }
                cki = Console.ReadKey(true);
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

        public static void StartServer()
        {
            if (serverRunning == true)
            {
                Console.WriteLine("Server is already running");
            }
            else
            {                               
                serverRunning = true;

                Console.WriteLine("The sever has been started");                
            }
        }

        public static void StopServer()
        {
            if (serverRunning == true)
            {
                serverRunning = false;

                Console.WriteLine("Server has been stopped");
            }
            else
            {
                Console.WriteLine("Server is stopped already");
            }
        }

    }
}
