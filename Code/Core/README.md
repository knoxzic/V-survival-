# Core Folder Guide

Purpose:
- Owns the top-level player loop for the S&box 2026 starter.
- This folder connects the systems that already live in `Code/Inventory`, `Code/PlayerStats`, `Code/Weapons`, and `Code/AI`.

Main files:
- [PlayerController.cs](PlayerController.cs): creates and owns the player systems.
- [PickupDropController.cs](PickupDropController.cs): pickup and drop actions.
- [DayNightCycle.cs](DayNightCycle.cs): time progression and night detection.

What to build in this folder first:
1. `PlayerController`
2. `DayNightCycle`
3. `PickupDropController`
4. `NightlyWaveSpawner`

How to link:
1. Create one `PlayerController` at scene startup.
2. Call `Tick(deltaSeconds, isSprinting)` once per frame.
3. Route weapon input through `EquipWeaponSlot`, `WeaponManager.TryShoot()`, and `WeaponManager.TryReload()`.
4. Route loot input through `PickupDropController`.
5. Call `AttachWeaponEffects(...)` with your S&box audio and particle adapter.
6. Read `RaidWaves.OnWaveSpawnRequested` when you want to spawn enemies.

Beginner order tip:
- Do not wire scene objects to HUD or inventory first.
- Start with the player controller, because it is the place where the other systems meet.

Common beginner mistake:
- Creating separate copies of health, ammo, or inventory in more than one place. Keep one source of truth here and let UI read from it.
