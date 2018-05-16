using System;
using System.Diagnostics;

namespace RunExe
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter number of customers:");
            int size = Convert.ToInt32(Console.ReadLine());
            if( size > 100 ) 
            {
                Console.WriteLine( "The maximum number of customers is 100. Please, enter the number from 1 to 100." );
            }
            else
            {
                for( int i = 0; i < size; i++ ) 
                {
                    Process.Start( "ChatClient.exe" );
                }
            }
        }
    }
}
