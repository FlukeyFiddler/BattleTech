using BattleTech;
using BattleTech.Save.SaveGameStructure;
using BattleTech.UI;
using Harmony;
using HBS;
using System;


namespace nl.flukeyfiddler.bt.Utils.Fiddling
{
    [HarmonyPatch(typeof(MainMenu), "ReceiveButtonPress")]
    public class MainMenu_ReceiveButtonPress_Patch
    {
        public static void Prefix(string button)
        {
            Logger.LogLine("Button received: " + button, MethodInfo.GetCurrentMethod());
        }
    }

    [HarmonyPatch(typeof(MainMenu), "Init")]
    public class MainMenu_Init_Patch
    {
        public static void Postfix(MainMenu __instance)
        {
            // auto continue!
            /*
            SlotModel mostRecentSave = UnityGameInstance.Instance.Game.SaveManager.GameInstanceSaves.MostRecentSave;
            Traverse mainMenuTraverse = Traverse.Create(__instance);
            mainMenuTraverse.Method("BeginResumeSave", new Type[] { typeof(SlotModel) }, new object[] { mostRecentSave }).GetValue();
            */
            if (LazySingletonBehavior<UnityGameInstance>.HasInstance)
            {
                UnityGameInstance.BattleTechGame.MessageCenter.
                    AddFiniteSubscriber(MessageCenterMessageType.OnEnterMainMenu,
                    delegate (MessageCenterMessage message)
                    {
                        MainMenu firstModule = LazySingletonBehavior<UIManager>.Instance.GetFirstModule<MainMenu>();

                        if (firstModule != null)
                        {
                            //Logger.LogMinimal(""+firstModule.Visible);
                            //UnityGameInstance.BattleTechGame.CreateNonCampaignSim(); 
                            //UnityGameInstance.BattleTechGame.CreateSim(false);
                            //UnityGameInstance.BattleTechGame.Simulation.AttachUX();
                            //__instance.Pool(false);
                            //firstModule.ReceiveButtonPress("Continue");

                            Logger.LogLine("in firstModule");
                            SlotModel mostRecentSave = UnityGameInstance.Instance.Game.SaveManager.GameInstanceSaves.MostRecentSave;
                            Logger.LogMinimal("mostrecentSave fetched");
                            Logger.LogMinimal("it is: " + mostRecentSave.CompanyName);
                            Logger.LogMinimal("Just tried to print mostRecentSave");
                            Traverse mainMenuTraverse = Traverse.Create(__instance);
                            mainMenuTraverse.Method("BeginResumeSave", new Type[] { typeof(SlotModel) }, new object[] { mostRecentSave }).GetValue();
                            Logger.LogMinimal("tried continue from __instance");
                            //Traverse.Create(firstModule).Method("BeginResumeSave", new Type[] { typeof(SlotModel) }, new object[] { mostRecentSave }).GetValue();
                            //Logger.LogMinimal("tried continue from firstModule");
                            //firstModule.Pool(false);

                        }
                        return true;
                    });
            }
        }
    }
}
