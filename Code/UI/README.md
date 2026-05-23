# Code/UI Folder Guide

Purpose:
- View-model layer between gameplay systems and Razor views.

Main files:
- HudViewModel.cs
- InventoryViewModel.cs

How to link:
1. Create view models with PlayerController.
2. Pass view-model values into Razor component parameters.
3. Call InventoryViewModel.MoveResourceStack and SplitResourceStackHalf from UI callbacks.

Beginner note:
- Keep rendering and input in Razor files; keep gameplay state changes in C# systems.
