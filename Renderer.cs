using ConsoleGameOfLife.Models;

namespace ConsoleGameOfLife;

public class Renderer
{
    private const char LiveCell = '█';
    private const char DeadCell = ' ';
    private readonly int _rows;
    private readonly int _cols;

    public Renderer(int rows, int cols)
    {
        _rows = rows;
        _cols = cols;
        Console.CursorVisible = false;
        Console.SetWindowSize(Math.Min(Console.LargestWindowWidth, cols + 2),
                            Math.Min(Console.LargestWindowHeight, rows + 5));
    }

    public void Render(Grid grid, bool isPaused, int speed)
    {
        Console.SetCursorPosition(0, 0);
        
        // Draw top border
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine('┌' + new string('─', _cols) + '┐');

        // Draw grid
        for (int row = 0; row < _rows; row++)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write('│');
            
            for (int col = 0; col < _cols; col++)
            {
                var cell = new Cell(row, col);
                Console.ForegroundColor = grid[cell] ? ConsoleColor.Green : ConsoleColor.DarkGray;
                Console.Write(grid[cell] ? LiveCell : DeadCell);
            }
            
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine('│');
        }

        // Draw bottom border
        Console.WriteLine('└' + new string('─', _cols) + '┘');

        // Draw status
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"Status: {(isPaused ? "Paused" : "Running")} | Speed: {speed}ms");
        Console.WriteLine("Controls: [Space] Pause/Resume | [R] Randomize | [C] Clear | [↑/↓] Speed | [Q] Quit");
    }

    public void Clear()
    {
        Console.Clear();
    }
}