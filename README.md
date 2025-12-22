# Project Poly
This is a prototype of a top-down, wave-based, horde shooter built in Godot 4.4 (.NET/C#). It was inspired by misleading mobile game ads that advertise fake gameplay.

## Gameplay Overview
- Start with a single controllable troop
- Fight back waves of enemies that get more difficult as time passes
- Recruit more troops to ensure you have enough firepower
- See how long you can survive the apocalypse!

## Technical Notes
- Built with C# using Godot 4.4
- Used modular systems where applicable (entity movement behavior, health component, level creation, etc.)
- Various programming patterns used to simplify development
  - Observer pattern for decoupling of separate systems
  - Singleton pattern for global systems (audio manager, scene manager, event bus, settings)
  - Strategy pattern to allow for different behavior between entities
- UI elements (credits and tutorial) generated from JSON data
- Procedural generation of enemy waves after the first ten
