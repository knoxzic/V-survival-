# Weapons Folder Guide

Purpose:
- Handles ammo, weapon state, and the bridge from gameplay into S&box 2026 visual/audio effects.

Main files:
- [AmmoType.cs](AmmoType.cs): the three reserve ammo categories.
- [AmmoPool.cs](AmmoPool.cs): stored reserve ammo counts.
- [WeaponRuntime.cs](WeaponRuntime.cs): per-weapon magazine state and weapon events.
- [WeaponManager.cs](WeaponManager.cs): active weapon and slot equipping.
- [WeaponEffectsBinder.cs](WeaponEffectsBinder.cs): connects weapon events to engine effects.
- [EngineWeaponEffectsPlayer.cs](EngineWeaponEffectsPlayer.cs): the S&box hook point for particles and audio.

Recommended build order:
1. `AmmoType`
2. `AmmoPool`
3. `WeaponRuntime`
4. `WeaponManager`
5. `WeaponEffectsBinder`
6. `EngineWeaponEffectsPlayer`

How to link:
1. Put weapon item IDs into the inventory weapon slots.
2. Use `WeaponManager.EquipPrimary()` and `WeaponManager.EquipSecondary()` to swap weapons.
3. Call `WeaponManager.TryShoot()` from your fire input.
4. Call `WeaponManager.TryReload()` from your reload input.
5. Put the real S&box muzzle flash and sound code inside `EngineWeaponEffectsPlayer`.
6. Attach that adapter through `PlayerController.AttachWeaponEffects(...)`.

Beginner note:
- `WeaponRuntime` raises events only.
- The event binder and engine adapter are what turn those events into visible effects and sound.

Common beginner mistake:
- Trying to play the muzzle flash directly from the UI or from the inventory.
- The correct path is input -> weapon runtime -> effect binder -> engine adapter.
