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

The ammo pool tracks Light, Heavy, and Shell ammo and exposes change events for UI updates.

### 1.7 Two weapon slots + separate inventory resources

Weapons are restricted to two dedicated slots (primary and secondary). Non-weapon resources use a separate 24-slot inventory.

### 2 Pick/Drop item components

This wraps pickup and drop behavior so game input can call a single point for resource or weapon drop actions.

### 3, 4, 5 Health/Stamina/Hunger/Thirst bars

Vitals tick down over time and are rendered in the HUD as progress bars for first-person play.

### 5 Wandering AI that can be killed and harvested

AI has wander target generation, damage handling, death state, and basic harvest yield.

### 6 Rust-like inventory UI (character left, 24 slots right)

The UI provides a split layout with character/weapon section on the left and a 24-cell grid on the right.

### 7 Editor-friendly snapping component with `[Property]`

Includes configurable grid size and optional Y-axis snapping to make placement/building easier.

### 9 Day and night cycle

Supports configurable day length and `IsNight` checks for triggering wave events.

### Nightly raid-wave spawner tied to day/night

The player tick now drives a nightly wave spawner that starts when night begins, requests waves at intervals, and increases pressure over nights.

### Engine path for muzzle flash + shoot/reload sound

Weapon events are now bridged into a single effects interface so engine-specific audio/particle playback can be plugged in cleanly.

### Drag/drop + split-stack inventory interactions

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

## Razor UI Concepts (Practical Guide)

Razor components combine markup and C# into one file.

### Component structure
Each component has:

Example from `UI/Hud.razor`:

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
# V-survival-

Beginner-friendly S&box 2026 survival starter for first-person combat, looting, base prep, inventory management, and nightly raid pressure.

This repository is split the way a S&box 2026 project should be split:
- C# gameplay systems live under `Code/`.
- Razor UI lives under `UI/`.
- Prefabs, scenes, and raw art live under `Prefabs/`, `Scenes/`, and `Assets/`.

## Read This First

If you are new to S&box, follow this order to avoid the most common compile and wiring problems:

1. Read [Code/README.md](Code/README.md).
2. Build [Code/Inventory](Code/Inventory/README.md).
3. Build [Code/PlayerStats](Code/PlayerStats/README.md).
4. Build [Code/Weapons](Code/Weapons/README.md).
5. Build [Code/Core](Code/Core/README.md).
6. Build [Code/AI](Code/AI/README.md).
7. Build [Code/UI](Code/UI/README.md).
8. Build [UI](UI/README.md).
9. Add [Prefabs](Prefabs/README.md).
10. Wire [Scenes](Scenes/README.md).
11. Organize [Assets](Assets/README.md).

If you skip that order, the usual beginner problems are:
- Razor files referencing view models that do not exist yet.
- Callbacks with the wrong parameter type.
- Weapon or inventory UI trying to own gameplay state.
- Scene setup happening before the systems it depends on exist.

## S&box 2026 Mental Model

Think in three layers.

### Gameplay layer
Plain C# systems hold the actual rules and state.
- inventory
- player stats
- weapons and ammo
- day/night timing
- AI wandering and raid waves

### Bridge layer
Small C# view models expose gameplay data to Razor.
- [Code/UI/HudViewModel.cs](Code/UI/HudViewModel.cs)
- [Code/UI/InventoryViewModel.cs](Code/UI/InventoryViewModel.cs)

### Razor layer
The `.razor` files draw the UI and send user actions back.
- [UI/Hud.razor](UI/Hud.razor)
- [UI/InventoryPanel.razor](UI/InventoryPanel.razor)

This is the safest way for a beginner to keep the project understandable and compile-friendly.

## Current Feature Map

### Weapons and ammo
- [Code/Weapons/AmmoType.cs](Code/Weapons/AmmoType.cs)
- [Code/Weapons/AmmoPool.cs](Code/Weapons/AmmoPool.cs)
- [Code/Weapons/WeaponRuntime.cs](Code/Weapons/WeaponRuntime.cs)
- [Code/Weapons/WeaponManager.cs](Code/Weapons/WeaponManager.cs)
- [Code/Weapons/WeaponEffectsBinder.cs](Code/Weapons/WeaponEffectsBinder.cs)
- [Code/Weapons/EngineWeaponEffectsPlayer.cs](Code/Weapons/EngineWeaponEffectsPlayer.cs)

### Inventory and items
- [Code/Inventory/ItemDefinition.cs](Code/Inventory/ItemDefinition.cs)
- [Code/Inventory/InventorySystem.cs](Code/Inventory/InventorySystem.cs)

### Player state
- [Code/PlayerStats/PlayerVitals.cs](Code/PlayerStats/PlayerVitals.cs)

### Core flow
- [Code/Core/PlayerController.cs](Code/Core/PlayerController.cs)
- [Code/Core/PickupDropController.cs](Code/Core/PickupDropController.cs)
- [Code/Core/DayNightCycle.cs](Code/Core/DayNightCycle.cs)

### AI and raid scheduling
- [Code/AI/WanderingAI.cs](Code/AI/WanderingAI.cs)
- [Code/AI/NightlyWaveSpawner.cs](Code/AI/NightlyWaveSpawner.cs)

### Razor UI
- [UI/Hud.razor](UI/Hud.razor)
- [UI/InventoryPanel.razor](UI/InventoryPanel.razor)

## How The Razor Link Works

1. Gameplay systems update state in C#.
2. View models expose that state in a simpler shape.
3. Razor components receive the data through `[Parameter]` properties.
4. Razor components send actions back through `EventCallback` parameters.

That pattern keeps the UI easy to understand and much easier to debug.

## Beginner Setup Order

If you are building this into a fresh S&box 2026 project, do this in order:

1. Create the `Code/` folder tree.
2. Add inventory and item data first.
3. Add player stats next so the HUD has real values.
4. Add weapons and ammo.
5. Add the player controller that owns the systems.
6. Add AI and night raid logic.
7. Add UI view models.
8. Add Razor panels.
9. Add prefabs and test scenes.
10. Connect engine audio, particle, and spawn hooks last.

If you follow that order, you avoid most missing-type and missing-namespace errors.

## Beginner Notes For S&box 2026

- Keep gameplay logic in C# files, not in Razor markup.
- Keep Razor components as display plus input forwarding only.
- Use events when you want the UI to refresh after state changes.
- Use prefabs for reusable objects and scenes for the actual test setups.
- Use `Assets/` for source content, not for gameplay logic.

## Folder Guides

Each working folder has its own beginner guide:
- [Code/README.md](Code/README.md)
- [Code/Core/README.md](Code/Core/README.md)
- [Code/Weapons/README.md](Code/Weapons/README.md)
- [Code/Inventory/README.md](Code/Inventory/README.md)
- [Code/PlayerStats/README.md](Code/PlayerStats/README.md)
- [Code/AI/README.md](Code/AI/README.md)
- [Code/UI/README.md](Code/UI/README.md)
- [UI/README.md](UI/README.md)
- [Prefabs/README.md](Prefabs/README.md)
- [Scenes/README.md](Scenes/README.md)
- [Assets/README.md](Assets/README.md)

## What Is Already In The Repo

The current starter includes:
- first-person weapon state and ammo pools
- two dedicated weapon slots and a separate resource inventory
- health, stamina, hunger, and thirst
- pickup and drop flow
- wandering AI and night wave scheduling
- a Razor HUD
- a Razor inventory panel with drag/drop and split-stack behavior
- a snap-to-grid helper for editor placement work

## What To Build Next

1. Replace the placeholder engine hooks in [Code/Weapons/EngineWeaponEffectsPlayer.cs](Code/Weapons/EngineWeaponEffectsPlayer.cs) with real S&box audio and particle playback.
2. Connect [Code/AI/NightlyWaveSpawner.cs](Code/AI/NightlyWaveSpawner.cs) to actual enemy prefab spawning.
3. Add hotbar and item-use callbacks to the Razor inventory.
4. Add real item definitions for weapons, resources, and consumables.
5. Add scene bootstrap code for `MainGame.scene` and `Test_Weapon.scene`.

## Important Limitation

This repository is a clean starter scaffold. It shows the correct structure and wiring pattern, but the actual S&box scene setup, audio playback, particle spawning, and prefab instantiation still need to be connected inside your S&box project.