using ConsoleGameOfLife.Models;

namespace ConsoleGameOfLife;

public static class Patterns
{
    public static readonly IReadOnlyList<(string Name, Cell[] Pattern)> All = new[]
    {
        (
            "Glider",
            new[]
            {
                new Cell(0, 1),
                new Cell(1, 2),
                new Cell(2, 0),
                new Cell(2, 1),
                new Cell(2, 2)
            }
        ),
        (
            "Blinker",
            new[]
            {
                new Cell(1, 0),
                new Cell(1, 1),
                new Cell(1, 2)
            }
        ),
        (
            "Block",
            new[]
            {
                new Cell(0, 0),
                new Cell(0, 1),
                new Cell(1, 0),
                new Cell(1, 1)
            }
        ),
        (
            "Beacon",
            new[]
            {
                new Cell(0, 0),
                new Cell(0, 1),
                new Cell(1, 0),
                new Cell(1, 1),
                new Cell(2, 2),
                new Cell(2, 3),
                new Cell(3, 2),
                new Cell(3, 3)
            }
        ),
        (
            "Pulsar",
            new[]
            {
                // Top
                new Cell(0, 2), new Cell(0, 3), new Cell(0, 4),
                new Cell(0, 8), new Cell(0, 9), new Cell(0, 10),
                // Upper middle
                new Cell(2, 0), new Cell(3, 0), new Cell(4, 0),
                new Cell(2, 5), new Cell(3, 5), new Cell(4, 5),
                new Cell(2, 7), new Cell(3, 7), new Cell(4, 7),
                new Cell(2, 12), new Cell(3, 12), new Cell(4, 12),
                // Lower middle
                new Cell(7, 0), new Cell(8, 0), new Cell(9, 0),
                new Cell(7, 5), new Cell(8, 5), new Cell(9, 5),
                new Cell(7, 7), new Cell(8, 7), new Cell(9, 7),
                new Cell(7, 12), new Cell(8, 12), new Cell(9, 12),
                // Bottom
                new Cell(11, 2), new Cell(11, 3), new Cell(11, 4),
                new Cell(11, 8), new Cell(11, 9), new Cell(11, 10)
            }
        )
    };

    public static Cell[] GetRandomPattern()
    {
        var random = new Random();
        return All[random.Next(All.Count)].Pattern;
    }

    public static Cell[] TranslatePattern(Cell[] pattern, int rowOffset, int colOffset) =>
        pattern.Select(cell => new Cell(cell.Row + rowOffset, cell.Col + colOffset)).ToArray();
}