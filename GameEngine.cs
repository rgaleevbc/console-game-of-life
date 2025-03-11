using ConsoleGameOfLife.Models;

namespace ConsoleGameOfLife;

public class GameEngine
{
    private static readonly (int row, int col)[] Neighbors =
    {
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1),           (0, 1),
        (1, -1),  (1, 0),  (1, 1)
    };

    private Grid _grid;
    private readonly int _rows;
    private readonly int _cols;

    public GameEngine(int rows = 50, int cols = 100)
    {
        _rows = rows;
        _cols = cols;
        _grid = new Grid(rows, cols);
    }

    public Grid CurrentGrid => _grid;

    public void RandomizeGrid() =>
        _grid = _grid.WithRandomCells();

    public void ClearGrid() =>
        _grid = new Grid(_rows, _cols);

    public void NextGeneration()
    {
        var newGrid = new Grid(_rows, _cols);

        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                var cell = new Cell(row, col);
                var liveNeighbors = CountLiveNeighbors(cell);
                var isAlive = _grid[cell];

                newGrid = newGrid.WithCell(cell, (isAlive, liveNeighbors) switch
                {
                    (true, < 2) => false,    // Underpopulation
                    (true, > 3) => false,    // Overpopulation
                    (true, _) => true,       // Survival
                    (false, 3) => true,      // Reproduction
                    _ => false               // Remains dead
                });
            }
        }

        _grid = newGrid;
    }

    private int CountLiveNeighbors(Cell cell) =>
        Neighbors.Count(offset => _grid[cell + offset]);

    public void LoadPattern(IEnumerable<Cell> pattern)
    {
        var newGrid = new Grid(_rows, _cols);
        foreach (var cell in pattern)
        {
            if (newGrid.IsValidCell(cell))
            {
                newGrid = newGrid.WithCell(cell, true);
            }
        }
        _grid = newGrid;
    }
}