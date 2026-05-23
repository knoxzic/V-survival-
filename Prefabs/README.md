# Prefabs Folder Guide

Purpose:
- Stores reusable scene objects used by code systems.

Subfolders:
- Weapons: weapon entities and visual models.
- Items: pickup entities.
- Effects: muzzle flashes, impact effects, shell ejections.
- Enemies: AI unit templates.

How to link:
1. Match prefab IDs with item and weapon IDs from code.
2. Use enemy prefabs inside nightly wave spawn handlers.
3. Use effects prefabs in EngineWeaponEffectsPlayer methods.
