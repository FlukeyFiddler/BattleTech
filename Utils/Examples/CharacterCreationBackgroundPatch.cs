using BattleTech.UI;
using Harmony;

namespace nl.flukeyfiddler.bt.Utils.Examples
{
    [HarmonyPatch(typeof(SGCharacterCreationBackgroundSelection), "ShowQuestion")]
    public class SGCharacterCreationBackgroundSelection_ShowQuestion_Patch
    {
        private static void Prefix(SGCharacterCreationBackgroundSelection __instance, int idx, int ___questionIdx)
        {
            SGCharacterCreationBackgroundSelection.BackgroundQuestion backgroundQuestion = __instance.questions[___questionIdx];

            switch (idx)
            {
                case 0: // Flavourtext 1
                    backgroundQuestion.premise = "EDIT Welcome to the Aurigan Reach.";

                    backgroundQuestion.description = "EDIT The Aurigan Reach is a small kingdom in the [[DM.BaseDescriptionDefs[LoreRimwardPeriphery],Rimward Periphery]]," +
                    " a region of space that lies at the outskirts of the more densely-colonized [[DM.BaseDescriptionDefs[LoreInnerSphere],Inner Sphere]]. It is home to the " +
                    "[[DM.BaseDescriptionDefs[LoreAuriganCoalition],Aurigan Coalition]], a federation organized around a parliamentary monarchy and ruled by the " +
                    "[[DM.BaseDescriptionDefs[LoreHouseArano],Arano]] family.\r\n\r\nFor three generations, under the rule of House Arano, the Aurigan Coalition has " +
                    "remained a relatively peaceful corner of the Periphery.\r\n\r\nIt is here your story begins...";
                    break;
                case 1: // Question 1
                    backgroundQuestion.premise = "EDIT Decades ago, your family came to the Reach from…";
                    break;
                case 2: // Flavourtext 2
                    backgroundQuestion.premise = "EDIT You are of noble birth.";

                    backgroundQuestion.description = "EDIT Though immigrants to the [[DM.BaseDescriptionDefs[LoreAuriganReach],Aurigan Reach]], your family soon established a " +
                        "comfortable presence in a small, backwater system on the edge of Aurigan space. By the time you were born, your family had become the de-facto ruling " +
                        "nobility of the system’s only inhabited planet. You were the oldest child, heir to the family’s titles and ancestral [[DM.BaseDescriptionDefs[LoreBattleMech]," +
                        "BattleMech]], an old [[DM.BaseDescriptionDefs[LoreAncestralBlackjack],Blackjack BJ-1]].\r\n\r\nThis is where you met[[DM.BaseDescriptionDefs[LoreMastiff], " +
                        "Raju \"Mastiff\" Montgomery]], a veteran of the[[DM.BaseDescriptionDefs[LoreSuccessionWars],Succession Wars]], whom your parents hired on for a season to " +
                        "train you as a[[DM.BaseDescriptionDefs[LoreMechWarrior], MechWarrior]].\r\n\r\nRaju was a strict but capable teacher, and you quickly became a skilled " +
                        "pilot under his tutelage.\r\n\r\nIt was an uneventful life...";
                    break;
                case 3: // Question 2
                    backgroundQuestion.premise = "EDIT Until the day after your sixteenth birthday, when...";
                    break;
                case 4: // Question 3
                    backgroundQuestion.premise = "EDIT Out on your own, you fell into the life of a...";
                    break;
                case 5: // Flavourtext 3
                    backgroundQuestion.premise = "EDIT Until years later, you crossed paths with Raju \"Mastiff\" Montgomery once again.";

                    backgroundQuestion.description = "EDIT On an ill-fated mission, you were caught behind enemy lines and separated from your lance. Raju happened to be on patrol nearby, " +
                        "and he launched a daring rescue. Upon finding out it was you he had rescued, he brought you into the [[DM.BaseDescriptionDefs[LoreHouseArano],House Arano]] Royal Guard." +
                        "\r\n\r\nSo it is that you find yourself reunited with your old mentor, preparing your ancestral [[DM.BaseDescriptionDefs[LoreAncestralBlackjack],Blackjack]] for guard duty " +
                        "on the coronation day of the [[DM.BaseDescriptionDefs[LoreKameaArano],Lady Kamea Arano]]…";
                    break;
            }
        }
    }
}
