# Console Game of Life

A modern C# implementation of Conway's Game of Life using .NET 8 and latest C# features.

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
- Top-level statements
- Enhanced console rendering with colors
- Pattern library with classic Game of Life patterns

## Installation

1. Clone this repository:
```bash
git clone https://github.com/rgaleevbc/console-game-of-life.git
cd console-game-of-life
```

2. Build and run:
```bash
dotnet run --configuration Release
```

## Controls

- SPACEBAR: Pause/Resume the simulation
- R: Randomize the grid
- C: Clear the grid
- P: Load next pattern from pattern library
- Arrow keys: Adjust simulation speed
- Q: Quit the game

## Project Structure

- `Program.cs`: Entry point and top-level statements
- `GameEngine.cs`: Core game logic and rules implementation
- `Grid.cs`: Immutable grid implementation with pattern matching
- `Renderer.cs`: Console rendering and user interface
- `Patterns.cs`: Built-in pattern library
- `Models/`: Data models and record structs

## License

This project is licensed under the MIT License - see the LICENSE file for details.