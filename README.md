# Game of Life

A modern C# implementation of Conway's Game of Life using .NET 8, featuring both console and desktop versions.

## Description

Conway's Game of Life is a cellular automaton that follows simple rules to create complex patterns. The game is played on a grid where each cell can be either alive or dead. The state of each cell in the next generation is determined by its current state and the number of live neighbors it has.

### Rules

1. Any live cell with fewer than two live neighbors dies (underpopulation)
2. Any live cell with two or three live neighbors lives on to the next generation
3. Any live cell with more than three live neighbors dies (overpopulation)
4. Any dead cell with exactly three live neighbors becomes a live cell (reproduction)

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Features

- Modern C# 12 syntax
- Immutable game state
- Pattern record structs
- Shared core library
- Two user interfaces:
  - Console version with colored text rendering
  - Desktop version with Avalonia UI
- Interactive grid editing
- Pattern library with classic Game of Life patterns
- Adjustable simulation speed

## Installation

1. Clone this repository:
```bash
git clone https://github.com/rgaleevbc/console-game-of-life.git
cd console-game-of-life
```

2. Run the console version:
```bash
dotnet run --project ConsoleGameOfLife.csproj --configuration Release
```

3. Or run the desktop version:
```bash
dotnet run --project GameOfLife.Desktop/GameOfLife.Desktop.csproj --configuration Release
```

## Controls

### Console Version
- SPACEBAR: Pause/Resume the simulation
- R: Randomize the grid
- C: Clear the grid
- P: Load next pattern from pattern library
- Arrow keys: Adjust simulation speed
- Q: Quit the game

### Desktop Version
- Start/Stop button: Control simulation
- Clear button: Clear the grid
- Random button: Generate random pattern
- Next Pattern button: Load next pattern from library
- Speed slider: Adjust simulation speed
- Click/drag on grid: Toggle cells manually

## Project Structure

The solution is organized into three projects:

### GameOfLife.Core
Core library containing shared game logic:
- `GameEngine.cs`: Core game logic and rules implementation
- `Models/Grid.cs`: Immutable grid implementation
- `Models/Cell.cs`: Cell record struct
- `Patterns.cs`: Built-in pattern library

### ConsoleGameOfLife
Console interface implementation:
- `Program.cs`: Entry point with top-level statements
- `Renderer.cs`: Console rendering and user interface

### GameOfLife.Desktop
Avalonia UI desktop interface:
- `MainWindow.axaml`: UI layout and styling
- `MainWindow.axaml.cs`: Window logic and event handling
- `App.axaml`: Application styling
- `Program.cs`: Desktop application entry point

## License

This project is licensed under the MIT License - see the LICENSE file for details.