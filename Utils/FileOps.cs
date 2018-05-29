using System;
using System.IO;

namespace nl.flukeyfiddler.Utils
{
    public static class FileOps
    {
        public static string GetFileAsString(string filePath) 
        {
            try
            {
                return File.ReadAllText(filePath);
            } catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
    }
}
