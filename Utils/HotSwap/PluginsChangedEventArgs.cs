namespace nl.flukeyfiddler.Utils
{
    using System;
    using System.Collections.Generic;

    public class PluginsChangedEventArgs<T> : EventArgs
    {
        // Last known Exports matching T
        public IEnumerable<T> AvailablePlugins { get; set; }
    }
}