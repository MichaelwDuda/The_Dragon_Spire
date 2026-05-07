# The Dragon Spire

<<<<<<< HEAD
The Dragon Spire is an in-development Unity tower-defense / first-person action prototype. The current game loop has the player defending a home tower while enemies spawn in waves, travel along waypoint paths, and take damage from both player weapons and placed towers.

This README is written for contributors who need to open the project, understand the main systems, and make changes without having to reverse-engineer the scene setup first.

## Current Status

This project is an active prototype. The repository contains working gameplay systems for first-person movement, weapon switching, enemy waves, enemy health, tower attacks, tower upgrades, and a basic lose condition. It does not currently document or imply a finished release flow, scoring system, save system, main menu, or packaged build.

## Requirements

- Unity Editor `6000.3.10f1`
- Unity Hub, recommended for installing and opening the exact editor version
- A machine that can run Unity's Universal Render Pipeline

Important packages currently listed in `Packages/manifest.json` include:

- Universal Render Pipeline `17.3.0`
- Input System `1.18.0`
- TextMesh Pro / UGUI
- ProBuilder `6.0.9`
- Visual Effect Graph `17.3.0`
- Unity Test Framework `1.6.0`
- AI Navigation `2.0.10`

## Getting Started

1. Clone the repository.
2. Open Unity Hub.
3. Add the project folder.
4. Open it with Unity `6000.3.10f1`.
5. Let Unity import the project and restore packages from `Packages/manifest.json`.
6. Open `Assets/Scenes/SampleScene.unity`.
7. Press Play.

If Unity asks whether to use the new Input System, keep the project on the Input System package. The active input settings are referenced from `ProjectSettings/EditorBuildSettings.asset`.

## Running The Main Scene

`Assets/Scenes/SampleScene.unity` is the build-enabled scene and the best starting point for the current gameplay loop. It includes the player, camera, weapon setup, enemy spawner, home tower, path objects, lighting, global volume, and input event system.

Other development scenes exist for focused testing:

- `Assets/Scenes/player_test.unity` contains player and tower testing setup.
- `Assets/Scenes/tower_Testing.unity` contains tower, enemy, UI, and path testing setup.

## Gameplay Overview

The player starts in a first-person view and defends the home tower. Enemies are spawned by the enemy spawner, assigned a waypoint route, and move toward the home tower. When an enemy reaches the end of the path, it damages the home tower and is destroyed.

The player can attack enemies directly with weapons and can interact with towers to open upgrade UI. Towers independently find enemies in range and deal damage according to their configured stats.

The home tower tracks health. When its health reaches zero, the current game pauses with `Time.timeScale = 0f`.

## Controls

| Action | Input |
| --- | --- |
| Move | `W`, `A`, `S`, `D` |
| Look | Mouse movement |
| Jump | `Space` |
| Attack | Left mouse button |
| Select weapon slot 1 | `1` |
| Select weapon slot 2 | `2` |
| Cycle weapons | Mouse wheel or `Tab` |
| Interact with highlighted tower | `E` |

The project includes input action assets at `Assets/InputSystem_Actions.inputactions` and `Assets/scripts/PlayerInput.inputactions`. The player controller uses `InputActionReference` fields for movement, look, jump, and attack actions, while weapon cycling and tower interaction currently read keyboard and mouse state directly.

## Core Systems

### Player Movement

`FirstPersonController` drives the active first-person movement. It uses a `CharacterController`, reads movement and look input through Unity's Input System, locks the cursor during gameplay, supports jumping, applies gravity, and allows reduced air control while airborne.

There is also a `Player` script with older Rigidbody-style movement code currently commented out. Treat `FirstPersonController` as the active movement implementation unless the scene setup changes.

### Weapons

`WeaponManager` switches between weapon GameObjects and visuals. It supports direct number-key selection, mouse-wheel cycling, and `Tab` cycling.

Current weapon behavior includes:

- `GunController`: spawns a bullet prefab from a bullet spawn transform and gives it forward velocity.
- `BulletBehavior`: damages an `EnemyHealth` target on trigger contact, then destroys the bullet.
- `FireBreathController`: plays or stops the fire breath particle system while attack is held and toggles a hitbox.
- `FireBreathBehavior`: applies repeated tick damage to enemies inside the fire breath hitbox.

### Enemies

`EnemySpawner` owns the wave configuration. Each wave chooses an enemy prefab, enemy count, spawn delay, and delay before the next wave. Spawned enemies receive the scene's waypoint list and home tower reference.

`Enemy_move` moves enemies from waypoint to waypoint. When an enemy reaches the final waypoint, it damages the home tower and destroys itself.

`EnemyHealth` tracks health, handles damage, invokes `onDied`, disables movement and collision on death, and destroys the enemy after an optional delay.

`EnemyAnimationController` supports Animator-driven enemies when an Animator Controller is present. It also has a procedural fallback for simple bobbing, hit pulsing, and death animation when no playable Animator Controller is available.

### Home Tower

`HomeTowerHealth` tracks the home tower's health. It logs damage, pauses the game on death, and contains a height-scaling helper for health visualization that is not currently called by `TakeDamage`.

### Towers And Upgrades

`TowerBase` stores shared tower stats: damage, range, fire rate, and upgrade costs. Its upgrade methods increase damage and range, reduce fire interval, and raise future upgrade costs.

`TowerAttack` searches for the nearest enemy tagged `enemy` within range, damages it when ready, and can spawn a projectile effect.

`AOETower` periodically damages every enemy tagged `enemy` inside range and can spawn an area effect prefab.

`TowerDetector` raycasts from the player/camera direction to find towers, applies highlighting, and opens the upgrade UI when `E` is pressed. `TowerHighlight` changes tower material color while highlighted.

`UpgradeUI` displays the selected tower's stats and costs, calls the tower upgrade methods, and unlocks or relocks the cursor when the panel opens or closes.

## Project Structure

| Path | Purpose |
| --- | --- |
| `Assets/scripts` | Main gameplay C# scripts and generated input wrapper code. |
| `Assets/Scenes` | Main and test scenes. `SampleScene` is currently build-enabled. |
| `Assets/Towers` | Tower prefabs, projectile/effect prefabs, materials, and upgrade panel prefab. |
| `Assets/enemy` | Enemy prefabs, waypoint prefabs, imported monster assets, and enemy setup notes. |
| `Assets/Player` | Player weapon prefabs such as bullets, fire breath, and hitbox assets. |
| `Assets/EffectExamples` | Imported visual effect example assets used for particles and effects. |
| `Assets/Settings` | URP renderer, pipeline, profile, and volume settings. |
| `Packages` | Unity package manifest and lock file. |
| `ProjectSettings` | Unity editor, build, input, renderer, and project settings. |

## Asset Notes

The project includes imported effect example content and a monster enemy asset under `Assets/enemy/UltimateMonsters/BigDemon`. Keep third-party asset licenses with their source folders.

For adding or replacing Asset Store enemies, see `Assets/enemy/AssetStoreEnemySetup.md`. In short, duplicate the imported enemy prefab into `Assets/enemy/`, tag it as `enemy`, add the gameplay components, keep or configure its Animator, and assign it to the `Enemy Spawner` waves.

Enemy Animator Controllers should use these parameter names when possible:

- Float: `Speed`
- Trigger: `Hit`
- Trigger: `Attack`
- Trigger: `Die`

## Development Workflow

- Start from `SampleScene` when validating the full game loop.
- Use the smaller test scenes when changing isolated player or tower behavior.
- Keep enemy prefabs tagged `enemy`; tower targeting depends on that tag.
- Check serialized references in the Inspector after moving prefabs or replacing scene objects.
- When adding a new weapon, register both its behavior object and visual object in `WeaponManager`.
- When adding a tower type, inherit from `TowerBase` if it should use the existing upgrade UI.
- Keep package and Unity version changes intentional, since Unity project version drift can create noisy asset and settings changes.

## Troubleshooting

### Unity Opens With The Wrong Version

Install Unity `6000.3.10f1` through Unity Hub and reopen the project with that version. The expected editor version is recorded in `ProjectSettings/ProjectVersion.txt`.

### Input Does Not Work

Confirm the Input System package is installed and enabled. Check that the player's `FirstPersonController`, `GunController`, and `FireBreathController` have their `InputActionReference` fields assigned in the scene or prefab.

### Towers Do Not Detect Enemies

Confirm enemy GameObjects are tagged `enemy`. Both `TowerAttack` and `AOETower` search by tag.

### Tower Upgrade UI Does Not Open

Confirm the player or camera object has `TowerDetector`, the tower is on the configured tower layer, the tower has `TowerHighlight` and `TowerBase`, and `UpgradeUI` is assigned.

### Enemies Do Not Move

Confirm the `EnemySpawner` has waypoints assigned, the enemy prefab has `Enemy_move`, and the spawned enemy receives a valid home tower reference. `Enemy_move` disables itself if no waypoints are available.

### Fire Breath Does Not Damage Enemies

Confirm the hitbox is active while attacking, has a trigger collider, and has `FireBreathBehavior`. Enemies must have `EnemyHealth` on the same object hit by the trigger, or the damage lookup will not find them.

## Future Improvement Ideas

These are possible next steps, not currently documented as finished features:

- Add score, currency, or reward spending to make tower upgrade costs meaningful.
- Add game over UI and restart flow instead of only pausing time.
- Add health UI for the home tower and enemies.
- Add wave completion and victory conditions.
- Add a main menu and build/export instructions.
- Consolidate input action assets so all controls follow one input path.
- Add automated tests for tower damage, enemy path completion, and weapon damage.
=======
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
>>>>>>> 535c7aab85a83da9b2f7dde91c658afba6ea3ce3
