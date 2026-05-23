# UI Folder Guide (Razor)

Purpose:
- Holds the Razor components that the player actually sees.
- In S&box 2026, this is where your HUD and inventory layout live.

Main files:
- [Hud.razor](Hud.razor): vitals and ammo overlay.
- [InventoryPanel.razor](InventoryPanel.razor): weapon slots plus 24-slot resource inventory.

Recommended build order:
1. Build the view models in `Code/UI`.
2. Build `Hud.razor`.
3. Build `InventoryPanel.razor`.
4. Hook the callbacks into your scene or UI controller.

How to link:
1. Feed values through `[Parameter]` properties.
2. Wire `OnMoveStack` to `InventoryViewModel.MoveResourceStack`.
3. Wire `OnSplitStackHalf` to `InventoryViewModel.SplitResourceStackHalf`.
4. Re-render when gameplay events tell the UI the state changed.

Beginner note:
- Razor components should request actions through callbacks.
- Razor should not own the real gameplay state.

Common beginner mistake:
- Putting game rules inside the `.razor` file.
- Keep rules in C# and presentation in Razor.
