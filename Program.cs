using ConsoleGameOfLife;

const int rows = 30;
const int cols = 80;
var speed = 100;
var isPaused = false;
var isRunning = true;
var currentPatternIndex = -1;

var engine = new GameEngine(rows, cols);
var renderer = new Renderer(rows, cols);

engine.RandomizeGrid();

while (isRunning)
{
    renderer.Render(engine.CurrentGrid, isPaused, speed);

    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey(true);
        switch (key.Key)
        {
            case ConsoleKey.Spacebar:
                isPaused = !isPaused;
                break;
            case ConsoleKey.R:
                engine.RandomizeGrid();
                break;
            case ConsoleKey.C:
                engine.ClearGrid();
                break;
            case ConsoleKey.P:
                currentPatternIndex = (currentPatternIndex + 1) % Patterns.All.Count;
                var (name, pattern) = Patterns.All[currentPatternIndex];
                var centerRow = rows / 2 - pattern.Max(c => c.Row) / 2;
                var centerCol = cols / 2 - pattern.Max(c => c.Col) / 2;
                engine.LoadPattern(Patterns.TranslatePattern(pattern, centerRow, centerCol));
                break;
            case ConsoleKey.UpArrow:
                speed = Math.Max(50, speed - 50);
                break;
            case ConsoleKey.DownArrow:
                speed = Math.Min(500, speed + 50);
                break;
            case ConsoleKey.Q:
                isRunning = false;
                break;
        }
    }

    if (!isPaused)
    {
        engine.NextGeneration();
    }

    Thread.Sleep(speed);
}

renderer.Clear();
Console.WriteLine("Game of Life ended. Press any key to exit...");
Console.ReadKey(true);