using Helpers;

namespace AdventOfCode2022;

public class Day14
{
    private int _maxX;
    private int _maxY;
    private int _minX;

    public long Challenge1(string[] input)
    {
        long result = 0;

        var wallPoints = ReadFromInput(input);
        var world = CreateWorld(wallPoints);
        var minX = wallPoints.Min(p => p.X);
        
        //DrawWorld(world);
        while (true)
        {
            var (sandX, sandY) = DropSand(world, 500 - minX);
            if (sandX == -1) break;

            world[sandY, sandX] = 'o';
            //DrawWorld(world);
        }

        result = world.Cast<char>().Count(x => x == 'o');

        return result;
    }

    private char[,] CreateWorld(List<Point> wallPoints)
    {
        _minX = wallPoints.Min(p => p.X);
        _maxX = wallPoints.Max(p => p.X);
        _maxY = wallPoints.Max(p => p.Y);
        var minY = 0;
        var xRange = _maxX - _minX +1;
        Console.WriteLine($"{xRange}: {_minX} - {_maxX}");
        var world = TwoDimensionalArray.Create(_maxY+1, xRange, '.');
        foreach (var p in wallPoints)
        {
            world[p.Y, p.X - _minX] = '#';
        }

        return world;
    }
    
    private char[,] CreateWorld2(List<Point> wallPoints, int extend)
    {
        _minX = wallPoints.Min(p => p.X);
        _maxX = wallPoints.Max(p => p.X);
        _maxY = wallPoints.Max(p => p.Y);

        wallPoints.AddRange(
            new Point(_minX - extend, _maxY + 2)
                .GetPointsBetween(new Point(_maxX + extend, _maxY + 2)));
        
        _minX = wallPoints.Min(p => p.X);
        _maxX = wallPoints.Max(p => p.X);
        _maxY = wallPoints.Max(p => p.Y);    
            
        var minY = 0;
        var xRange = _maxX - _minX +1;
        Console.WriteLine($"{xRange}: {_minX} - {_maxX}");
        var world = TwoDimensionalArray.Create(_maxY+1, xRange, '.');
        foreach (var p in wallPoints)
        { 
            world[p.Y, p.X - _minX] = '#';
        }

        return world;
    }

    private List<Point> ReadFromInput(string[] input)
    {
        var wallPoints = new List<Point>();
        
        foreach (var line in input)
        {
            var coords = line.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < coords.Length; i++)
            {
                var start = Point.From(coords[i - 1], ",");
                var end = Point.From(coords[i], ",");
                wallPoints.AddRange(start.GetPointsBetween(end));
            }
        }

        return wallPoints;
    }

    private (int endX, int endY) DropSand(char[,] world, int x, int y = 0)
    {
        if (y >= _maxY || x < 0 || x >= _maxX)
        {
            return (-1, -1);
        }

        while (y + 1 < _maxY && world[y+1, x] == '.')
        {
            y++;
        }
        
        if (x - 1 == -1) return (-1, -1); 
        if (x + 1 == _maxX + 1) return (-1, -1);
        if (y + 1 == _maxY + 1) return (-1, -1);
        if (world[y + 1, x - 1] == '.') return DropSand(world, x - 1, y + 1);
        if (world[y + 1, x + 1] == '.') return DropSand(world, x + 1, y + 1);

        return (x, y);
    }
    

    public long Challenge2(string[] input)
    {
        long result = 0;
        
        var wallPoints = ReadFromInput(input);
        var world = CreateWorld2(wallPoints, 200);
        
        //DrawWorld(world);
        while (true)
        {
            var (sandX, sandY) = DropSand(world, 500 - _minX);
            if (sandX == -1) break;

            world[sandY, sandX] = 'o';
            
            if (sandY == 0) break;
            //DrawWorld(world);
        }
        DrawWorld(world);
        result = world.Cast<char>().Count(x => x == 'o');
        
        return result;
    }

    private void DrawWorld(char[,] world)
    {
        Console.WriteLine();
        Console.WriteLine();
        for (int y = 0; y < world.GetLength(0); y++)
        {
            for (int x = 0; x < world.GetLength(1); x++)
            {
                Console.Write(world[y,x]);
            }

            Console.WriteLine();
        }
    }
}