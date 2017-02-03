using LogBook.Services;
using LogBook.Services.Models;
using System;

namespace LogBook.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Step 1: Initialise The Log Handler and Pass a Database Connection String
            var logHandler = new LogHandler("name=LogBookEntityModel");

            // Step 2: Log Basic Information Messages
            logHandler.WriteLog(LogType.Information, "Welcome to LogBook", string.Empty);

            // Step 3: Log Exception Details
            try
            {
                throw new NotImplementedException("This is an example Exception");
            }
            catch (Exception ex)
            {
                logHandler.WriteLog(LogType.Error, "LogBook.Demo.Program.cs", ex, ex.Message, string.Empty);
            }

            Console.WriteLine("Demo Log Entries have been logged. Press any key to retrieve the latest Log Entries.");
            Console.ReadKey();

            var logEntries = logHandler.ReadLatestLogEntries(100);

            foreach(var entry in logEntries)
            {
                Console.WriteLine($"Log Entry: {entry.LogEntryId}. Message: {entry.Message}. Time: {entry.LogTime}. Host: {entry.HostName}.");
            }

            Console.WriteLine("Demo Log Entries have been retrieved. Press any key to terminate the Demo.");
            Console.ReadKey();
        }
    }
}