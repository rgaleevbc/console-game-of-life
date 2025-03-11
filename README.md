# Console Game of Life

A console-based implementation of Conway's Game of Life written in Python.

## Description

Conway's Game of Life is a cellular automaton that follows simple rules to create complex patterns. The game is played on a grid where each cell can be either alive or dead. The state of each cell in the next generation is determined by its current state and the number of live neighbors it has.

### Rules

1. Any live cell with fewer than two live neighbors dies (underpopulation)
2. Any live cell with two or three live neighbors lives on to the next generation
3. Any live cell with more than three live neighbors dies (overpopulation)
4. Any dead cell with exactly three live neighbors becomes a live cell (reproduction)

## Requirements

- Python 3.8 or higher

## Installation

1. Clone this repository:
```bash
git clone https://github.com/rgaleevbc/console-game-of-life.git
cd console-game-of-life
```

2. Install the required packages:
```bash
pip install -r requirements.txt
```

## Usage

Run the game using:
```bash
python game_of_life.py
```

Controls:
- Press SPACE to pause/resume the simulation
- Press R to randomize the grid
- Press C to clear the grid
- Press Q to quit

## License

This project is licensed under the MIT License - see the LICENSE file for details.