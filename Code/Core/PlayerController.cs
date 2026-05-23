using VSurvival.Code.Inventory;
using VSurvival.Code.PlayerStats;
using VSurvival.Code.AI;
using VSurvival.Code.Weapons;

namespace VSurvival.Code.Core;

public sealed class PlayerController
{
    private WeaponEffectsBinder? _weaponEffectsBinder;

    public PlayerController()
    {
        Inventory = new InventorySystem();
        Vitals = new PlayerVitals();
        AmmoPool = new AmmoPool();
        WeaponManager = new WeaponManager(Inventory, AmmoPool);
        PickupDrop = new PickupDropController(Inventory);
        DayNight = new DayNightCycle();
        RaidWaves = new NightlyWaveSpawner();
    }

    public InventorySystem Inventory { get; }
    public PlayerVitals Vitals { get; }
    public AmmoPool AmmoPool { get; }
    public WeaponManager WeaponManager { get; }
    public PickupDropController PickupDrop { get; }
    public DayNightCycle DayNight { get; }
    public NightlyWaveSpawner RaidWaves { get; }

    public string? FirstPersonHeldItemId { get; private set; }

    public void Tick(float deltaSeconds, bool isSprinting)
    {
        Vitals.Tick(deltaSeconds, isSprinting);
        DayNight.Tick(deltaSeconds);
        RaidWaves.Tick(deltaSeconds, DayNight.IsNight);
    }

    public void SetHeldItem(string? itemId)
    {
        FirstPersonHeldItemId = itemId;
    }

    public bool EquipWeaponSlot(int slot)
    {
        return slot switch
        {
            0 => WeaponManager.EquipPrimary(),
            1 => WeaponManager.EquipSecondary(),
            _ => false
        };
    }

    public void AttachWeaponEffects(IWeaponEffectsPlayer effectsPlayer)
    {
        _weaponEffectsBinder?.Dispose();
        _weaponEffectsBinder = new WeaponEffectsBinder(WeaponManager, effectsPlayer);
    }
}
