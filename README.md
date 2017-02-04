# What is LogBook?
- LogBook is a really small and simple .NET library that plugs into any .NET application, such as an MVC Website. 
- LogBook provides a *LogHandler* which can be called from your code, the *LogHandler* manages writing Log Entries into your database and allows you to read back the Log Entries in a variety of ways.
- Logbook integrates with SQL Server and manages its schema automatically, so no nasty scripts to run.

# How do you use LogBook?
- We are planning to add LogBook to the Nuget Package Manager in the near future.
- Review the *Lookbook.Demo* project for a working example of the project.

1. To use LogBook download the source code and add a reference to the *LogBook.Services* and *LogBook.Entities* DLL's within the source code.
2. Initialise a LogHandler: *var logHandler = new LogHandler();*
3. Write a Log Entry: *logHandler.WriteLog(LogType.Information, "Welcome to LogBook", string.Empty);*
4. Read Log Entries from the Database: *var logEntries = logHandler.ReadLatestLogEntries(100);*
