using Helpers;

namespace AdventOfCode2022;

public class Day12
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var w = input[0].Length;
        var h = input.Length;

        var locations = new MLocation[h, w];
        MLocation start = null, goal = null;
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                char letter = input[y][x];
                char useLetter = letter == 'S' ? 'a' : letter == 'E' ? 'z' : letter;
                var loc = new MLocation(x, y, (int)useLetter);
                if (letter == 'S') start = loc;
                else if (letter == 'E') goal = loc;
                locations[y, x] = loc;
            }
        }

        var grid = new SquareGrid<MLocation>(locations);
        var algo = new AStarSearch<MLocation>();
        var foundGoal = algo.FindGoal(grid, start, goal);

        if (foundGoal != null)
        {
            result = (int)algo.CostSoFar[goal];
        }
        
        return result;
    }

    public class MLocation : ILocation
    {
        public int X { get; }
        public int Y { get; }
        public int Height { get; }

        public MLocation(int x, int y, int height)
        {
            X = x;
            Y = y;
            Height = height;
        }
        
        public double CalculateCost(ILocation neighbor)
        {
            return 1;
        }

        public bool IsPassable(ILocation neighbor)
        {
            return ((MLocation)neighbor).Height - Height <= 1;
        }
    }

    public long Challenge2(string[] input)
    {
        long result = 0;
        
        var w = input[0].Length;
        var h = input.Length;
        var locations = new MLocation[h, w];
        var starts = new List<MLocation>();
        MLocation goal = null;
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                char letter = input[y][x];
                char useLetter = letter == 'S' ? 'a' : letter == 'E' ? 'z' : letter;
                var loc = new MLocation(x, y, (int)useLetter);
                if (useLetter == 'a') starts.Add(loc);
                else if (letter == 'E') goal = loc;
                locations[y, x] = loc;
            }
        }

        var grid = new SquareGrid<MLocation>(locations);
        var algo = new AStarSearch<MLocation>();
        var foundGoal = algo.FindGoal(grid, starts, goal);

        if (foundGoal != null)
        {
            result = (int)algo.CostSoFar[goal];
        }
        
        return result;
    }
}


