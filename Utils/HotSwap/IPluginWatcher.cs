namespace nl.flukeyfiddler.Utils
{
    using System;
    using System.Collections.Generic;

    // Watch changes for a specific MEF Import type
    public interface IPluginWatcher<T> : IDisposable
    {
       event EventHandler<PluginsChangedEventArgs<T>> PluginsChanged;

       IEnumerable<T> CurrentlyAvailable { get; }
    }
}