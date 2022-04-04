using System;
using static System.Console;

namespace CosmosApp01
{
    public class Logger
    {
        public void LogInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
