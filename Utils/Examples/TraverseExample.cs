using BattleTech;
using BattleTech.Save.SaveGameStructure;
using Harmony;
using System;
using System.Collections.Generic;
using System.Text;

namespace nl.flukeyfiddler.bt.Utils.Examples
{
    class TraverseExample
    {
        static void Example()
        {
            

            Traverse.Create(simGameInstance).Method("TriggerSaveNow", new object[] {
                typeof(SaveReason), typeof(bool)}).
                GetValue(SaveReason.SIM_GAME_EVENT_FIRED, true);
            
        }
    }
}
