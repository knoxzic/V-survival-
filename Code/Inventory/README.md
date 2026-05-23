# Inventory Folder Guide

Purpose:
- Stores item data, resource stacks, and dedicated weapon slots.

Main files:
- ItemDefinition.cs: item identity and stack rules.
- InventorySystem.cs: pickup/drop plus move and split operations.

How to link:
1. Use TryPickup for world loot.
2. For UI drag/drop, call TryMoveResourceStack(from, to).
3. For split-stack behavior, call TryAutoSplitHalf(index) or TrySplitResourceStack(from, to, amount).
4. Keep weapon items in the two dedicated weapon slots.

Beginner note:
- Resource slots and weapon slots are intentionally separated to match survival game UX.
