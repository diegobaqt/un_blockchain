using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace UNBlockchain.Extensions.ErrorLog
{
    public static class LoggerFactoryExtensions
    {
        public static ILoggerFactory AddTextWriter(this ILoggerFactory loggerFactory, IConfigurationSection configurationSection)
        {
            var logLevelSwitch = new SourceSwitch("LogLevel", SourceLevels.Off.ToString()) { Level = GetLogLevel(configurationSection) };
            var expandedFileName = GetLogFile(configurationSection);
            expandedFileName = Environment.ExpandEnvironmentVariables(expandedFileName);
            var localPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            if (expandedFileName.StartsWith("." + Path.DirectorySeparatorChar) ||
                expandedFileName.StartsWith("." + Path.AltDirectorySeparatorChar))
                expandedFileName = expandedFileName.Substring(2);
            expandedFileName = Path.IsPathRooted(expandedFileName) ? expandedFileName : Path.Combine(localPath, expandedFileName);

            var listener = new TextWriterTraceListener(expandedFileName);
            Trace.AutoFlush = true;
            return loggerFactory.AddTraceSource(logLevelSwitch, listener);
        }

        private static SourceLevels GetLogLevel(IConfigurationSection configurationSection)
        {
            if (configurationSection == null)
                return SourceLevels.Off;
            var logSection = configurationSection.GetSection("LogLevel");
            if (logSection == null)
                return SourceLevels.Off;
            var logLevel = logSection.GetValue<string>("Default");

            return FromLogLevel(logLevel);
        }

        private static SourceLevels FromLogLevel(string logLevel)
        {
            if (logLevel == LogLevel.Critical.ToString())
                return SourceLevels.Critical;
            if (logLevel == LogLevel.Debug.ToString())
                return SourceLevels.Verbose;
            if (logLevel == LogLevel.Error.ToString())
                return SourceLevels.Error;
            if (logLevel == LogLevel.Information.ToString())
                return SourceLevels.Information;
            if (logLevel == LogLevel.None.ToString())
                return SourceLevels.Off;
            if (logLevel == LogLevel.Trace.ToString())
                return SourceLevels.Verbose;
            return logLevel == LogLevel.Warning.ToString() ? SourceLevels.Warning : SourceLevels.Off;
        }

        private static string GetLogFile(IConfigurationSection configurationSection)
        {
            const string defaultFile = ".\\logs\\log.txt";
            var logFile = configurationSection?.GetValue<string>("LogFile");

            return logFile ?? defaultFile;
        }
    }
}
