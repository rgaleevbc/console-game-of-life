using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;
using GameOfLife.Core;
using GameOfLife.Core.Models;
using System;
using System.Linq;

namespace GameOfLife.Desktop;

public partial class MainWindow : Window
{
    private const int CellSize = 15;
    private const int Rows = 40;
    private const int Cols = 60;
    
    private readonly GameEngine _engine;
    private readonly DispatcherTimer _timer;
    private bool _isRunning;
    private int _currentPatternIndex = -1;
    private bool _isDrawing;

    public MainWindow()
    {
        InitializeComponent();
        
        _engine = new GameEngine(Rows, Cols);
        
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        _timer.Tick += Timer_Tick;
        
        SpeedSlider.ValueChanged += (s, e) => _timer.Interval = TimeSpan.FromMilliseconds(e.NewValue);
        
        Loaded += (s, e) =>
        {
            GameCanvas.Width = Cols * CellSize;
            GameCanvas.Height = Rows * CellSize;
            DrawGrid();
        };

        GameCanvas.PointerReleased += (s, e) => _isDrawing = false;
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        _engine.NextGeneration();
        DrawGrid();
    }

    private void DrawGrid()
    {
        GameCanvas.Children.Clear();

        // Draw grid lines
        for (int row = 0; row <= Rows; row++)
        {
            var line = new Avalonia.Controls.Shapes.Line
            {
                StartPoint = new(0, row * CellSize),
                EndPoint = new(Cols * CellSize, row * CellSize),
                Stroke = new SolidColorBrush(Color.FromRgb(40, 40, 40)),
                StrokeThickness = 1
            };
            GameCanvas.Children.Add(line);
        }

        for (int col = 0; col <= Cols; col++)
        {
            var line = new Avalonia.Controls.Shapes.Line
            {
                StartPoint = new(col * CellSize, 0),
                EndPoint = new(col * CellSize, Rows * CellSize),
                Stroke = new SolidColorBrush(Color.FromRgb(40, 40, 40)),
                StrokeThickness = 1
            };
            GameCanvas.Children.Add(line);
        }

        // Draw cells
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Cols; col++)
            {
                var cell = new Cell(row, col);
                if (_engine.CurrentGrid[cell])
                {
                    var rect = new Avalonia.Controls.Shapes.Rectangle
                    {
                        Width = CellSize - 2,
                        Height = CellSize - 2,
                        Fill = new SolidColorBrush(Color.FromRgb(50, 255, 50))
                    };
                    Canvas.SetLeft(rect, col * CellSize + 1);
                    Canvas.SetTop(rect, row * CellSize + 1);
                    GameCanvas.Children.Add(rect);
                }
            }
        }
    }

    private void OnStartStopClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _isRunning = !_isRunning;
        if (_isRunning)
        {
            StartStopButton.Content = "Stop";
            _timer.Start();
            StatusText.Text = "Running";
        }
        else
        {
            StartStopButton.Content = "Start";
            _timer.Stop();
            StatusText.Text = "Paused";
        }
    }

    private void OnClearClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _engine.ClearGrid();
        DrawGrid();
        StatusText.Text = "Grid cleared";
    }

    private void OnRandomClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _engine.RandomizeGrid();
        DrawGrid();
        StatusText.Text = "Random pattern generated";
    }

    private void OnNextPatternClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _currentPatternIndex = (_currentPatternIndex + 1) % Patterns.All.Count;
        var (name, pattern) = Patterns.All[_currentPatternIndex];
        var centerRow = Rows / 2 - pattern.Max(c => c.Row) / 2;
        var centerCol = Cols / 2 - pattern.Max(c => c.Col) / 2;
        _engine.LoadPattern(Patterns.TranslatePattern(pattern, centerRow, centerCol));
        DrawGrid();
        StatusText.Text = $"Loaded pattern: {name}";
    }

    private void OnCanvasPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var point = e.GetPosition(GameCanvas);
        var row = (int)(point.Y / CellSize);
        var col = (int)(point.X / CellSize);
        
        if (row >= 0 && row < Rows && col >= 0 && col < Cols)
        {
            _isDrawing = true;
            ToggleCell(row, col);
        }
    }

    private void OnCanvasPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_isDrawing) return;
        
        var point = e.GetPosition(GameCanvas);
        var row = (int)(point.Y / CellSize);
        var col = (int)(point.X / CellSize);
        
        if (row >= 0 && row < Rows && col >= 0 && col < Cols)
        {
            ToggleCell(row, col);
        }
    }

    private void ToggleCell(int row, int col)
    {
        var cell = new Cell(row, col);
        var currentState = _engine.CurrentGrid[cell];
        var newGrid = new GameOfLife.Core.Models.Grid(Rows, Cols);
        
        // Copy existing cells
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Cols; c++)
            {
                var existingCell = new Cell(r, c);
                if (_engine.CurrentGrid[existingCell])
                {
                    newGrid = newGrid.WithCell(existingCell, true);
                }
            }
        }
        
        // Toggle the clicked cell
        newGrid = newGrid.WithCell(cell, !currentState);
        
        _engine.LoadPattern(newGrid.GetLiveCells());
        DrawGrid();
    }
}