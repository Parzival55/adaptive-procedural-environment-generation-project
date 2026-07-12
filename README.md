# Unity Version 6000.1.6f1

# Adaptive Procedural Environment Generation Framework

## Overview

A modular procedural environment generation framework

## Features

- Adaptive generation profiles
- Deterministic seed generation
- Modular generation pipeline
- Procedural rooms
- Corridor generation
- Wall generation
- Generation statistics

## Architecture

Core components:

- GridManager
- GenerationPipeline
- RoomGenerator
- CorridorGenerator
- WallGenerator
- SeedManager
- StatisticsCalculator

# Quick Start

1. Open Main Scene

2. Press Play

3. Select a Theme

4. Select a Gameplay Profile

5. Click Generate

6. Explore the generated environment (Scene)

7. Inspect CastleTheme or CaveTheme ScriptableObjects in Assets/Themes to customise the toolkit.

# Export

- Most previous generation is exported automatically to a JSON file with Seed + build specifics
 To Find
        Project/
            APEG/
              Export/ 
                 Latest.Layout.json

    

# Extra Info #

# Opening The Project

Open the project in Unity Hub.

Open the Main scene.

Press Play.

# Using The Toolkit

1. Select a Gameplay Profile
    - Exploration
    - Combat
    - Survival

2. Select an Environment Theme
    - Medieval Castle
    - Cave

3. Press Generate Environment

4. A new procedural world will be created.

# Viewing Theme Configuration

Both environment themes are implemented using Unity ScriptableObjects.

They can be found at:

Assets/
    Themes/
        CastleTheme.asset
        CaveTheme.asset

Selecting either asset in the Unity Project window will display the Inspector, where developers can view and modify:

• Floor prefabs
• Wall prefabs
• Decorations
• Lighting
• Room features
• Rendering settings

* Even more variables available


