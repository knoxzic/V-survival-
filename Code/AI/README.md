# AI Folder Guide

Purpose:
- Contains enemy, wildlife, and raid-wave behavior.
- This folder should stay engine-light: it decides what should happen, but the scene decides how to spawn it.

Main files:
- [WanderingAI.cs](WanderingAI.cs): roaming target selection, damage, death, and harvest.
- [NightlyWaveSpawner.cs](NightlyWaveSpawner.cs): night-based wave scheduling and difficulty scaling.

Recommended build order:
1. `WanderingAI`
2. `NightlyWaveSpawner`
3. Scene-side enemy spawn handler

How to link:
1. Update wandering entities in your AI loop.
2. Call `NightlyWaveSpawner.Tick(deltaSeconds, isNight)` every frame.
3. Subscribe to `OnWaveSpawnRequested` and spawn enemies in the scene.
4. Use `WaveSpawnData.EnemyCount` and `DifficultyTier` to choose which prefabs to spawn.
5. Use `OnNightStarted` and `OnNightEnded` for music, alarms, or raid setup.

Beginner note:
- The spawner only requests waves.
- Your S&box scene or component should do the actual instantiation.

Common beginner mistake:
- Spawning enemies directly inside the spawner and also in the scene.
- Pick one place to spawn them.
