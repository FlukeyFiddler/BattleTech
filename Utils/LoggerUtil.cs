using System;
using System.IO;
using System.Reflection;

namespace nl.flukeyfiddler.bt.Utils
{
    public static class LoggerUtil
    {
        private static readonly string endLine = 
            Environment.NewLine + "---------------------------------------" +
            "--------------------------------------" + Environment.NewLine;

        public static void LogError(LogFilePath LogFilePath, Exception ex, MethodBase caller = null)
        {
            using (StreamWriter writer = new StreamWriter(LogFilePath.path, true))
            {
                writer.WriteLine(getInfoLine());
                writer.WriteLine("Message: " + ex.Message);
                writer.WriteLine("Stacktrace: " + ex.StackTrace);
                writer.WriteLine(endLine);
            }
        }

        public static void LogLine(LogFilePath LogFilePath, string line, MethodBase caller = null)
        {
            using (StreamWriter writer = new StreamWriter(LogFilePath.path, true))
            {
                writer.WriteLine(getInfoLine());
                writer.WriteLine(line);
                writer.WriteLine(endLine);
            }
        }

        public static void LogMinimal(LogFilePath LogFilePath, string line)
        {
            using (StreamWriter writer = new StreamWriter(LogFilePath.path, true))
            {
                writer.WriteLine(line);
            }
        }

        public static void LogBlock(LogFilePath LogFilePath, string[] lines, MethodBase caller = null)
        {
            using (StreamWriter writer = new StreamWriter(LogFilePath.path, true))
            {
                writer.WriteLine(getInfoLine(caller));
                
                foreach(string line in lines)
                {
                    writer.WriteLine(line);
                }

                writer.WriteLine(endLine);
            }
        }

        private static string getInfoLine(MethodBase methodBase = null)
        {
            string date = DateTime.Now.ToString();
            string methodName = (methodBase == null) ? "Not Supplied" :
                methodBase.ReflectedType.Name + "." + methodBase.Name;

            return String.Format("Date: {0}, From: {1}", date, methodName);
        }
    }
}
