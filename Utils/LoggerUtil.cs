using System;
using System.IO;

namespace nl.flukeyfiddler.bt.Utils
{
    public static class LoggerUtil
    {
        
        public static void LogError(LogFilePath LogFilePath, Exception ex)
        {
            using (StreamWriter writer = new StreamWriter(LogFilePath.path, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }

        public static void LogLine(LogFilePath LogFilePath, string line)
        {
            using (StreamWriter writer = new StreamWriter(LogFilePath.path, true))
            {
                writer.WriteLine(line + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }

        public static void LogMinimal(LogFilePath LogFilePath, string line)
        {
            using (StreamWriter writer = new StreamWriter(LogFilePath.path, true))
            {
                writer.WriteLine(line + Environment.NewLine);
            }
        }
    }
}
