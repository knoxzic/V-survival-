# Code Folder Guide

Purpose:
- This is the gameplay layer for the S&box 2026 starter.
- Keep rules, state, and engine-facing logic here.
- Keep Razor markup out of this folder.

Recommended build order:
1. [Code/Inventory](Code/Inventory/README.md)
2. [Code/PlayerStats](Code/PlayerStats/README.md)
3. [Code/Weapons](Code/Weapons/README.md)
4. [Code/Core](Code/Core/README.md)
5. [Code/AI](Code/AI/README.md)
6. [Code/UI](Code/UI/README.md)

Why this order matters:
- Inventory and item types are the foundation for loot, hotbar, and weapon slots.
- Player stats must exist before the HUD can display anything.
- Weapons depend on ammo and inventory.
- Core gameplay owns the player and day/night loop.
- AI depends on the core loop because raid waves are tied to time of day.
- UI comes last because it reads from the systems above it.

Beginner rule:
- If a file needs a type that does not exist yet, stop and build the missing lower-level folder first.

Safe integration path:
- Build the gameplay systems.
- Build the view models.
- Build the Razor UI.
- Then connect prefabs, scenes, and assets.