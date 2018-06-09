using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace nl.flukeyfiddler.bt.Utils.Logger
{
    public static class LoggerUtil
    {
        private static readonly string endLine = 
            Environment.NewLine + "---------------------------------------" +
            "--------------------------------------" + Environment.NewLine;

        public static void Error(LogFilePath logFilePath, Exception ex, MethodBase caller = null)
        {
            StreamWriter writer = newStreamWriter(logFilePath);
            writer.WriteLine(getInfoLineString(caller));
            writer.WriteLine("Message: " + ex.Message);
            writer.WriteLine("Stacktrace: " + ex.StackTrace);
            writer.WriteLine(endLine);
        }

        public static void Line(LogFilePath logFilePath, string line, MethodBase caller = null)
        {
            StreamWriter writer = newStreamWriter(logFilePath);
            writer.WriteLine(getInfoLineString(caller));
            writer.WriteLine(line);
            writer.WriteLine(endLine);

        }

        public static void InfoLine(LogFilePath logFilePath, MethodBase caller = null)
        {
            StreamWriter writer = newStreamWriter(logFilePath);
            writer.WriteLine(getInfoLineString(caller));
        }

        public static void Minimal(LogFilePath logFilePath, string line)
        {
            StreamWriter writer = newStreamWriter(logFilePath);
            writer.WriteLine(line);
        }

        public static void EndLine(LogFilePath logFilePath)
        {
            StreamWriter writer = newStreamWriter(logFilePath);
            writer.WriteLine(endLine);
        }

        public static void Block(LogFilePath logFilePath, string[] lines, MethodBase caller = null)
        {
            StreamWriter writer = newStreamWriter(logFilePath);
            writer.WriteLine(getInfoLineString(caller));
                
            foreach(string line in lines)
            {
                writer.WriteLine(line);
            }

            writer.WriteLine(endLine);
        }

        public static void GameStarted(LogFilePath logFilePath)
        {
            StreamWriter writer = newStreamWriter(logFilePath);
            for (int i = 0; i <= 3; i++)
                {
                    writer.WriteLine(Regex.Replace(endLine, Environment.NewLine, ""));
                }
                writer.WriteLine(DateTime.Now.ToString() + ": Game started");
                writer.WriteLine(endLine);
        }

        private static StreamWriter newStreamWriter(LogFilePath logFilePath, bool append = true)
        {
            return new StreamWriter(logFilePath.path);
        }

        private static string getInfoLineString(MethodBase methodBase = null)
        {
            string date = DateTime.Now.ToString();
            string methodName = (methodBase == null) ? "Not Supplied" :
                methodBase.ReflectedType.Name + "." + methodBase.Name;

            return String.Format("Date: {0}, From: {1}", date, methodName);
        }
    }
}
