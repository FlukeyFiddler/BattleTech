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
 }