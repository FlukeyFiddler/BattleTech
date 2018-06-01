using BattleTech;
using BattleTech.DataObjects;
using BattleTech.Save;
using BattleTech.Save.SaveGameStructure;
using BattleTech.Serialization;
using BattleTech.Serialization.Handlers;
using BattleTech.Serialization.Models;
using BattleTech.Serialization.Utility;
using BattleTech.UI;
using Harmony;
using nl.flukeyfiddler.bt.IronMechMode.Util;
using System;
using System.Collections.Generic;

namespace nl.flukeyfiddler.bt.Utils.Fiddling
{
    /*
   [HarmonyPatch(typeof(Serializer), "Deserialize", new Type[] { typeof(Enum)})]
   public static class Test_Adding_To_Enum
   {
       public static void Prefix(byte[] data, Enum instanceType, SerializationTarget target, TargetMaskOperation maskOperation)
       {
           Logger.LogLine("in Deserializer with Type as param");
           Logger.LogLine(instanceType.ToString());
           Logger.LogLine(target.GetType().ToString());
       }

   }
   */

    [HarmonyPatch(typeof(EnumSerializationHandler), "Deserialize")]
    public static class Test_Adding_To_Enum_Typed
    {
        public static void Postfix(EnumSerializationHandler __instance,
            ProtobufSerialization serializer, DataContractFieldPair contractField,
            object instance, object fieldValue, Dictionary<uint, object> deserializedReferences,
            ClassTypeResolver ___classResolver)
        {

            if (((EnumMember)fieldValue).EnumValue == "SIM_GAME_EVENT_RESOLVED"
                && contractField.MemberName == "saveReason")
            {
                Logger.LogLine("here goes nothing ...");
                EnumMember newFieldValue = new EnumMember();
                newFieldValue.EnumType = ((EnumMember)fieldValue).EnumType;
                newFieldValue.EnumValue = "IRON_MECH_MOD";
                /*
                __instance.Serialize(serializer, contractField, instance,
                    newFieldValue, contractField.);

                __instance.Deserialize(serializer, contractField, instance,
                    newFieldValue, deserializedReferences);
                Logger.LogLine("All done");
                */
            }
        }
    }
    /*
           [SerializableEnum("GameInstanceSave.SaveReason")]
           public enum SaveReason
           {
               // Token: 0x04005EC5 RID: 24261
               MANUAL = 1,
               // Token: 0x04005EC6 RID: 24262
               COMBAT_GAME_DESIGNER_TRIGGER,
               // Token: 0x04005EC7 RID: 24263
               SIM_GAME_QUARTERLY_REPORT,
               // Token: 0x04005EC8 RID: 24264
               SIM_GAME_EVENT_FIRED,
               // Token: 0x04005EC9 RID: 24265
               SIM_GAME_EVENT_OPTION_SELECTED,
               // Token: 0x04005ECA RID: 24266
               SIM_GAME_CONTRACT_ACCEPTED,
               // Token: 0x04005ECB RID: 24267
               SIM_GAME_BREADCRUMB_COMPLETE = 12,
               // Token: 0x04005ECC RID: 24268
               SIM_GAME_EVENT_RESOLVED = 7,
               // Token: 0x04005ECD RID: 24269
               SIM_GAME_FIRST_SAVE = 9,
               // Token: 0x04005ECE RID: 24270
               SIM_GAME_ARRIVED_AT_PLANET,
               // Token: 0x04005ECF RID: 24271
               SIM_GAME_COMPLETED_CONTRACT,
               // Token: 0x04005ED0 RID: 24272
               COMBAT_SIM_STORY_MISSION_RESTART = 8
           }
           */
}
