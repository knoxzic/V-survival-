# V-survival-

Sandbox survival prototype focused on first-person combat, resource gathering, base prep, and night pressure waves.

## Project Layout

```text
Project 3/
├── Code/
│   ├── Core/              # Player, flow control, pickup/drop, day-night
│   ├── Weapons/           # Shooting, weapon manager, ammo pools
│   ├── Inventory/         # Inventory system, item definitions, slots
│   ├── PlayerStats/       # Health, stamina, hunger, thirst
│   ├── AI/                # Wandering AI + harvest flow
│   ├── UI/                # UI view models
│   ├── Building/          # Reserved for fort building system
│   └── Utils/             # Snap helpers and reusable tools
├── Prefabs/
│   ├── Weapons/
│   ├── Items/
│   ├── Effects/
│   └── Enemies/
├── Scenes/
│   ├── MainGame.scene
│   └── Test_Weapon.scene
├── UI/
│   ├── Hud.razor
│   └── InventoryPanel.razor
└── Assets/
	├── Models/
	├── Sounds/
	├── Materials/
	└── Particles/
```

## Implemented Starter Systems

### 1.6 Overall ammunition with 3 ammo types
- `Code/Weapons/AmmoType.cs`
- `Code/Weapons/AmmoPool.cs`

The ammo pool tracks Light, Heavy, and Shell ammo and exposes change events for UI updates.

### 1.7 Two weapon slots + separate inventory resources
- `Code/Inventory/InventorySystem.cs`
- `Code/Weapons/WeaponManager.cs`

Weapons are restricted to two dedicated slots (primary and secondary). Non-weapon resources use a separate 24-slot inventory.

### 2 Pick/Drop item components
- `Code/Core/PickupDropController.cs`

This wraps pickup and drop behavior so game input can call a single point for resource or weapon drop actions.

### 3, 4, 5 Health/Stamina/Hunger/Thirst bars
- `Code/PlayerStats/PlayerVitals.cs`
- `UI/Hud.razor`
- `Code/UI/HudViewModel.cs`

Vitals tick down over time and are rendered in the HUD as progress bars for first-person play.

### 5 Wandering AI that can be killed and harvested
- `Code/AI/WanderingAI.cs`

AI has wander target generation, damage handling, death state, and basic harvest yield.

### 6 Rust-like inventory UI (character left, 24 slots right)
- `UI/InventoryPanel.razor`
- `Code/UI/InventoryViewModel.cs`

The UI provides a split layout with character/weapon section on the left and a 24-cell grid on the right.

### 7 Editor-friendly snapping component with `[Property]`
- `Code/Utils/SnapToGridComponent.cs`

Includes configurable grid size and optional Y-axis snapping to make placement/building easier.

### 9 Day and night cycle
- `Code/Core/DayNightCycle.cs`

Supports configurable day length and `IsNight` checks for triggering wave events.

### Nightly raid-wave spawner tied to day/night
- `Code/AI/NightlyWaveSpawner.cs`
- `Code/Core/PlayerController.cs`

The player tick now drives a nightly wave spawner that starts when night begins, requests waves at intervals, and increases pressure over nights.

### Engine path for muzzle flash + shoot/reload sound
- `Code/Weapons/WeaponEffectsBinder.cs`
- `Code/Weapons/EngineWeaponEffectsPlayer.cs`
- `Code/Core/PlayerController.cs`

Weapon events are now bridged into a single effects interface so engine-specific audio/particle playback can be plugged in cleanly.

### Drag/drop + split-stack inventory interactions
- `Code/Inventory/InventorySystem.cs`
- `Code/UI/InventoryViewModel.cs`
- `UI/InventoryPanel.razor`

Resource inventory supports move, swap, merge, and split-half operations for Rust-style interaction patterns.

## Raid-Wave Logic Explained

The raid system has three phases every day:

1. Day phase:
	- `DayNightCycle.IsNight` is false.
	- `NightlyWaveSpawner` is idle and does not request new waves.

2. Night start transition:
	- When the cycle flips from day to night, `CurrentNightIndex` increments.
	- `SpawnedWavesThisNight` resets to 0.
	- `OnNightStarted` fires for setup logic (sirens, music, prep timers).

3. Night wave loop:
	- A timer counts down using `SecondsBetweenWaves`.
	- Every interval, `OnWaveSpawnRequested` fires with `WaveSpawnData`:
	  - `Night`: current night number
	  - `Wave`: wave number for this night
	  - `DifficultyTier`: scales every two nights
	  - `EnemyCount`: increases by night + wave index
	- Once `MaxWavesPerNight` is reached, spawning pauses until the next night.

Night end:
- On transition back to day, `OnNightEnded` fires.

This pattern keeps scheduling deterministic while leaving actual prefab spawn choices to engine-specific code.

## Linking Components Correctly (Beginner CS + Razor)

### C# runtime linkage
1. Create `PlayerController` at scene startup.
2. Call `Tick(deltaSeconds, isSprinting)` each frame.
3. For shooting input, call `WeaponManager.TryShoot()` and `WeaponManager.TryReload()`.
4. Implement `IWeaponEffectsPlayer` in your engine layer and call `AttachWeaponEffects(...)`.
5. Subscribe to `RaidWaves.OnWaveSpawnRequested` and spawn enemies from `Prefabs/Enemies`.

### Razor linkage
1. Feed HUD values from `HudViewModel` into `Hud.razor` parameters.
2. Feed inventory arrays from `InventoryViewModel` into `InventoryPanel.razor`.
3. Wire `InventoryPanel.razor` callbacks:
	- `OnMoveStack` -> `InventoryViewModel.MoveResourceStack`
	- `OnSplitStackHalf` -> `InventoryViewModel.SplitResourceStackHalf`
4. Re-render on inventory and vitals change events.

## Folder-by-Folder Beginner Instructions

Each actively worked folder now contains its own guide:
- `Code/Core/README.md`
- `Code/Weapons/README.md`
- `Code/Inventory/README.md`
- `Code/PlayerStats/README.md`
- `Code/AI/README.md`
- `Code/UI/README.md`
- `UI/README.md`
- `Prefabs/README.md`
- `Scenes/README.md`
- `Assets/README.md`

## Razor UI Concepts (Practical Guide)

Razor components combine markup and C# into one file.

### Component structure
Each component has:
- HTML-like markup (what is drawn)
- `@code` block (state/logic)
- optional parameters with `[Parameter]`

Example from `UI/Hud.razor`:
- `WeaponId`, `AmmoInMagazine`, `Health`, etc. are input parameters
- the HUD renders values and progress bars from those parameters

### Data flow
Recommended flow for game UI:
1. Gameplay systems update model state (`PlayerVitals`, `AmmoPool`, inventory)
2. View models adapt gameplay data for UI (`HudViewModel`, `InventoryViewModel`)
3. Razor components render final values (`Hud.razor`, `InventoryPanel.razor`)

This keeps gameplay logic out of `.razor` files and makes UI easier to test/change.

### One-way parameters
Use parameters when a parent UI/controller owns the data and child components only display it.

### Event-driven refresh
Gameplay classes expose events (`OnVitalsChanged`, `OnAmmoChanged`, etc.).
The UI layer listens and requests re-render when state changes.

### Why this approach works for survival games
- frequent state changes (ammo, health, hunger)
- clear separation between simulation and presentation
- easy to add more widgets (hotbar, raid timer, compass)

## Suggested Next Build Steps

1. Add per-weapon config assets (ammo type, magazine, reload timing, VFX/SFX IDs).
2. Implement actual engine prefab/audio calls in `EngineWeaponEffectsPlayer`.
3. Add world-drop entity spawning for dropped stacks and weapons.
4. Add enemy archetype selection inside raid wave spawn handlers.
5. Add inventory stack limits by item definition in merge operations.
6. Add hotbar quick-use and drag-to-hotbar behavior.

## Current Scope Notes

The code in this repository is a clean starter framework to unblock development quickly. Engine-specific integration points (input binding, scene object spawning, particle/audio playback, and prefab linking) should now be connected inside your game engine runtime.