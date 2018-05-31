using Harmony;
using System.Reflection;

namespace nl.flukeyfiddler.Utils.Examples
{
    public class MyMod
    {
        public static void Init()
        {

            var harmony = HarmonyInstance.Create("nl.flukeyfiddler.MyMod");

            var original = typeof(PilotRepresentation).GetMethod("PlayPilotVO");
            var genericMethod = original.MakeGenericMethod(new Type[] { typeof(AudioSwitch_dialog_lines_pilots) });
            var transpiler = typeof(PilotRepresentation_PlayPilotVO_Patch).GetMethod("Transpiler");
            harmony.Patch(genericMethod, null, null, new HarmonyMethod(transpiler));

            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
