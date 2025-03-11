namespace ConsoleGameOfLife.Models;

public readonly record struct Cell(int Row, int Col)
{
    public static Cell operator +(Cell cell, (int row, int col) offset) =>
        new(cell.Row + offset.row, cell.Col + offset.col);
}