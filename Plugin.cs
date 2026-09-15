using System.Reflection;
using BepInEx;
using DisableNetworkTriggers.Components;
using HarmonyLib;
using UnityEngine;

namespace DisableNetworkTriggers
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin? Instance { get; private set; }

        private Harmony? _harmony;

        private void Awake()
        {
            Instance = this;

            Logger.LogInfo($"{PluginInfo.Name} is initializing...");

            ApplyHarmonyPatches();

            Logger.LogInfo(
                $"{PluginInfo.Name} v{PluginInfo.Version} loaded successfully."
            );
        }

        private void OnEnable()
        {
            ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            RemoveHarmonyPatches();
            ButtonManager.DestroyButton();

            Instance = null;
        }

        private void OnDestroy()
        {
            RemoveHarmonyPatches();
            ButtonManager.DestroyButton();

            Instance = null;
        }

        private void Update()
        {
            ButtonManager.TrySpawnButton();
        }

        private void ApplyHarmonyPatches()
        {
            if (_harmony != null)
                return;

            _harmony = new Harmony(PluginInfo.GUID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());

            Logger.LogInfo("Harmony patches applied.");
        }

        private void RemoveHarmonyPatches()
        {
            if (_harmony == null)
                return;

            _harmony.UnpatchSelf();
            _harmony = null;

            Logger.LogInfo("Harmony patches removed.");
        }
    }
}