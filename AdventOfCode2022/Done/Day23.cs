using Helpers;

namespace AdventOfCode2022;

public class Day23
{
    private static Point N = new(0, -1);
    private static Point NE = new(1, -1);
    private static Point NW = new(-1, -1);
    private static Point S = new(0, 1);
    private static Point SE = new(1, 1);
    private static Point SW = new(-1, 1);
    private static Point W = new(-1, 0);
    private static Point E = new(1, 0);

    private static Point[] Wind = { N, NE, E, SE, S, SW, W, NW };
    private Point[][] Checks =
    {
        new [] { N, NE, NW },
        new [] { S, SE, SW },
        new [] { W, NW, SW },
        new [] { E, NE, SE },
    };
    
    public long Challenge1(string[] input)
    {
        long result = 0;

        var elfs = ReadInput(input).ToArray();
        var rounds = 10;
        var checkIx = 0;
        var dict = new Dictionary<int, Point>();
        for (int i = 0; i < rounds; i++)
        {
            //Draw(elfs);
            dict.Clear();

            for (int j = 0; j < elfs.Length; j++)
            {
                var to = GetDirection(j, elfs, checkIx);
                if (!to.Equals(elfs[j])) dict[j] = to;
            }

            if (dict.Count == 0)
            {
                Console.WriteLine("No elfs need to move");
                break;
            }
            
            // newly proposed positions are duplicates these dont move
            var allUnique = dict
                .GroupBy(x => x.Value)
                .Where(g => g.Count() == 1)
                .Select(g => g.First()).ToList();
            //Console.WriteLine($"Round {i+1} => unique {allUnique.Count}");
            foreach (var (e, move) in allUnique)
            {
                elfs[e] = move;
            }

            checkIx = (checkIx + 1) % 4;
            //Console.WriteLine($"Check is now: {checkIx}");
        }

        var minX = elfs.Min(e => e.X);
        var maxX = elfs.Max(e => e.X);
        var minY = elfs.Min(e => e.Y);
        var maxY = elfs.Max(e => e.Y);

        Console.WriteLine($"x: {minX} till {maxX}");
        Console.WriteLine($"y: {minY} till {maxY}");

        result = ((maxX - minX) +1) * ((maxY - minY) +1) - elfs.Length;

        return result;
    }

    private void Draw(Point[] elfs)
    {
        var minX = elfs.Min(e => e.X);
        var maxX = elfs.Max(e => e.X);
        var minY = elfs.Min(e => e.Y);
        var maxY = elfs.Max(e => e.Y);

        for (var i = minY; i <= maxY; i++)
        {
            for (var j = minX; j <=maxX; j++)
            {
                Console.Write(elfs.Any(a => a.X==j && a.Y == i) ? '#' : '.');
            }

            Console.WriteLine();
        }
    }

    private Point GetDirection(int elfIndex, Point[] elfs, int c)
    {
        var others = elfs[..elfIndex].Concat(elfs[(elfIndex + 1)..]).ToList();
        var elf = elfs[elfIndex];
        // if no other elfs in any direction then dont move
        var tos = Wind.Select(w => elf + w).ToList();
        if (!tos.Any(o => others.Contains(o))) return elf;

        for (var i = 0; i < Checks.Length; i++)
        {
            tos = Checks[(c+i)%4].Select(w => elf + w).ToList();
            if (!tos.Any(o => others.Contains(o))) return tos[0];
        }
        
        return elf;
    }

    private IEnumerable<Point> ReadInput(string[] input)
    {
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '#') yield return new Point(x, y);
            }
        }
    }


    public long Challenge2(string[] input)
    {
        long result = 0;
        
        var elfs = ReadInput(input).ToArray();
        var rounds = 10000;
        var checkIx = 0;
        var dict = new Dictionary<int, Point>();
        for (int i = 0; i < rounds; i++)
        {
            if (i % 10 == 0)
            {
                Console.WriteLine("{0:0.00}%",i/(rounds * 1.0)*100);
            }
            //Draw(elfs);
            dict.Clear();

            for (int j = 0; j < elfs.Length; j++)
            {
                var to = GetDirection(j, elfs, checkIx);
                if (!to.Equals(elfs[j])) dict[j] = to;
            }

            if (dict.Count == 0)
            {
                Console.WriteLine("No elfs need to move");
                result = i + 1;
                break;
            }
            
            // newly proposed positions are duplicates these dont move
            var allUnique = dict
                .GroupBy(x => x.Value)
                .Where(g => g.Count() == 1)
                .Select(g => g.First()).ToList();
            //Console.WriteLine($"Round {i+1} => unique {allUnique.Count}");
            foreach (var (e, move) in allUnique)
            {
                elfs[e] = move;
            }

            checkIx = (checkIx + 1) % 4;
            //Console.WriteLine($"Check is now: {checkIx}");
        }

        return result;
    }
}