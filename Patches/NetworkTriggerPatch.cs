using GorillaNetworking;
using HarmonyLib;

namespace DisableNetworkTriggers
{
    [HarmonyPatch(typeof(GorillaNetworkJoinTrigger), nameof(GorillaNetworkJoinTrigger.OnBoxTriggered))]
    public class NetworkTriggerPatch
    {
        public static bool enabled;

        public static bool Prefix() =>
            !enabled;
    }
}