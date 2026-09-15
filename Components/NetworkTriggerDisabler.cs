using GorillaNetworking;
using HarmonyLib;
using DisableNetworkTriggers;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace DisableNetworkTriggers.Components
{
    public static class ButtonManager
    {
        private static bool _lastLeftStick;
        private static bool _isSteamVR;

        private static bool _initialized;

        public static void TrySpawnButton()
        {
            InitializePlatform();

            bool leftStick = IsLeftStickPressed();

            if (leftStick && !_lastLeftStick)
            {
                ToggleNetworkTriggers();
            }

            _lastLeftStick = leftStick;
        }

        private static void InitializePlatform()
        {
            if (_initialized)
                return;

            try
            {
                string? platform = Traverse
                    .Create(PlayFabAuthenticator.instance)
                    .Field("platform")
                    .GetValue()
                    ?.ToString();

                if (!string.IsNullOrEmpty(platform))
                {
                    _isSteamVR =
                        platform.ToLower().Contains("steam");

                    Debug.Log(
                        $"[DisableableNetworkTriggers] " +
                        $"Detected platform: {platform} " +
                        $"(SteamVR: {_isSteamVR})"
                    );

                    _initialized = true;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(
                    $"[DisableableNetworkTriggers] " +
                    $"Failed to detect platform: {e}"
                );
            }
        }

        private static bool IsLeftStickPressed()
        {
            if (_isSteamVR)
            {
                return SteamVR_Actions
                    .gorillaTag_LeftJoystickClick
                    .GetState(SteamVR_Input_Sources.LeftHand);
            }

            InputDevice device =
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

            if (device.isValid &&
                device.TryGetFeatureValue(
                    CommonUsages.primary2DAxisClick,
                    out bool pressed))
            {
                return pressed;
            }

            return false;
        }

        private static void ToggleNetworkTriggers()
        {
            NetworkTriggerPatch.enabled = !NetworkTriggerPatch.enabled;

            Debug.Log(
                $"[DisableableNetworkTriggers] " +
                $"Network Triggers Disabled: " +
                $"{NetworkTriggerPatch.enabled}"
            );
        }

        public static void DestroyButton()
        {
            _lastLeftStick = false;
            _initialized = false;
        }
    }
}