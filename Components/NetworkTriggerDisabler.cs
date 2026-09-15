using HarmonyLib;
using UnityEngine;
using UnityEngine.XR;

namespace DisableNetworkTriggers.Components
{
    public static class ButtonManager
    {
        private static bool lastLeftStick;

        public static void Update()
        {
            bool pressed = IsLeftStickPressed();

            if (pressed && !lastLeftStick)
                Toggle();

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

        private static void Toggle()
        {
            NetworkTriggerPatch.disabled = !NetworkTriggerPatch.disabled;

            Debug.Log(
                $"[DisableableNetworkTriggers] Network triggers " +
                $"{(NetworkTriggerPatch.disabled ? "disabled" : "enabled")}"
            );
        }

        public static void Reset()
        {
            lastLeftStick = false;
        }
    }
}
