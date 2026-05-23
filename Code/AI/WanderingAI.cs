using System;
using System.Numerics;

namespace VSurvival.Code.AI;

public sealed class WanderingAI
{
    private readonly Random _random = new();

    public WanderingAI(Vector3 origin, float radius = 20f)
    {
        Origin = origin;
        WanderRadius = MathF.Max(1f, radius);
        CurrentTarget = origin;
    }

    public Vector3 Origin { get; }
    public Vector3 CurrentTarget { get; private set; }
    public float WanderRadius { get; }
    public float Health { get; private set; } = 50f;
    public bool IsDead => Health <= 0f;
    public bool CanBeHarvested => IsDead;

    public event Action? OnTargetChanged;
    public event Action? OnKilled;

    public void PickNewTarget()
    {
        var x = (float)(_random.NextDouble() * 2d - 1d) * WanderRadius;
        var z = (float)(_random.NextDouble() * 2d - 1d) * WanderRadius;
        CurrentTarget = Origin + new Vector3(x, 0f, z);
        OnTargetChanged?.Invoke();
    }

    public void ApplyDamage(float amount)
    {
        if (IsDead || amount <= 0f) return;

        Health = MathF.Max(0f, Health - amount);
        if (IsDead)
        {
            OnKilled?.Invoke();
        }
    }

    public int Harvest()
    {
        if (!CanBeHarvested) return 0;
        return _random.Next(1, 4);
    }
}
