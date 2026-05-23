# Inventory Folder Guide

Purpose:
- Stores item data, resource stacks, and the two dedicated weapon slots.

Main files:
- [ItemDefinition.cs](ItemDefinition.cs): item identity and stack rules.
- [InventorySystem.cs](InventorySystem.cs): pickup, drop, move, merge, and split operations.

Recommended build order:
1. `ItemDefinition`
2. `InventorySystem`
3. `InventoryViewModel`
4. `InventoryPanel.razor`

How to link:
1. Use `TryPickup` for world loot.
2. Use `TryMoveResourceStack(from, to)` for drag/drop movement.
3. Use `TryAutoSplitHalf(index)` or `TrySplitResourceStack(from, to, amount)` for split-stack behavior.
4. Keep weapon items in the two dedicated weapon slots.
5. Let `InventoryViewModel` expose read-only data to the Razor panel.
6. Let `InventoryPanel.razor` send actions back through callbacks.

Beginner note:
- Resource slots and weapon slots are intentionally separated.
- That keeps the UI easier to understand and makes hotbar or weapon-swap logic simpler later.

Common beginner mistake:
- Editing inventory state directly from Razor markup.
- The safer pattern is Razor -> callback -> view model -> inventory system.
