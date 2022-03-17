using Sqlapp.Interfaces;

namespace Sqlapp.Util
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}
