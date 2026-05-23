# PlayerStats Folder Guide

Purpose:
- Owns health, stamina, hunger, and thirst simulation.
- This is one of the first gameplay folders you should finish because the HUD depends on it.

Main file:
- [PlayerVitals.cs](PlayerVitals.cs)

Recommended build order:
1. `PlayerVitals`
2. `HudViewModel`
3. `Hud.razor`

How to link:
1. Call `Vitals.Tick(deltaSeconds, isSprinting)` once per frame.
2. Use `ApplyDamage` when enemies, hazards, or fall damage should hurt the player.
3. Use `AddHealth` when the player heals.
4. Bind the HUD progress bars to the current values.
5. Prefer `OnVitalsChanged` to refresh the UI instead of polling every control manually.

Beginner note:
- Keep the stat math in C#.
- Keep the on-screen bars in Razor.

Common beginner mistake:
- Putting hunger or thirst math into the HUD file.
- The HUD should display state, not own state.
