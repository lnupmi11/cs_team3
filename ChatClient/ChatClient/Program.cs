﻿using System;

namespace ChatClient
{
    class Program
    {
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
        static void Main(string[] args)
        {
            ShowHeader();
            Console.Write("Input name: ");
            string userName = Console.ReadLine();
            User user = new User { UserName = userName };

            Client client = new Client();
            client.Connect(user);
            
            Console.ReadKey();
        }
    }
}
