# Core Folder Guide

Purpose:
- Owns top-level gameplay flow and wiring between systems.

Main files:
- PlayerController.cs: creates and updates all player systems.
- PickupDropController.cs: pickup and drop actions.
- DayNightCycle.cs: time progression and night detection.

How to link:
1. Create a PlayerController instance at game start.
2. Call Tick(deltaSeconds, isSprinting) every frame.
3. Route input to EquipWeaponSlot, WeaponManager.TryShoot, and WeaponManager.TryReload.
4. Call AttachWeaponEffects with an IWeaponEffectsPlayer implementation.

Beginner note:
- Keep this folder focused on orchestration, not detailed weapon or UI logic.
