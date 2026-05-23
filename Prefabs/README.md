# Prefabs Folder Guide

Purpose:
- Stores reusable scene objects used by the code systems.
- Build prefabs after the gameplay code is stable.

Subfolders:
- `Weapons`: weapon entities and visual models.
- `Items`: pickup entities.
- `Effects`: muzzle flashes, impact effects, shell ejections.
- `Enemies`: AI unit templates.

How to link:
1. Match prefab IDs with item and weapon IDs from code.
2. Use enemy prefabs inside nightly wave spawn handlers.
3. Use effect prefabs inside `EngineWeaponEffectsPlayer`.
4. Keep one clear ID naming pattern so you can find the right prefab later.

Beginner note:
- Prefabs are content containers.
- They should not replace your gameplay code or your UI code.
