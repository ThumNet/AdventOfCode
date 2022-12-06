using Helpers;

namespace AdventOfCode2021;

public class Day5
{
    public int Challenge1(string[] input)
    {
        int result = 0;

        var grid = TwoDimensionalArray.Create(1000, 1000);

        foreach (var line in input)
        {
            var (p1, p2) = ParseLine(line);

            if (p1.X == p2.X)
            {
                var s = Math.Min(p1.Y, p2.Y);
                var e = Math.Max(p1.Y, p2.Y);
                for (var i = s; i <= e; i++)
                {
                    grid[i, p1.X] += 1;
                }
            }
            else if (p1.Y == p2.Y)
            {
                var s = Math.Min(p1.X, p2.X);
                var e = Math.Max(p1.X, p2.X);
                for (var i = s; i <= e; i++)
                {
                    grid[p1.Y, i] += 1;
                }
            }
            
        }
        result = grid.Cast<int>().Where(n => n >= 2).Count();

        return result;
    }

    private (Point, Point) ParseLine(string line)
    {
        var parts = line.Split(" -> ");
        return (Point.From(parts[0], ","), Point.From(parts[1], ","));
    }

    public int Challenge2(string[] input)
    {
        int result = 0;

        var grid = TwoDimensionalArray.Create(1000, 1000);

        foreach (var line in input)
        {
            var (p1, p2) = ParseLine(line);
            var pointsBetween = p1.GetPointsBetween(p2);
            foreach (var p in pointsBetween)
            {
                grid[p.Y, p.X] += 1;
            }
        }
        
        result = grid.Cast<int>().Where(n => n >= 2).Count();

        return result;
    }

    public int Challenge2_WITHOUT_PointHelperMethods(string[] input)
    {
        int result = 0;

        var grid = TwoDimensionalArray.Create(1000, 1000);

        foreach (var line in input)
        {
            var (p1, p2) = ParseLine(line);

            if (p1.X == p2.X)
            {
                var s = Math.Min(p1.Y, p2.Y);
                var e = Math.Max(p1.Y, p2.Y);
                for (var i = s; i <= e; i++)
                {
                    grid[i, p1.X] += 1;
                }
            }
            else if (p1.Y == p2.Y)
            {
                var s = Math.Min(p1.X, p2.X);
                var e = Math.Max(p1.X, p2.X);
                for (var i = s; i <= e; i++)
                {
                    grid[p1.Y, i] += 1;
                }
            }
            else
            {
                // diagonal
                var horMove = Math.Max(p1.X, p2.X) - Math.Min(p1.X, p2.X);
                var vertMove = Math.Max(p1.Y, p2.Y) - Math.Min(p1.Y, p2.Y);
                if (horMove == vertMove)
                {
                    var xm = p1.X > p2.X ? +1 : -1;
                    var ym = p1.Y > p2.Y ? +1 : -1;
                    var move = Math.Abs(p1.X - p2.X);
                    for (var i = 0; i <= move; i++)
                    {
                        grid[p2.Y + (ym * i), p2.X + ( xm * i)] += 1;
                    }
                }
            }
            
        }
        result = grid.Cast<int>().Where(n => n >= 2).Count();
        
        return result;
    }
}