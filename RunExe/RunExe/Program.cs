using System;
using System.Diagnostics;

namespace RunExe
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter n");
            int size = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < size; i++)
            {
                Process.Start("ChatClient.exe");
            }
        }
    }
}
