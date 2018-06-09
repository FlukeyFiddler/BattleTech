namespace nl.flukeyfiddler.bt.Utils.Logger
{
    public class LogFilePath
    {
        private readonly string defaultPath = "mods\\FlukeyFiddlerUnsetLog.txt";
        public string path { get; set; }

        public LogFilePath(string path = null)
        {
            this.path = path ?? defaultPath;
        }
    }
}