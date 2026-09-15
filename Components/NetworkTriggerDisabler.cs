using HarmonyLib;
using UnityEngine;
using UnityEngine.XR;

namespace DisableNetworkTriggers.Components
{
    public static class ButtonManager
    {
        private static bool lastLeftStick;

        public static void TrySpawnButton()
        {
            bool pressed = IsLeftStickPressed();

            if (pressed && !lastLeftStick)
                ToggleNetworkTriggers();

            lastLeftStick = pressed;
        }

        private static bool IsLeftStickPressed()
        {
            InputDevice leftHand =
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

            return leftHand.isValid &&
                   leftHand.TryGetFeatureValue(
                       CommonUsages.primary2DAxisClick,
                       out bool pressed) &&
                   pressed;
        }

        private static void ToggleNetworkTriggers()
        {
            NetworkTriggerPatch.enabled =
                !NetworkTriggerPatch.enabled;

            Debug.Log(
                $"[DisableableNetworkTriggers] Network triggers " +
                $"{(NetworkTriggerPatch.enabled ? "disabled" : "enabled")}"
            );
        }

        public static void DestroyButton()
        {
            lastLeftStick = false;
        }
    }
}
