# What is LogBook?
-- [![Build Status](https://travis-ci.org/tommcclean/LogBook.svg?branch=master)](https://travis-ci.org/tommcclean/LogBook)
- LogBook is a really small and simple .NET library that plugs into any .NET application, such as an MVC Website. 
- LogBook provides a *LogHandler* which can be called from your code, the *LogHandler* manages writing Log Entries into your database and allows you to read back the Log Entries in a variety of ways.
- Logbook integrates with SQL Server and manages its schema automatically, so no nasty scripts to run.

# How do you use LogBook?
- Review the *Lookbook.Demo* project for a working example of the project.

1. Add LogBook to your Project using the Nuget Package Manager (NPM) *Install-Package tommcclean.LogBook*
2. Add a Database Connection String to your Configuration File called *LogBookEntityModel*
3. Initialise a LogHandler: *var logHandler = new LogHandler();*
4. Write a Log Entry: *logHandler.WriteLog(LogType.Information, "Welcome to LogBook", string.Empty);*
5. Read Log Entries from the Database: *var logEntries = logHandler.ReadLatestLogEntries(100);*
