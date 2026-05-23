# UI Folder Guide (Razor)

Purpose:
- Holds Razor components for HUD and inventory.

Main files:
- Hud.razor: vitals and ammo overlay.
- InventoryPanel.razor: weapon slots plus 24-slot resource inventory.

How to link:
1. Feed values through [Parameter] properties.
2. Wire drag/drop callback parameter OnMoveStack to InventoryViewModel.MoveResourceStack.
3. Wire split callback parameter OnSplitStackHalf to InventoryViewModel.SplitResourceStackHalf.

Beginner note:
- Razor components should request actions through callbacks, not mutate gameplay state directly.
