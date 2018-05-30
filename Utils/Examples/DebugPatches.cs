using Harmony;

namespace nl.flukeyfiddler.bt.Utils.Examples
{
    [HarmonyPatch(typeof(MainMenu), "HandleEscapeKeypress")]
    public class MainMenu_HandleEscapeKeyPress_Patch
    {
        public static void Postfix(MainMenu __instance)
        {
            Logger.LogLine("It works");
        }
    }
}
