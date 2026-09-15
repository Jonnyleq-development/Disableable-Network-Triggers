# Gorilla Tag - Disable Network Triggers Mod

A Gorilla Tag BepInEx mod that adds an interactable toggle button in the Stump next to the small stone to enable or disable network join triggers (`GorillaNetworkJoinTrigger.OnBoxTriggered`).

## Features

- **Harmony Patch**: Patches `GorillaNetworkJoinTrigger.OnBoxTriggered` to skip trigger execution when disabled.
- **In-Game Toggle Button**: Spawns an interactable 3D button next to the small stone in the Stump with visual color status (Green = Triggers Enabled, Red = Triggers Disabled), label text, audio feedback, and haptics.

## Project Structure

- `Patches/NetworkTriggerPatch.cs` — Harmony patch targeting `GorillaNetworkJoinTrigger.OnBoxTriggered`.
- `Components/NetworkTriggerButton.cs` — Interactive button component handling hand collision, debounce, and state updates.
- `Components/ButtonManager.cs` — Locates the small stone in the Stump and spawns the 3D button.
- `Plugin.cs` — Plugin entry point.
- `PluginInfo.cs` — Mod GUID and version information.

## Getting Started

1. **Configure Game Path**: Open `DisableableNetworkTriggers.csproj` and verify that `<GorillaTagPath>` points to your Gorilla Tag installation directory (e.g., `C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag`).
2. **Build the Mod**:
   ```bash
   dotnet build
   ```
3. **Installation**:
   - The build target will automatically copy the resulting DLL (`DisableableNetworkTriggers.dll`) to your `BepInEx/plugins/` folder if the path exists.
   - Alternatively, copy the generated `.dll` from `bin/Debug/netstandard2.1/DisableableNetworkTriggers.dll` into `Gorilla Tag/BepInEx/plugins/`.
