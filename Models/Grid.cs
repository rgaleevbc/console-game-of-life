namespace ConsoleGameOfLife.Models;

public readonly record struct Grid
{
    private readonly bool[,] _cells;

    public Grid(int rows, int cols)
    {
        _cells = new bool[rows, cols];
        Rows = rows;
        Cols = cols;
    }

    public int Rows { get; }
    public int Cols { get; }

    public bool this[Cell cell]
    {
        get => IsValidCell(cell) ? _cells[cell.Row, cell.Col] : false;
        init => _cells[cell.Row, cell.Col] = value;
    }

    public bool IsValidCell(Cell cell) =>
        cell.Row >= 0 && cell.Row < Rows && cell.Col >= 0 && cell.Col < Cols;

    public Grid WithCell(Cell cell, bool value)
    {
        if (!IsValidCell(cell)) return this;
        
        var newGrid = new Grid(Rows, Cols);
        Array.Copy(_cells, newGrid._cells, _cells.Length);
        newGrid._cells[cell.Row, cell.Col] = value;
        return newGrid;
    }

    public Grid WithRandomCells(double probability = 0.3)
    {
        var random = new Random();
        var newGrid = new Grid(Rows, Cols);
        
        for (int row = 0; row < Rows; row++)
            for (int col = 0; col < Cols; col++)
                newGrid._cells[row, col] = random.NextDouble() < probability;
        
        return newGrid;
    }

    public IEnumerable<Cell> GetLiveCells()
    {
        for (int row = 0; row < Rows; row++)
            for (int col = 0; col < Cols; col++)
                if (_cells[row, col])
                    yield return new Cell(row, col);
    }
}