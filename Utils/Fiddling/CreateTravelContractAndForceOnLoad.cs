using System;
using System.Collections.Generic;
using System.Text;

namespace nl.flukeyfiddler.bt.Utils.Fiddling
{
    class CreateTravelContractAndForceOnLoad
    {

      [HarmonyPatch(typeof(MainMenu), "EnableLoadButtonIfSaves")]
      public class MainMenu_EnableLoadButtonIfSaves_Patch
      {
          private static void Postfix(ref HBSButton button)
          {
              button.SetState(ButtonState.Disabled);
          }
      }

        [HarmonyPatch(typeof(SimGameState), "Dehydrate")]
        public class SimGameState_Dehydrate_Patch
        {
            public static void Prefix(SimGameState __instance, SimGameSave save, ref SerializableReferenceContainer references)
            {
                Contract selectedContract = __instance.SelectedContract;
                // TODO check we're in simGame, not combat!
                if (selectedContract == null || __instance.BattleTechGame.Combat != null)
                    return;

                LanceConfiguration lastLance = Traverse.Create(__instance).Method("GetLastLance").GetValue<LanceConfiguration>();

                selectedContract.SetLanceConfiguration(lastLance);
                selectedContract.SetCarryOverNegotationValues(true);
                selectedContract.Accept(true);
                selectedContract.Override.disableNegotations = true;

                Contract travelContract = createTravelContract(selectedContract);
                Logger.Minimal("created travelcontract with name: " + travelContract.Name);
                __instance.PrepareBreadcrumb(travelContract);
                // __instance.SetSelectedContract(travelContract, true);
                //references.AddItem(ModSettings.MOD_SAVE_REFERENCECONTAINER_KEY, travelContract);
                //references.AddItem("activeBreadcrumb", travelContract);
                Logger.Minimal("saving selected contract: " + travelContract.GUID);
                //references.AddItemList<Contract>("globalContracts", new List<Contract>() { selectedContract });
                //this.globalContracts = globalReferences.GetItemList<Contract>("globalContracts");

            }

            private static Contract createTravelContract(Contract oldContract)
            {

                // string mapName, string mapPath, string encounterGuid, 
                // ContractType contractType, ContractOverride ovr, GameContext context, 
                // Faction employer, Faction target, 
                // Faction ally, bool isGlobal, int difficulty
                Type[] createTravelContractParamTypes = new Type[] {
                    typeof(string), typeof(string), typeof(string), typeof(ContractType),
                    typeof(ContractOverride), typeof(GameContext), typeof(Faction), typeof(Faction),
                    typeof(Faction), typeof(bool), typeof(int)
                };

                object[] createTravelContractArguments = new object[] {
                    oldContract.mapName, oldContract.mapPath, oldContract.encounterObjectGuid,
                    oldContract.ContractType, oldContract.Override, oldContract.GameContext,
                    oldContract.TeamFactions[CombatTeamFactionGuids.employer], oldContract.TeamFactions[CombatTeamFactionGuids.target], 
                    oldContract.TeamFactions[CombatTeamFactionGuids.targetsAlly], true, oldContract.Difficulty
                };

                Contract contract = Traverse.Create(oldContract.BattleTechGame.Simulation).
                    Method("CreateTravelContract", createTravelContractParamTypes).GetValue<Contract>(createTravelContractArguments);
                return contract;
            }
        }
       
    [HarmonyPatch(typeof(SimGameState), "Rehydrate")]
        public class SimGameState_Rehydrate_Patch
        {
            public static void Postfix(SimGameState __instance, GameInstanceSave gameInstanceSave)
            {

                SimGameSave save = gameInstanceSave.SimGameSave;


                if (!save.GlobalReferences.HasItem(ModSettings.MOD_SAVE_REFERENCECONTAINER_KEY))
                    return;

                Contract selectedContract = save.GlobalReferences.GetItem<Contract>(ModSettings.MOD_SAVE_REFERENCECONTAINER_KEY);
                if (selectedContract == null)
                    return;
               // __instance.SetSelectedContract(selectedContract);

                //Traverse.Create(__instance).Field("activeBreadcrumb").SetValue(selectedContract);
                Logger.Minimal("loaded selected Contract: " + selectedContract.Name);
                Logger.Minimal("selected contract accepted: " + selectedContract.Accepted);
                Logger.Minimal("selected contract can negogiate : " + selectedContract.CanNegotiate);
                Logger.Minimal("selected contract carry over negotiate values : " + selectedContract.CarryOverNegotationValues);
                Logger.Minimal("selected contract cbills: " + selectedContract.PercentageContractValue);
                Logger.Minimal("selected contract salvage: " + selectedContract.PercentageContractSalvage);
                Logger.Minimal("selected contract rep: " +selectedContract.PercentageContractReputation );
                //__instance.PrepareBreadcrumb(selectedContract);
                selectedContract.Accept(true);

            }
        }
        
    [HarmonyPatch(typeof(GameInstance), "Save")]
        public class GameInstance_Save_Patch
        {
            public static void Prefix(GameInstance __instance, SaveReason reason, out Contract __state)
            {
                if (reason != SaveReason.SIM_GAME_CONTRACT_ACCEPTED) {
                    __state = null;
                    return;

                }
                 __state = __instance.Simulation.SelectedContract;
            }

            public static void Postfix(ref GameInstance __instance, SaveReason reason, Contract __state)
            {
                if (reason != SaveReason.SIM_GAME_CONTRACT_ACCEPTED)
                    return;

                SimGameState simGame = __instance.Simulation;
                Contract selectedContract = __state;

                List<ContractData> contractData = new List<ContractData>();

                foreach (ContractData contract in Traverse.Create(simGame).Field("contractBits").GetValue<List<ContractData>>())
                {
                    Logger.Minimal("got me a contractBit");
                    Logger.Minimal("contract name: " + contract.conName);
                    if (selectedContract.Name == contract.conName)
                    {
                        Logger.Minimal("found contract: " + contract.conName);
                        contractData.Add(contract);
                    }
                }

                Traverse.Create(simGame).Field("globalContracts").SetValue(new List<Contract> { selectedContract});
                Traverse.Create(simGame).Field("contractBits").SetValue(contractData);
            }

            [HarmonyPatch(typeof(SimGameState), "Rehydrate")]
            public class SimGameState_Rehydrate_Patch
            {
                public static void Postfix(SimGameState __instance, GameInstanceSave gameInstanceSave)
                {
                    SimGameSave save = gameInstanceSave.SimGameSave;

                    List<string> debugLines = new List<string>();

                    if(save.ActiveContractName != null)
                    {
                        Logger.Minimal("active contract not null");
                        __instance.SetSelectedContract(save.GlobalContracts[0]);
                    }
                }
            }  
        }
    }
}
