# Scenes Folder Guide

Purpose:
- Stores game levels and test scenes.
- Scenes should be wired after the gameplay code and Razor UI exist.

Files:
- [MainGame.scene](MainGame.scene): the full game loop.
- [Test_Weapon.scene](Test_Weapon.scene): isolated weapon, audio, and effect testing.

How to link:
1. In `MainGame.scene`, initialize `PlayerController` and the UI components.
2. In `Test_Weapon.scene`, focus on fire timing, reload timing, muzzle flash, and sound balance.
3. Subscribe to `NightlyWaveSpawner` events only in scenes that should test raids.
4. Keep the test scene simple so weapon problems are easy to isolate.

Beginner note:
- If a scene is hard to understand, it is usually doing too much.
- Keep the main scene for gameplay and the test scene for one system at a time.
