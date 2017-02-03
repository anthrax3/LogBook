using LogBook.Services;
using LogBook.Services.Models;

namespace LogBook.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Welcome to the LogBook Demo
            LogHandler.WriteLog(LogType.Information, "Welcome to LogBook");
        }
    }
}