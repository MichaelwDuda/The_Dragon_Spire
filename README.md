# The Dragon Spire

## Overview

This is a first-person tower defense game built in Unity. The player places and tests defensive towers while surviving waves of enemies. The game combines FPS controls with defensive ability systems like gunfire and fire breath.

## Core Features

* First-person player controller
* Tower-based defense gameplay
* Projectile weapon system (gun)
* Fire breath area attack using particle system
* Enemy health and damage system
* Trigger-based hit detection for attacks
* Test scene for rapid iteration

## Controls

* Left Mouse Button: Attack / Fire weapon
* Hold Attack: Activate fire breath (if equipped)
* Movement: Standard FPS controls (WASD + mouse look)

## Systems Overview

### Player Combat

The player can use different attack types:

* Gun: Fires projectiles with physics-based movement
* Fire Breath: Uses a particle system with a trigger hitbox for damage over time

### Enemies

Enemies use a simple health system. They take damage from:

* Bullet collisions
* Fire breath trigger damage zones

### Towers

Towers are placed objects that can be tested in a dedicated scene. They use prefab-based setup for reuse across scenes.

## Project Structure

* `Player/` - Player prefabs for the Bullet and Fire Breath.
* `Enemy/` - Enemy prefabs and waypoints.
* `Scripts/` - All different scripts for weapons, enemy, and towers.
* `Scenes/` - Test and main gameplay scenes
* `Towers/` - Tower prefabs for all the different towers and their structures.

## Setup Notes

* Ensure EventSystem exists in UI scenes
* Input System package is required
* Prefabs must be assigned correctly in the Inspector
* Hitboxes require trigger colliders and Rigidbody setup

## Known Issues

* UI input depends on correct InputSystemUIInputModule setup
* Prefab references must be assigned per scene instance
* Physics-based attacks require correct layer collision settings

## Future Improvements

* Enemy wave system
* Tower upgrade system
* Better VFX and hit feedback
* Object pooling for projectiles
