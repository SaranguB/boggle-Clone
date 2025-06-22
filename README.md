Overview

The goal of this project was to create a scalable and extensible version of a Word Boggle-style game using Unity. The focus was on clean architecture, modularity, and data-driven design, allowing future features to be integrated easily without major structural changes.

Architecture & Design Decisions

Game Management

A central GameManager singleton handles the overall game lifecycle, event services, and service initialization.

UI transitions and mode selection are managed through dedicated UI Controllers and Service classes.

MVC Pattern

The project is structured using the Model-View-Controller (MVC) pattern to ensure clear separation of concerns:

Models represent the data (e.g., ScriptableObjects for modes and tiles).

Views handle Unity-specific visual representation.

Controllers manage the logic and communication between model and view.

Modular UI System

UI is split into reusable components such as:

MainMenuUIController

GameModeUIController

Mode-specific views for Endless and Level modes

Game Modes

Endless Mode:

Generates a 4x4 grid of letters

Players form words by dragging over letters

Score is calculated based on word length

After forming a word, used tiles are removed and replaced from the top (falling letter effect)

Level Mode (Partially Implemented):

Level data is loaded from JSON and parsed into LevelModeSo using a converter

Static grid and special tiles like bonus and blocked tiles are supported

Level objectives is not fully implemented

Data-Driven Design

ScriptableObjects are used for:

Endless mode data

Level mode data (via runtime conversion from JSON)

Tile configuration

This allows designers to define level and tile content without modifying code.

Object Pooling

A custom generic object pooling system is implemented to reuse tiles efficiently and reduce overhead during gameplay.

What Would I Do With More Time

If more time was available, I would:

Implement all level mode objectives (e.g., reach score, collect bonus tiles, unblock blocked tiles)

Add sound effects and background music

Introduce smooth animated transitions between UI states and gameplay

Polish UI feedback (tile selection animations, score popups)

Add proper fail/success conditions for levels
