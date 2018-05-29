using Harmony;
using System.Reflection;

namespace nl.flukeyfiddler.Utils.Examples
{
    public class MyMod
    {
        public static void Init()
        {
            var harmony = HarmonyInstance.Create("nl.flukeyfiddler.MyMod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}