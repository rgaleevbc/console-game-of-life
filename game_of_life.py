import numpy as np
import os
import time
import keyboard
import random

class GameOfLife:
    def __init__(self, width=50, height=25):
        self.width = width
        self.height = height
        self.grid = np.zeros((height, width), dtype=bool)
        self.paused = False
        self.running = True

    def randomize(self):
        """Randomly populate the grid"""
        self.grid = np.random.choice([True, False], size=(self.height, self.width), p=[0.3, 0.7])

    def clear(self):
        """Clear the grid"""
        self.grid = np.zeros((self.height, self.width), dtype=bool)

    def count_neighbors(self, row, col):
        """Count the number of live neighbors for a cell"""
        total = 0
        for i in range(-1, 2):
            for j in range(-1, 2):
                if i == 0 and j == 0:
                    continue
                r = (row + i) % self.height
                c = (col + j) % self.width
                total += self.grid[r, c]
        return total

    def next_generation(self):
        """Calculate the next generation based on Conway's Game of Life rules"""
        new_grid = np.copy(self.grid)
        for row in range(self.height):
            for col in range(self.width):
                neighbors = self.count_neighbors(row, col)
                if self.grid[row, col]:  # Live cell
                    if neighbors < 2 or neighbors > 3:
                        new_grid[row, col] = False
                else:  # Dead cell
                    if neighbors == 3:
                        new_grid[row, col] = True
        self.grid = new_grid

    def draw(self):
        """Draw the current state of the grid"""
        os.system('cls' if os.name == 'nt' else 'clear')
        for row in self.grid:
            print(''.join('█' if cell else ' ' for cell in row))
        print("\nControls:")
        print("SPACE - Pause/Resume")
        print("R - Randomize")
        print("C - Clear")
        print("Q - Quit")
        print(f"Status: {'Paused' if self.paused else 'Running'}")

    def handle_input(self):
        """Handle keyboard input"""
        if keyboard.is_pressed('space'):
            self.paused = not self.paused
            time.sleep(0.2)  # Debounce
        elif keyboard.is_pressed('r'):
            self.randomize()
            time.sleep(0.2)
        elif keyboard.is_pressed('c'):
            self.clear()
            time.sleep(0.2)
        elif keyboard.is_pressed('q'):
            self.running = False

    def run(self):
        """Main game loop"""
        self.randomize()  # Start with a random grid
        while self.running:
            self.draw()
            self.handle_input()
            if not self.paused:
                self.next_generation()
            time.sleep(0.1)

if __name__ == "__main__":
    try:
        game = GameOfLife()
        game.run()
    except KeyboardInterrupt:
        print("\nGame terminated by user")
    finally:
        os.system('cls' if os.name == 'nt' else 'clear')