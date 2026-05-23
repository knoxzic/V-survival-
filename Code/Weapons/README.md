# Weapons Folder Guide

Purpose:
- Handles weapon firing state, ammo pools, equip flow, and effect/sound hooks.

Main files:
- AmmoType.cs and AmmoPool.cs: global reserve ammo types and counts.
- WeaponRuntime.cs: per-weapon magazine, shoot, and reload events.
- WeaponManager.cs: active weapon and slot-based equipping.
- WeaponEffectsBinder.cs: subscribes to weapon events and triggers engine effects.
- EngineWeaponEffectsPlayer.cs: placeholder for engine API calls.

How to link:
1. Add weapons to inventory weapon slots.
2. Equip with WeaponManager.EquipPrimary/EquipSecondary.
3. Trigger WeaponManager.TryShoot and TryReload from input.
4. Implement EngineWeaponEffectsPlayer methods using your engine audio/particle APIs.
5. Attach effects through PlayerController.AttachWeaponEffects.

Beginner note:
- WeaponRuntime events are your bridge from gameplay logic to visual/audio feedback.
