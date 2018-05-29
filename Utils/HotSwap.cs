using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace nl.flukeyfiddler.Utils
{
    // code based on https://github.com/i-e-b/MefExperiments
    public class HotSwap
    {
        private static readonly object VersionLock = new object();
        static IHotSwapInvoke current;

        private static readonly string modDirectory = "HotSwap";

        public HotSwap()
        {
            Directory.CreateDirectory(modDirectory);

            Logger.LogLine("Watching directory " + modDirectory + " for changes," +
                "press esc to exit");

            IPluginWatcher<IHotSwapInvoke> watcher = new PluginWatcher<IHotSwapInvoke>(modDirectory);
            watcher.PluginsChanged += Watcher_PluginsChanged;

            lock (VersionLock)
            {
                try
                {
                    current = Latest(watcher.CurrentlyAvailable);

                    Logger.LogLine("\r\nInitial startup\r\nLatest version:");
                    Logger.LogLine(current.Version().ToString());

                    // Here we set some state that should be maintained between versions
                    current.OnHotSwapInvoke();
                }
                catch (Exception ex)
                {
                    Logger.LogLine(ex.Message);                 
                }
            }
        }

        static void Watcher_PluginsChanged(object sender, PluginsChangedEventArgs<IHotSwapInvoke> e)
        {
            var latest = Latest(e.AvailablePlugins);
            lock (VersionLock)
            {
                if (latest.Version() > current.Version()) Upgrade(current, latest);


                Logger.LogLine("New version:");
                Logger.LogLine(current.Version().ToString());
                
            }
        }

        static IHotSwapInvoke Latest(IEnumerable<IHotSwapInvoke> currentlyAvailable)
        {
            var list = currentlyAvailable.ToList();
            if (!list.Any()) throw new Exception("No tasks available");
            return list.OrderBy(task => task.Version()).Last();
        }

        static void Upgrade(IHotSwap old, IHotSwapInvoke latest)
        {
            lock (VersionLock)
            {
                try
                {
                    latest.SetState(old.GetState());
                    current = latest;
                }
                catch (Exception ex)
                {
                    Logger.LogLine("Can't upgrade: " + ex.Message);
                }
            }
        }
    }
}