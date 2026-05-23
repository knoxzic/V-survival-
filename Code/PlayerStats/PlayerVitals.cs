using System;

namespace VSurvival.Code.PlayerStats;

public sealed class PlayerVitals
{
    public float MaxHealth { get; init; } = 100f;
    public float MaxStamina { get; init; } = 100f;
    public float MaxHunger { get; init; } = 100f;
    public float MaxThirst { get; init; } = 100f;

    public float Health { get; private set; } = 100f;
    public float Stamina { get; private set; } = 100f;
    public float Hunger { get; private set; } = 100f;
    public float Thirst { get; private set; } = 100f;

    public event Action? OnVitalsChanged;
    public event Action? OnPlayerDied;

    public void Tick(float deltaSeconds, bool isSprinting)
    {
        Hunger = MathF.Max(0f, Hunger - (0.15f * deltaSeconds));
        Thirst = MathF.Max(0f, Thirst - (0.25f * deltaSeconds));

        var staminaDelta = isSprinting ? -18f * deltaSeconds : 14f * deltaSeconds;
        Stamina = Math.Clamp(Stamina + staminaDelta, 0f, MaxStamina);

        if (Hunger <= 0f || Thirst <= 0f)
        {
            Health = MathF.Max(0f, Health - (5f * deltaSeconds));
        }

        if (Health <= 0f)
        {
            OnPlayerDied?.Invoke();
        }

        OnVitalsChanged?.Invoke();
    }

    public void AddHealth(float amount)
    {
        Health = Math.Clamp(Health + amount, 0f, MaxHealth);
        OnVitalsChanged?.Invoke();
    }

    public void ApplyDamage(float amount)
    {
        if (amount <= 0f) return;
        Health = Math.Clamp(Health - amount, 0f, MaxHealth);
        if (Health <= 0f) OnPlayerDied?.Invoke();
        OnVitalsChanged?.Invoke();
    }
}
