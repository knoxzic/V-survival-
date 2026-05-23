# Scenes Folder Guide

Purpose:
- Stores game levels and test scenes.

Files:
- MainGame.scene: full game loop.
- Test_Weapon.scene: isolated weapon/audio/effects testing.

How to link:
1. In MainGame.scene, initialize PlayerController and UI components.
2. In Test_Weapon.scene, focus on fire/reload timing, muzzle flash, and sound balance.
3. Subscribe to NightlyWaveSpawner events only in scenes where raid testing is needed.
