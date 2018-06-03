using BattleTech.UI;
using Harmony;
using nl.flukeyfiddler.Utils;
using System;
using System.Reflection;

namespace nl.flukeyfiddler.Examples
{
    [HarmonyPatch(typeof(Type), "MethodName")]
    public class Type_MethodName_Patch
    {
        public static void Postfix(Type __instance, ref Type __result)
        {
        }
    }
    
    // Gives access very high up the ClassTree
    [HarmonyPatch(typeof(MainMenu), "HandleEscapeKeypress")]
    public class MainMenu_HandleEscapeKeyPress_Patch
    {
        public static void Postfix(MainMenu __instance)
        {
        }
    }

    // Patch page 1 description of character creation
    [HarmonyPatch(typeof(SGCharacterCreationBackgroundSelection), "ShowQuestion")]
    public class SGCharacterCreationBackgroundSelection_ShowQuestion_Patch
    {
        private static void Postfix(SGCharacterCreationBackgroundSelection __instance, int idx, int ___questionIdx)
        {
            if(idx == 0)
            {
                SGCharacterCreationBackgroundSelection.BackgroundQuestion backgroundQuestion = __instance.questions[___questionIdx];
                backgroundQuestion.description = "The Aurigan Reach is a small kingdom in the <color=#F79B26FF><b><link=TTT:52>Rimward Periphery</link></color></b>" +
                    ", a region of space that lies at the outskirts of the more densely-colonized <color=#F79B26FF><b><link=TTT:53>Inner Sphere</link></color></b>. " +
                    "It is home to the <color=#F79B26FF><b><link=TTT:54>Aurigan Coalition</link></color></b>, a federation organized around a parliamentary monarchy " +
                    "and ruled by the <color=#F79B26FF><b><link=TTT:55>Arano</link></color></b> family.\r\n\r\nFor three generations, under the rule of House Arano, " +
                    "the Aurigan Coalition has remained a relatively peaceful corner of the Periphery.\r\n\r\nIt is here your story begins...\r\n";
            } 
        }
    }

}