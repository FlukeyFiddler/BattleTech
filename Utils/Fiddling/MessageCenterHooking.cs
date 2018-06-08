using BattleTech;
using BattleTech.Save.SaveGameStructure;
using Harmony;
using nl.flukeyfiddler.bt.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace nl.flukeyfiddler.bt.Utils.Fiddling
{
    [HarmonyPatch(typeof(CombatGameState), "_Init")]
    public class CombatGameState_Init_Patch
    {
        private static CombatGameState combatGameState;

        private static void Postfix(CombatGameState __instance)
        {
            combatGameState = __instance;

            Logger.Line("TODO: test that this subscriber does not need to be destroyed on " +
                "OnCombatGameDestroyed", MethodBase.GetCurrentMethod());

            // Alas MessageCenterMessageType.OnRoundBeginComplete doesn't seem to be implemented
            // Grr neither OnPhaseBeginComplete
            __instance.MessageCenter.AddSubscriber(MessageCenterMessageType.OnTurnActorActivate,
                new ReceiveMessageCenterMessage(OnTurnActorActivateMessage));
        }
        //  new ReceiveMessageCenterMessage(OnTurnActorActivateMessage)
        //  PhaseBeginCompleteMessage(int round, int phase, string TurnActorGUID, int available)
        public static void OnTurnActorActivateMessage(MessageCenterMessage message)
        {
            TurnActorActivateMessage turnActorActivateMessage = message as TurnActorActivateMessage;

            if (turnActorActivateMessage.TurnActorGUID == combatGameState.LocalPlayerTeamGuid)
            {
                combatGameState.MessageCenter.AddFiniteSubscriber(MessageCenterMessageType.SequenceCompleteMessage, new TeamActivationSequence)
                combatGameState.BattleTechGame.Save(SaveReason.COMBAT_GAME_DESIGNER_TRIGGER, false);
                //  UnityGameInstance.BattleTechGame.Save(SaveReason.COMBAT_GAME_DESIGNER_TRIGGER, false);
            }
        }
    }
}
