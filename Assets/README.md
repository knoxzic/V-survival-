# Assets Folder Guide

Purpose:
- Stores source content consumed by prefabs and scene objects.

Subfolders:
- Models: world objects, weapons, enemies.
- Sounds: gunshots, reloads, ambience, creature audio.
- Materials: shaders and surfaces.
- Particles: smoke, flashes, impacts.

How to link:
1. Build prefabs from assets first.
2. Reference prefabs from systems, not raw asset files, to keep scene setup stable.
