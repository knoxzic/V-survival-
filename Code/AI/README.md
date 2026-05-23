# AI Folder Guide

Purpose:
- Contains enemy and wildlife behavior loops.

Main files:
- WanderingAI.cs: roaming target selection, damage, death, harvest.
- NightlyWaveSpawner.cs: night-based wave scheduling and difficulty scaling.

How to link:
1. Update wandering entities in your AI loop.
2. Call NightlyWaveSpawner.Tick(deltaSeconds, isNight) each frame.
3. Subscribe to OnWaveSpawnRequested and spawn enemies in the scene.
4. Use WaveSpawnData.EnemyCount and DifficultyTier to select prefabs and stats.

Beginner note:
- Spawner only requests waves; your engine-specific spawn system should do the actual instantiation.
