using BattleTech;
using Harmony;
using System;
using System.Collections.Generic;
using System.Text;

namespace nl.flukeyfiddler.bt.Utils.Examples
{
    class SubscribeToConfigurableAmmountOfMessages
    {
        public static List<MessageCenterMessageType> messageCenterAutosaveHooks =
                new List<MessageCenterMessageType>(){
                MessageCenterMessageType.OnSimStarSystemChanged,
                MessageCenterMessageType.OnSimGameMechWarriorPersonnelChange,
                MessageCenterMessageType.OnSimGameFundsChange,
        };
    }

    [HarmonyPatch(typeof(SimGameState), "_OnInit")]
    public class SimGameState_OnInit_Patch
    {
        private static void Postfix(SimGameState __instance)
        {
            foreach (MessageCenterMessageType messageType in SubscribeToConfigurableAmmountOfMessages.messageCenterAutosaveHooks)
            {
                __instance.MessageCenter.AddSubscriber(messageType,
                    new ReceiveMessageCenterMessage(HandleMessageCenterAutosave));
            }
        }

        private static void HandleMessageCenterAutosave(MessageCenterMessage message)
        {
            // Cewl code here
        }
    }
}
