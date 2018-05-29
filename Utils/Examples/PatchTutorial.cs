// add the namespaces of classes not found automatically to the top of the file 
// (so you don't have to call the classes by namespace 
// e.g. Harmony.HarmonyPath or Battletech.Weapon, BattleTech.WeaponDef

using BattleTech;
using Harmony;
using System.Reflection;

namespace country.ladyAlekto.VarianceThingie
{
    // DamageVariance is a property, you see that because it it's something like 
    // public int MyProperty {get; set;} In you case only the getter is set. 
    
    // properties have a get_ and set_ method, so to patch the getter prepend get_
    // The harmony Annotation [HarmonyPatch yadda yadda ] is set above a class,
    [HarmonyPatch(typeof(Weapon), "get_DamageVariance")]
    // you can name the class whatever you want, but our convention seems to be
    // {Target_Class}_{Target_Method}_Patch
    public class Weapon_DamageVariance_Patch
    {
        // The actual patching method is either PreFix(), PostFix(), or Transpiler()
        // you can get the instance of the class (the "this." you see in dnxSpy)
        // by adding a {Class} __instance parameter

        // The PostFix allows you to get the result of what the targetted method 
        // returns, by adding a {Returned Type} __result parameter

        // If you want to change either of the parameters, add the ref keyword.
        // This gives you the reference (pointer to memory address) to the 
        // actual object the target Method / Class, otherwise
        // you just get a copy (local variable)
            
        public static void PostFix(Weapon __instance, ref int __result)
        {
            __result = __instance.weaponDef.DamageVariance;
        }

        // This method is called by Harmony on all classes that use a [Harmony] Annotation
        // and is needed to tell Harmony to actually Patch the damn thing :p
        public static void Init()
        {
            // The string is some unique identifier, convention could be
            // yourcountry.username.projectName, same for the namespace
            var harmony = HarmonyInstance.Create("country.ladyAlekto.VarianceThingie");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}