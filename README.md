# VR Escape Room - MP1

A VR escape room game built with Unity and XR Interaction Toolkit for Meta Quest.

## Contributors
- Haibo Tong
- Babayev Riyad

## Description
A dungeon themed escape room where players must find and place three colored clues into their corresponding gates to escape.

## Features

### Core Features (20 pts)
- **Grab Affordance Clues**: 3 grabbable objects with XR Grab Interactables
- **Collider Environs**: Floor and surfaces have colliders for physics
- **Physics Objects**: All clues have Rigidbodies that fall and tumble
- **Unlocking Keys**: 3 Socket Interactors that trigger when correct items are placed
- **Escaping the Room**: Win condition triggers after all 3 gates are unlocked

### Optional Features
- UI Scoreboard showing progress (1 pt)
- Countdown timer with loss condition (1 pt)
- Restart button (1 pt)
- Start Scene with start button (2 pts)
- Win celebration with visual effects (2 pts)
- Collectibles system (2 pts)
- Secrets - hidden treasure revealed (2 pts)
- Doors and Keys - door opens on unlock (2 pts)
- Three Reads Lighting (3 pts)
- Red Herrings - 3 extra grabbable objects (2 pts)
- Themed Setting with dungeon assets (3 pts)
- Themed Props (3 pts)

## Controls

### VR Controls
- Grab objects with controller grip button
- Move using teleportation or continuous movement

### Desktop Testing
- WASD: Move
- Mouse: Look around
- Left Click: Grab/Release objects
- Right Click: Throw objects
- Scroll: Adjust hold distance
- Esc: Unlock cursor

## How to Play
1. Start from the Start Scene, click START button
2. Find the three colored clues (Red, Blue, Green)
3. Pick up each clue and place it in the matching colored gate
4. Collect floating collectibles for bonus points
5. Once all three clues are placed, you win!

## Build Instructions
1. Open project in Unity
2. File → Build Profiles
3. Select Android platform
4. Click Build to create APK

## Requirements
- Unity 2022.3 or later
- XR Interaction Toolkit 3.3.1
- Oculus XR Plugin

## Assets Used
- Decrepit Dungeon LITE (Unity Asset Store)
