using System.Collections.Generic;

namespace nl.flukeyfiddler.bt.Utils
{
    public static class CombatTeamFactionGuids
    {
        public static readonly string player = "bf40fd39-ccf9-47c4-94a6-061809681140";
        public static readonly string employer = "ecc8d4f2-74b4-465d-adf6-84445e5dfc230";
        public static readonly string target = "be77cadd-e245-4240-a93e-b99cc98902a5";
        public static readonly string targetsAlly = "31151ed6-cfc2-467e-98c4-9ae5bea784cf";

        public static Dictionary<string, string> FactionGUIDToName = new Dictionary<string, string>
        {
            { player, "player" },
            { employer, "employer" },
            { target, "target" },
            { targetsAlly, "targetsAlly" },
        };
    }
}
