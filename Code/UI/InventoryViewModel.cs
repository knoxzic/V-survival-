using System.Collections.Generic;
using VSurvival.Code.Core;
using VSurvival.Code.Inventory;

namespace VSurvival.Code.UI;

public sealed class InventoryViewModel
{
    private readonly PlayerController _player;

    public InventoryViewModel(PlayerController player)
    {
        _player = player;
    }

    public IReadOnlyList<InventorySystem.ItemStack?> Resources => _player.Inventory.ResourceInventory;

    public IReadOnlyList<string?> WeaponSlots => _player.Inventory.GetAllWeaponSlots();

    public bool MoveResourceStack(int fromIndex, int toIndex)
    {
        return _player.Inventory.TryMoveResourceStack(fromIndex, toIndex);
    }

    public bool SplitResourceStackHalf(int fromIndex)
    {
        return _player.Inventory.TryAutoSplitHalf(fromIndex);
    }
}
