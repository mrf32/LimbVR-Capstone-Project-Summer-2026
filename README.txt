# LimbVR – Upper-Limb Prosthetic Rehabilitation Simulator

## Overview

LimbVR is a Unity-based capstone project focused on improving upper-limb prosthetic rehabilitation through an interactive virtual environment.

The project simulates an upper-limb prosthetic hand performing object manipulation tasks while integrating external sensor data from a wearable prosthetic system. The long-term objective is to provide clinicians and researchers with a platform for evaluating prosthetic control strategies, collecting rehabilitation metrics, and improving patient outcomes.

This repository contains the Unity application responsible for visualization, gameplay, hardware communication, and data collection.

---

# Getting Started

## Prerequisites

Before opening the project, ensure you have the following installed:

- Unity 6 (or the version specified by the project)
- Unity Hub
- Git
- Visual Studio 2022 or JetBrains Rider with Unity support
- Windows (required for the current serial communication implementation)

---

## Clone the Repository

```bash
git clone https://github.com/mrf32/LimbVR-Capstone-Project-Summer-2026.git
```

---

## Open the Project

1. Open **Unity Hub**.
2. Click **Add Project**.
3. Select the cloned repository.
4. Open the project using the recommended Unity version.
5. Allow Unity to import all assets and packages.

> **Note:** The first import may take several minutes while Unity generates the `Library` folder and imports all assets.

---

## Required Unity Packages

Unity automatically restores all required packages from:

- `Packages/manifest.json`
- `Packages/packages-lock.json`

No additional package installation should be necessary.

---

## Hardware Configuration

The current prototype communicates with external hardware using serial communication.

Default configuration:

| Setting | Value |
|----------|-------|
| Serial Port | COM3 |
| Baud Rate | 115200 |

If using a different serial device, update the serial port inside the project accordingly.

The project can also be tested without external hardware using the built-in keyboard controls.

---

## Running the Project

1. Open the primary gameplay scene.
2. Press **Play** in the Unity Editor.
3. Move the virtual prosthetic hand.
4. Grasp the object.
5. Transport it into the green scoring area.
6. Continue until the session timer expires.

---

# Project Objectives

The current prototype allows participants to:

- Control a virtual prosthetic hand
- Receive real-time input from external sensors
- Grasp virtual objects
- Complete object placement tasks
- Receive performance feedback through scoring
- Record rehabilitation session data
- Evaluate prosthetic interaction performance

This Unity application serves as the software component of the LimbVR capstone project.

---

# Current Gameplay

Each rehabilitation session follows this workflow:

1. Move the prosthetic hand toward the virtual ball.
2. Enter the object's grasp region.
3. Activate the grasp input.
4. Pick up the object.
5. Move the object into the green target area.
6. Earn one point for a successful placement.
7. Spawn the next object.
8. Repeat until the session timer reaches zero.

---

# Features

Current functionality includes:

- Unity-based rehabilitation environment
- Serial communication with external hardware
- Real-time sensor data acquisition
- Keyboard testing controls
- Virtual prosthetic hand movement
- Object grasp detection
- Object placement scoring
- Session timer
- Random object spawning
- Performance data logging
- Score tracking
- User interface displaying score and timer

---

# Project Structure

```
LimbVR/
│
├── Assets/
│   ├── Scripts/
│   │   ├── Prosthetics.cs
│   │   ├── Object.cs
│   │   └── TrashCan.cs
│   │
│   ├── Prefabs/
│   ├── Scenes/
│   ├── Materials/
│   ├── Models/
│   ├── Textures/
│   └── UI/
│
├── Packages/
├── ProjectSettings/
├── .gitignore
├── .gitattributes
└── README.md
```

---

# Core Scripts

## Prosthetics.cs

Primary controller responsible for:

- Game management
- Serial communication
- Sensor parsing
- Target movement
- Robot movement
- Grasp state management
- Score calculation
- Session timer
- Random object spawning
- Performance data logging

---

## Object.cs

Determines whether the prosthetic hand is inside an object's grasp region.

This information is used by the game manager to determine whether an object can be picked up.

---

## TrashCan.cs

Detects when an object enters the scoring area.

Successful placement events are reported back to the game manager for score calculation.

---

# Controls

## Keyboard Controls (Development & Testing)

| Key | Action |
|------|--------|
| W / S | Move target |
| A / D | Move target |
| Space | Activate grasp |

These controls are intended primarily for testing while hardware integration is being refined.

---

# Data Logging

During gameplay, the application records:

- Sensor values
- Session time
- Player score

The collected data is written to a text file for later analysis and research.

---

# Unity Scene Requirements

Before running the project, verify the following:

- The green scoring area has a Collider with **Is Trigger** enabled.
- At least one interacting object contains a Rigidbody.
- Ball objects are tagged **Object**.
- Prosthetic hand colliders are tagged **Prosthetics**.
- Required Inspector references have been assigned.
- The correct scene is loaded.
- The serial device is connected (if using external hardware).

---

# Git Repository

This project uses standard Unity version control practices.

Tracked files include:

- Assets
- Packages
- ProjectSettings
- Source code
- Prefabs
- Scenes
- Materials
- Textures
- Models

The repository excludes Unity-generated files through `.gitignore`, including:

- Library/
- Temp/
- Logs/
- Obj/
- Build/
- Builds/
- UserSettings/

The repository also includes a `.gitattributes` file to ensure consistent line endings across development environments.

---

# Known Limitations

The current prototype is under active development.

Areas planned for improvement include:

- Event-based scoring system
- Improved object lifecycle management
- Safer serial communication
- Configurable hardware settings
- Improved data logging efficiency
- Better error handling
- Code refactoring
- Expanded rehabilitation exercises
- Additional gameplay mechanics
- Improved UI and user experience

---

# Future Development

Planned features include:

- Full prosthetic hardware integration
- VR headset support
- Improved grasp mechanics
- Multiple rehabilitation exercises
- Performance analytics dashboard
- Configurable rehabilitation sessions
- Cloud-based data storage
- Expanded clinician tools
- Improved object interaction physics
- Research-focused data visualization

---

# Development Team

**LimbVR**

Summer 2026 Senior Capstone Project

Developed as part of a multidisciplinary effort to explore virtual reality applications for upper-limb prosthetic rehabilitation.

---

# License

This repository is part of an academic senior capstone project.

Licensing and distribution terms will be determined upon project completion.
