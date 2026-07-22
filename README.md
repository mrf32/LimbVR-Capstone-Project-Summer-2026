# LimbVR – Upper-Limb Prosthetic Rehabilitation Simulator


LimbVR is a Unity-based senior capstone project focused on improving upper-limb prosthetic rehabilitation through an interactive virtual environment.

The application integrates external sensor data from a wearable prosthetic system with a Unity simulation, allowing users to perform object manipulation tasks while collecting rehabilitation performance metrics. The project serves as a research and development platform for evaluating prosthetic control strategies, virtual rehabilitation exercises, and data collection workflows.

This repository contains the Unity application responsible for:

- Virtual rehabilitation environment
- Prosthetic hand interaction
- Serial communication with external hardware
- Gameplay and scoring
- Performance data logging
- Research prototype development

---

# Getting Started

## Prerequisites

Before opening the project, install:

- **Unity Hub**
- **Unity Editor 6000.0.41f1**
- Git
- Visual Studio 2022 (or JetBrains Rider) with Unity support
- Windows (required for the current serial communication implementation)

## Clone the Repository

```bash
git clone https://github.com/mrf32/LimbVR-Capstone-Project-Summer-2026.git
```

## Open the Project

1. Open Unity Hub.
2. Click **Add Project**.
3. Select the cloned repository.
4. Open it using **Unity 6000.0.41f1**.
5. Allow Unity to import all assets and restore project packages.

> The initial import may take several minutes while Unity generates the Library folder.

## Running the Prototype

1. Open the primary gameplay scene.
2. Press **Play**.
3. If hardware is connected, verify the serial connection.
4. If no hardware is connected, use the keyboard controls for testing.

Current keyboard controls:

- **W / S** – Move target
- **A / D** – Move target
- **Space** – Activate grasp

---


# LimbVR Current Code Overview

## 1. Project Purpose

This Unity prototype implements a simple upper-limb rehabilitation task:

1. The user moves a virtual prosthetic hand toward a ball.
2. The hand grasps the ball when the grasp input is active and the hand is inside the ball's grasp area.
3. The user moves the ball into the green target area.
4. A successful placement should add exactly one point.
5. The ball should then reappear at another location.
6. The session ends when the game timer reaches zero.

The current code package contains three C# scripts:

- `Prosthetics.cs`
- `Object.cs`
- `TrashCan.cs`

## 2. Script Responsibilities

### `Prosthetics.cs`

This is the main controller and game-management script.

It currently handles:

- Serial communication through `COM3` at `115200` baud.
- Parsing four comma-separated sensor values.
- Keyboard-based test control.
- Movement of the assigned `target` object.
- Movement of the assigned `robot` object toward the target.
- Grasp-state calculation.
- Game timer and score display.
- Score calculation.
- Random object placement and prefab spawning.
- Logging sensor values, time, and score to a text file.

Important public references that must be assigned in the Unity Inspector:

- `robot`
- `target`
- `Script`
- `Script2`
- `spherePrefab`
- `timerText`
- `scoreText`

Current keyboard controls:

- Vertical axis: translates the target.
- Horizontal axis: moves the target on another axis.
- Space bar: activates the grasp input.

The serial sensor values are stored as:

- `vlxFloat1`
- `vlxFloat2`
- `vlxFloat3`
- `vlxFloat4`

### `Object.cs`

This script detects whether the prosthetic hand is inside the object's grasp region.

Behavior:

- When a collider tagged `Prosthetics` remains inside the trigger, `stayInGraspDomain` becomes `1`.
- When that collider exits, `stayInGraspDomain` becomes `0`.

### `TrashCan.cs`

This script detects whether an object is inside the green scoring area.

Behavior:

- When a collider tagged `Object` enters the trigger, `InTrashCan` becomes `true`.
- When it exits, `InTrashCan` becomes `false`.

## 3. Current Gameplay Logic

The current grasp logic is approximately:

```text
Grasp input active
+ prosthetic hand inside the object's grasp domain
= graspStatus true
```

When grasping is active and the hand is in range, the assigned `robot` GameObject moves toward the assigned `target` position using `Vector3.MoveTowards()`.

The current scoring condition is:

```csharp
if (Script2.InTrashCan && vlxFloat4 < 60 && gameTimer > 0f)
```

When this condition is true, the code:

1. Moves the assigned `robot` to a random position.
2. Adds one point.
3. Creates another sphere from `spherePrefab` at a random position.

## 4. Confirmed Scoring Bug

### Symptom

One ball placement can sometimes add several points and create several balls, even though each placement should add only one point.

### Direct Cause

The scoring code is inside Unity's `Update()` method, which runs once per rendered frame.

`TrashCan.InTrashCan` remains `true` for as long as an object stays inside the green trigger area.

## 5. Recommended Fix

The scoring event should occur once when a valid ball first enters the green area rather than being checked continuously every frame.

Conceptual flow:

```text
Ball enters green trigger
        ↓
OnTriggerEnter fires once
        ↓
Validate ball and game state
        ↓
Add one point
        ↓
Remove or reposition that ball
        ↓
Create or activate one new ball
```

## 6. Other Technical Notes

- Prefer `&&` over `&` for Boolean conditions.
- Avoid blocking `serial.ReadLine()` directly inside `Update()`.
- Validate serial array lengths before indexing.
- Buffer or rate-limit file logging.
- Make COM port, baud rate, and output path configurable.
- Clamp the timer at zero after expiration.

## 7. Unity Setup Checklist

Verify that:

- The scoring area collider has **Is Trigger** enabled.
- At least one object has a Rigidbody.
- The ball is tagged `Object`.
- The prosthetic collider is tagged `Prosthetics`.
- Inspector references are assigned correctly.
- `spherePrefab` is configured correctly.
- Spawn locations do not overlap the scoring area.

## 8. Current Conclusion

The primary confirmed issue is that the score-and-spawn logic executes every frame inside `Update()` while `InTrashCan` remains true.

The next revision should move scoring to an event-driven approach using `OnTriggerEnter()`, identify the exact ball that triggered the score, and ensure that only one ball is removed or spawned per successful placement.
