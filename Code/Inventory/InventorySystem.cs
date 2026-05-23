using System;
using System.Collections.Generic;
using System.Linq;

namespace VSurvival.Code.Inventory;

public sealed class InventorySystem
{
    public const int ResourceSlots = 24;

    private readonly ItemStack?[] _resourceSlots = new ItemStack?[ResourceSlots];
    private readonly string?[] _weaponSlots = new string?[2];

    public event Action? OnInventoryChanged;

    public IReadOnlyList<ItemStack?> ResourceInventory => _resourceSlots;

    public int Capacity => _resourceSlots.Length;

    public bool TryPickup(ItemDefinition item, int amount = 1)
    {
        if (item.IsWeapon)
        {
            for (var i = 0; i < _weaponSlots.Length; i++)
            {
                if (_weaponSlots[i] is not null) continue;
                _weaponSlots[i] = item.Id;
                OnInventoryChanged?.Invoke();
                return true;
            }

            return false;
        }

        for (var i = 0; i < _resourceSlots.Length; i++)
        {
            var stack = _resourceSlots[i];
            if (stack is not null && stack.Value.ItemId == item.Id && stack.Value.Amount < item.MaxStack)
            {
                var nextAmount = Math.Min(item.MaxStack, stack.Value.Amount + amount);
                _resourceSlots[i] = stack.Value with { Amount = nextAmount };
                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        for (var i = 0; i < _resourceSlots.Length; i++)
        {
            if (_resourceSlots[i] is not null) continue;
            _resourceSlots[i] = new ItemStack(item.Id, Math.Min(amount, item.MaxStack));
            OnInventoryChanged?.Invoke();
            return true;
        }

        return false;
    }

    public bool TryDropResourceSlot(int index)
    {
        if (index < 0 || index >= _resourceSlots.Length || _resourceSlots[index] is null) return false;

        _resourceSlots[index] = null;
        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool TryMoveResourceStack(int fromIndex, int toIndex)
    {
        if (!IsValidResourceSlot(fromIndex) || !IsValidResourceSlot(toIndex) || fromIndex == toIndex) return false;

        var source = _resourceSlots[fromIndex];
        var target = _resourceSlots[toIndex];
        if (source is null) return false;

        if (target is null)
        {
            _resourceSlots[toIndex] = source;
            _resourceSlots[fromIndex] = null;
            OnInventoryChanged?.Invoke();
            return true;
        }

        if (target.Value.ItemId == source.Value.ItemId)
        {
            _resourceSlots[toIndex] = new ItemStack(target.Value.ItemId, target.Value.Amount + source.Value.Amount);
            _resourceSlots[fromIndex] = null;
            OnInventoryChanged?.Invoke();
            return true;
        }

        _resourceSlots[fromIndex] = target;
        _resourceSlots[toIndex] = source;
        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool TrySplitResourceStack(int fromIndex, int toIndex, int splitAmount)
    {
        if (!IsValidResourceSlot(fromIndex) || !IsValidResourceSlot(toIndex) || fromIndex == toIndex) return false;
        if (splitAmount <= 0) return false;

        var source = _resourceSlots[fromIndex];
        var target = _resourceSlots[toIndex];
        if (source is null || target is not null) return false;
        if (source.Value.Amount <= splitAmount) return false;

        _resourceSlots[fromIndex] = source.Value with { Amount = source.Value.Amount - splitAmount };
        _resourceSlots[toIndex] = new ItemStack(source.Value.ItemId, splitAmount);
        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool TryAutoSplitHalf(int fromIndex)
    {
        if (!IsValidResourceSlot(fromIndex)) return false;

        var source = _resourceSlots[fromIndex];
        if (source is null || source.Value.Amount < 2) return false;

        var emptySlot = FindFirstEmptyResourceSlot();
        if (emptySlot < 0) return false;

        var splitAmount = source.Value.Amount / 2;
        return TrySplitResourceStack(fromIndex, emptySlot, splitAmount);
    }

    public bool TryDropWeaponSlot(int index)
    {
        if (index < 0 || index >= _weaponSlots.Length || _weaponSlots[index] is null) return false;

        _weaponSlots[index] = null;
        OnInventoryChanged?.Invoke();
        return true;
    }

    public string? GetWeaponSlot(int index)
    {
        if (index < 0 || index >= _weaponSlots.Length) return null;
        return _weaponSlots[index];
    }

    public IReadOnlyList<string?> GetAllWeaponSlots() => _weaponSlots.ToArray();

    private int FindFirstEmptyResourceSlot()
    {
        for (var i = 0; i < _resourceSlots.Length; i++)
        {
            if (_resourceSlots[i] is null) return i;
        }

        return -1;
    }

    private bool IsValidResourceSlot(int index)
    {
        return index >= 0 && index < _resourceSlots.Length;
    }

    public readonly record struct ItemStack(string ItemId, int Amount);
}
