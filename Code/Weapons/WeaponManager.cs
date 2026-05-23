using System;
using VSurvival.Code.Inventory;

namespace VSurvival.Code.Weapons;

public sealed class WeaponManager
{
    private readonly InventorySystem _inventory;

    public WeaponManager(InventorySystem inventory, AmmoPool ammoPool)
    {
        _inventory = inventory;
        AmmoPool = ammoPool;
    }

    public WeaponRuntime? ActiveWeapon { get; private set; }
    public AmmoPool AmmoPool { get; }

    public event Action<WeaponRuntime?>? OnActiveWeaponChanged;

    public bool EquipPrimary()
    {
        var id = _inventory.GetWeaponSlot(0);
        return EquipFromId(id, AmmoType.Light);
    }

    public bool EquipSecondary()
    {
        var id = _inventory.GetWeaponSlot(1);
        return EquipFromId(id, AmmoType.Shell);
    }

    public bool TryShoot() => ActiveWeapon?.TryShoot() == true;

    public bool TryReload() => ActiveWeapon?.TryStartReload(AmmoPool) == true;

    private bool EquipFromId(string? itemId, AmmoType fallbackAmmo)
    {
        if (string.IsNullOrWhiteSpace(itemId))
        {
            ActiveWeapon = null;
            OnActiveWeaponChanged?.Invoke(ActiveWeapon);
            return false;
        }

        ActiveWeapon = new WeaponRuntime(itemId, fallbackAmmo, magazineSize: 30, reloadSeconds: 1.25f);
        OnActiveWeaponChanged?.Invoke(ActiveWeapon);
        return true;
    }
}
