using Harmony;
using System.Reflection;

namespace nl.flukeyfiddler.NotSoPermanentEvasion
{
    public class NotSoPermanentEvasion
    {
        public static void Init()
        {
            var harmony = HarmonyInstance.Create("nl.flukeyfiddler.NotSoPermanentEvasion");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}