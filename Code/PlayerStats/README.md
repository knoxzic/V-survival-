# PlayerStats Folder Guide

Purpose:
- Owns health, stamina, hunger, and thirst simulation.

Main file:
- PlayerVitals.cs

How to link:
1. Call Vitals.Tick(deltaSeconds, isSprinting) each frame.
2. Bind HUD progress bars to Health, Stamina, Hunger, and Thirst values.
3. Use ApplyDamage and AddHealth from combat/healing systems.

Beginner note:
- Raise UI updates from OnVitalsChanged instead of polling when possible.
