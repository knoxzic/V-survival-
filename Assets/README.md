# Assets Folder Guide

Purpose:
- Stores source content consumed by prefabs and scenes.
- These files are raw ingredients, not gameplay logic.

Subfolders:
- `Models`: world objects, weapons, enemies.
- `Sounds`: gunshots, reloads, ambience, creature audio.
- `Materials`: shaders and surfaces.
- `Particles`: smoke, flashes, impacts.

How to link:
1. Build prefabs from assets first.
2. Reference prefabs from systems, not raw asset files, to keep scene setup stable.
3. Keep asset names consistent with the code IDs you use in weapons, inventory, and AI.

Beginner note:
- Assets are usually the last thing you wire after the code and UI are already working.
