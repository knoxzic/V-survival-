using VSurvival.Code.Inventory;

namespace VSurvival.Code.Core;

public sealed class PickupDropController
{
    private readonly InventorySystem _inventory;

    public PickupDropController(InventorySystem inventory)
    {
        _inventory = inventory;
    }

    public bool TryPickup(ItemDefinition item, int amount = 1)
    {
        return _inventory.TryPickup(item, amount);
    }

    public bool DropResourceAt(int slotIndex)
    {
        return _inventory.TryDropResourceSlot(slotIndex);
    }

    public bool DropWeaponAt(int slotIndex)
    {
        return _inventory.TryDropWeaponSlot(slotIndex);
    }
}
