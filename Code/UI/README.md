# Code/UI Folder Guide

Purpose:
- This is the bridge between gameplay systems and Razor views.
- It keeps the UI simple and keeps your gameplay code from getting tangled up in markup.

Main files:
- [HudViewModel.cs](HudViewModel.cs)
- [InventoryViewModel.cs](InventoryViewModel.cs)

Recommended build order:
1. `HudViewModel`
2. `InventoryViewModel`
3. `UI/Hud.razor`
4. `UI/InventoryPanel.razor`

How to link:
1. Construct the view models from `PlayerController`.
2. Pass view-model values into Razor component parameters.
3. Use UI callbacks to call `MoveResourceStack` and `SplitResourceStackHalf`.
4. Use events from gameplay systems to refresh the UI when values change.

Beginner note:
- This folder should only expose data and actions.
- It should not contain the actual HUD layout or the actual inventory layout.

Common beginner mistake:
- Skipping the view-model layer and trying to make Razor talk directly to every gameplay class.
- The view-model layer keeps the project easier to maintain.
