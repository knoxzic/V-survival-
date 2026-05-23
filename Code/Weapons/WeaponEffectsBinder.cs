using System;

namespace VSurvival.Code.Weapons;

public interface IWeaponEffectsPlayer
{
    void PlayMuzzleFlash(string weaponId);
    void PlayShootSound(string weaponId);
    void PlayReloadStartSound(string weaponId);
    void PlayReloadEndSound(string weaponId);
}

public sealed class WeaponEffectsBinder : IDisposable
{
    private readonly WeaponManager _weaponManager;
    private readonly IWeaponEffectsPlayer _effects;
    private WeaponRuntime? _current;

    public WeaponEffectsBinder(WeaponManager weaponManager, IWeaponEffectsPlayer effects)
    {
        _weaponManager = weaponManager;
        _effects = effects;

        _weaponManager.OnActiveWeaponChanged += HandleActiveWeaponChanged;
        HandleActiveWeaponChanged(_weaponManager.ActiveWeapon);
    }

    public void Dispose()
    {
        _weaponManager.OnActiveWeaponChanged -= HandleActiveWeaponChanged;
        Unbind(_current);
        _current = null;
    }

    private void HandleActiveWeaponChanged(WeaponRuntime? next)
    {
        Unbind(_current);
        _current = next;
        Bind(_current);
    }

    private void Bind(WeaponRuntime? weapon)
    {
        if (weapon is null) return;

        weapon.OnShoot += HandleShoot;
        weapon.OnMuzzleFlash += HandleMuzzleFlash;
        weapon.OnReloadStarted += HandleReloadStarted;
        weapon.OnReloadFinished += HandleReloadFinished;
    }

    private void Unbind(WeaponRuntime? weapon)
    {
        if (weapon is null) return;

        weapon.OnShoot -= HandleShoot;
        weapon.OnMuzzleFlash -= HandleMuzzleFlash;
        weapon.OnReloadStarted -= HandleReloadStarted;
        weapon.OnReloadFinished -= HandleReloadFinished;
    }

    private void HandleShoot(WeaponRuntime weapon)
    {
        _effects.PlayShootSound(weapon.Id);
    }

    private void HandleMuzzleFlash(WeaponRuntime weapon)
    {
        _effects.PlayMuzzleFlash(weapon.Id);
    }

    private void HandleReloadStarted(WeaponRuntime weapon)
    {
        _effects.PlayReloadStartSound(weapon.Id);
    }

    private void HandleReloadFinished(WeaponRuntime weapon)
    {
        _effects.PlayReloadEndSound(weapon.Id);
    }
}