using System;
using VSurvival.Code.Core;
using VSurvival.Code.Weapons;

namespace VSurvival.Code.UI;

public sealed class HudViewModel
{
    private readonly PlayerController _player;

    public HudViewModel(PlayerController player)
    {
        _player = player;
        _player.Vitals.OnVitalsChanged += () => Changed?.Invoke();
        _player.AmmoPool.OnAmmoChanged += (_, _) => Changed?.Invoke();
        _player.WeaponManager.OnActiveWeaponChanged += _ => Changed?.Invoke();
    }

    public event Action? Changed;

    public float Health => _player.Vitals.Health;
    public float Stamina => _player.Vitals.Stamina;
    public float Hunger => _player.Vitals.Hunger;
    public float Thirst => _player.Vitals.Thirst;

    public int LightAmmo => _player.AmmoPool.Get(AmmoType.Light);
    public int HeavyAmmo => _player.AmmoPool.Get(AmmoType.Heavy);
    public int ShellAmmo => _player.AmmoPool.Get(AmmoType.Shell);

    public int AmmoInMagazine => _player.WeaponManager.ActiveWeapon?.AmmoInMagazine ?? 0;
    public string WeaponId => _player.WeaponManager.ActiveWeapon?.Id ?? "Unarmed";
}
