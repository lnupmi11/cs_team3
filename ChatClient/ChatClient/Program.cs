using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //Client client = new Client();
            //client.Connect();
            //client.SendMessage("hello!");

            ShowHeader();
            Console.ReadKey();
        }
    }
}
