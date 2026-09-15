using GorillaNetworking;
using HarmonyLib;

namespace DisableNetworkTriggers
{
    [HarmonyPatch(typeof(GorillaNetworkJoinTrigger), nameof(GorillaNetworkJoinTrigger.OnBoxTriggered))]
    public static class NetworkTriggerPatch
    {
        public static bool disabled;

        static bool Prefix() => !disabled;
    }
}
