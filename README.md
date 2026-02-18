# VR Escape Room - MP1

A VR escape room game built with Unity and XR Interaction Toolkit for Meta Quest.

## Contributors
- [Your Name Here]
- [Partner Name Here]

## Description
A wizard's dungeon themed escape room where players must find and place three magical crystals (Clues) into their corresponding altars (Gates) to escape.

## Features

### Core Features
- **Grab Affordance Clues**: 3 grabbable objects with XR Grab Interactables
- **Collider Environs**: Floor and surfaces have colliders for physics
- **Physics Objects**: All clues have Rigidbodies that fall and tumble
- **Unlocking Keys**: 3 Socket Interactors that trigger when correct items are placed
- **Escaping the Room**: Win condition triggers after all 3 gates are unlocked

### Optional Features
- UI Scoreboard showing progress
- Countdown timer with loss condition
- Win celebration panel

## Controls

### VR Controls
- Grab objects with controller grip button
- Move using teleportation or continuous movement

### Desktop Testing (XR Device Simulator)
- WASD: Move
- Mouse: Look around
- Left Click: Grab/Release objects
- Right Click: Throw objects
- Scroll: Adjust hold distance
- Esc: Unlock cursor

## How to Play
1. Find the three colored crystals (Red, Blue, Green)
2. Pick up each crystal
3. Place each crystal in its matching colored altar
4. Once all three are placed, you win!

## Build Instructions
1. Open project in Unity
2. File → Build Settings
3. Select Android platform
4. Click Build to create APK

## Requirements
- Unity 2022.3 or later
- XR Interaction Toolkit 3.3.1
- Oculus XR Plugin
