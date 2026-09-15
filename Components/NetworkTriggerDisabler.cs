using GorillaNetworking;
using HarmonyLib;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace DisableNetworkTriggers.Components
{
    public static class ButtonManager
    {
        private static bool lastLeftStick;
        private static bool steamVR;
        private static bool initialized;

        public static void Update()
        {
            if (!initialized)
                DetectPlatform();

            bool pressed = IsLeftStickPressed();

            if (pressed && !lastLeftStick)
                Toggle();

            lastLeftStick = pressed;
        }

        private static void DetectPlatform()
        {
            try
            {
                string platform = Traverse
                    .Create(PlayFabAuthenticator.instance)
                    .Field("platform")
                    .GetValue()
                    ?.ToString();

                if (string.IsNullOrEmpty(platform))
                    return;

                steamVR = platform.ToLowerInvariant().Contains("steam");
                initialized = true;

                Debug.Log(
                    $"[DisableableNetworkTriggers] Platform: {platform}"
                );
            }
            catch (System.Exception e)
            {
                Debug.LogError(
                    $"[DisableableNetworkTriggers] Platform detection failed: {e}"
                );
            }
        }

        private static bool IsLeftStickPressed()
        {
            if (steamVR)
            {
                return SteamVR_Actions.gorillaTag_LeftJoystickClick
                    .GetState(SteamVR_Input_Sources.LeftHand);
            }

            InputDevice leftHand =
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

            return leftHand.isValid &&
                   leftHand.TryGetFeatureValue(
                       CommonUsages.primary2DAxisClick,
                       out bool pressed) &&
                   pressed;
        }

        private static void Toggle()
        {
            NetworkTriggerPatch.enabled =
                !NetworkTriggerPatch.enabled;

            Debug.Log(
                $"[DisableableNetworkTriggers] Network triggers " +
                $"{(NetworkTriggerPatch.enabled ? "disabled" : "enabled")}"
            );
        }

        public static void Reset()
        {
            lastLeftStick = false;
            initialized = false;
        }
    }
}
