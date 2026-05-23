using System;

namespace VSurvival.Code.AI;

public sealed class NightlyWaveSpawner
{
    private float _spawnTimer;
    private bool _wasNight;

    public NightlyWaveSpawner()
    {
        SecondsBetweenWaves = 45f;
        MaxWavesPerNight = 4;
    }

    public float SecondsBetweenWaves { get; set; }
    public int MaxWavesPerNight { get; set; }
    public int CurrentNightIndex { get; private set; }
    public int SpawnedWavesThisNight { get; private set; }

    public event Action<int>? OnNightStarted;
    public event Action<int>? OnNightEnded;
    public event Action<WaveSpawnData>? OnWaveSpawnRequested;

    public void Tick(float deltaSeconds, bool isNight)
    {
        if (isNight && !_wasNight)
        {
            CurrentNightIndex++;
            SpawnedWavesThisNight = 0;
            _spawnTimer = 0f;
            OnNightStarted?.Invoke(CurrentNightIndex);
        }

        if (!isNight && _wasNight)
        {
            OnNightEnded?.Invoke(CurrentNightIndex);
        }

        _wasNight = isNight;
        if (!isNight || SpawnedWavesThisNight >= MaxWavesPerNight) return;

        _spawnTimer -= deltaSeconds;
        if (_spawnTimer > 0f) return;

        SpawnedWavesThisNight++;
        _spawnTimer = Math.Max(5f, SecondsBetweenWaves);

        var difficultyTier = 1 + (CurrentNightIndex / 2);
        var enemyCount = 2 + SpawnedWavesThisNight + CurrentNightIndex;

        OnWaveSpawnRequested?.Invoke(new WaveSpawnData(
            CurrentNightIndex,
            SpawnedWavesThisNight,
            difficultyTier,
            enemyCount));
    }

    public readonly record struct WaveSpawnData(
        int Night,
        int Wave,
        int DifficultyTier,
        int EnemyCount);
}