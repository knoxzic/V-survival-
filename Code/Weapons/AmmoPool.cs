using System;
using System.Collections.Generic;

namespace VSurvival.Code.Weapons;

public sealed class AmmoPool
{
    private readonly Dictionary<AmmoType, int> _ammo = new()
    {
        [AmmoType.Light] = 120,
        [AmmoType.Heavy] = 60,
        [AmmoType.Shell] = 32
    };

    public event Action<AmmoType, int>? OnAmmoChanged;

    public int Get(AmmoType type) => _ammo.TryGetValue(type, out var value) ? value : 0;

    public void Add(AmmoType type, int amount)
    {
        if (amount <= 0) return;

        _ammo[type] = Get(type) + amount;
        OnAmmoChanged?.Invoke(type, _ammo[type]);
    }

    public bool TryConsume(AmmoType type, int amount)
    {
        if (amount <= 0) return true;

        var current = Get(type);
        if (current < amount) return false;

        _ammo[type] = current - amount;
        OnAmmoChanged?.Invoke(type, _ammo[type]);
        return true;
    }
}
