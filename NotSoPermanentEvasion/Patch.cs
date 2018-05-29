using BattleTech.UI;
using Harmony;
using nl.flukeyfiddler.Utils;

namespace nl.flukeyfiddler.NotSoPermanentEvasion
{
    [HarmonyPatch(typeof(MainMenu), "Init")]
    public class MainMenu_Init_Patch
    {
        public static void Postfix(MainMenu __instance)
        {
            Logger.LogLine("Main Menu init");
            FileLog.Log("Main Menu init");
        }
    }
    
    [HarmonyPatch(typeof(MainMenu), "HandleEscapeKeypress")]
    public class MainMenu_HandleEscapeKeyPress_Patch
    {
        public static void Postfix(MainMenu __instance)
        {
        }
    }
    
}