using System;
using System.Collections.Generic;
using System.Text;

// Calling method should StaticClassWithSettableField.SettableField.settableField = "new value";

namespace nl.flukeyfiddler.bt.Utils.Examples
{
    public static class StaticClassWithSettableField
    {
        internal static SettableField SettableField = new SettableField();

        public static string WhatsTheValue()
        {
            return SettableField;
        }

        internal class SettableField
        {
            public string settableField = "default value";
        }
    }
}