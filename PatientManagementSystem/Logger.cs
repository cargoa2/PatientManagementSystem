using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagementSystem
{
    public enum LogLevel
    {
        All = 1,
        Info,
        Debug,
        Error
    }
    public static class Logger
    {
        public static void Log(LogLevel LogLevel, string Message, string Content, string ExceptionText, string Data)
        {
            int levelOfEntryToLog;
            if (!int.TryParse(ConfigurationSettings.AppSettings.Get("LoggingLevel"), out levelOfEntryToLog))
            {
                levelOfEntryToLog = 4;
            }

            string logTarget = ConfigurationSettings.AppSettings.Get("LoggingLocation") == null ? "text" : ConfigurationSettings.AppSettings.Get("LoggingLocation");

            if ((int)LogLevel >= levelOfEntryToLog)
            {
                if (logTarget.ToLower() == "text")
                {
                    string logPath = @"C:\inetpub";
                    using (StreamWriter sw = File.AppendText($"{logPath}\\log.txt"))
                    {
                        sw.WriteLine($"DateTime: {DateTime.Now.ToLocalTime()}");
                        sw.WriteLine($"Message: {Message}");
                        sw.WriteLine($"Content: {Content}");
                        sw.WriteLine($"Exception: {ExceptionText}");
                        sw.WriteLine($"Data: {Data}");
                        sw.WriteLine("--------------------------------------------------------------");
                    }
                }
            }
        }
    }

}
