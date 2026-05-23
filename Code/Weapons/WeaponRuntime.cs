using System;

namespace VSurvival.Code.Weapons;

public sealed class WeaponRuntime
{
    public WeaponRuntime(string id, AmmoType ammoType, int magazineSize, float reloadSeconds)
    {
        Id = id;
        AmmoType = ammoType;
        MagazineSize = Math.Max(1, magazineSize);
        ReloadSeconds = Math.Max(0.1f, reloadSeconds);
        AmmoInMagazine = MagazineSize;
    }

    public string Id { get; }
    public AmmoType AmmoType { get; }
    public int MagazineSize { get; }
    public float ReloadSeconds { get; }
    public int AmmoInMagazine { get; private set; }
    public bool IsReloading { get; private set; }

    public event Action<WeaponRuntime>? OnShoot;
    public event Action<WeaponRuntime>? OnMuzzleFlash;
    public event Action<WeaponRuntime>? OnReloadStarted;
    public event Action<WeaponRuntime>? OnReloadFinished;

    public bool TryShoot()
    {
        if (IsReloading || AmmoInMagazine <= 0) return false;

        AmmoInMagazine--;
        OnShoot?.Invoke(this);
        OnMuzzleFlash?.Invoke(this);
        return true;
    }

    public bool TryStartReload(AmmoPool ammoPool)
    {
        if (IsReloading || AmmoInMagazine >= MagazineSize) return false;

        var needed = MagazineSize - AmmoInMagazine;
        var available = ammoPool.Get(AmmoType);
        if (available <= 0) return false;

        IsReloading = true;
        OnReloadStarted?.Invoke(this);

        var loadAmount = Math.Min(needed, available);
        ammoPool.TryConsume(AmmoType, loadAmount);
        AmmoInMagazine += loadAmount;

        IsReloading = false;
        OnReloadFinished?.Invoke(this);
        return true;
    }
}
