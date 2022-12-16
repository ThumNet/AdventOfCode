using System.Numerics;
using Helpers;

namespace AdventOfCode2022;

public class Day15
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var beacons = new List<Point>();
        var coverage = new List<Coverage>();

        foreach (var line in input)
        {
            var parts = line //Sensor at x=2, y=18: closest beacon is at x=-2, y=15
                .Replace("Sensor at x=", "")
                .Replace(" y=", "")
                .Replace(": closest beacon is at x=", "|")
                .Replace(" y=", "").Split("|");
            var s = Point.From(parts[0], ",");
            var b = Point.From(parts[1], ",");
            coverage.Add(new Coverage(s, b));
            beacons.Add(b);
        }

        var row = 2000000;
        result = coverage
            .Where(c => c.Top.Y <= row && c.Bottom.Y >= row)
            .SelectMany(c => c.GetPointsOnY(row))
            .Except(beacons.Where(p => p.Y == row))
            .Count();
        
        return result;
    }

    public class Coverage
    {
        public Coverage(Point s, Point b)
        {
            Sensor = s;
            Manhatten = Math.Abs(s.X - b.X) + Math.Abs(s.Y - b.Y);
            Top = new Point(s.X, s.Y - Manhatten);
            Bottom = new Point(s.X, s.Y + Manhatten);
            Left = new Point(s.X - Manhatten, s.Y);
            Right = new Point(s.X + Manhatten, s.Y);
        }
        
        public Point Sensor { get; set; }
        public int Manhatten { get; set; }
        
        public Point Top { get; set; }
        public Point Left { get; set; }
        public Point Right { get; set; }
        public Point Bottom { get; set; }

        public IEnumerable<Point> GetPointsOnY(int y)
        {
            var manIx = Manhatten - Math.Abs(Sensor.Y - y);
            if (manIx == 0) return new[] { new Point(Sensor.X, y) };
            return new Point(Sensor.X - manIx, y).GetPointsBetween(new Point(Sensor.X + manIx, y));
        }

        public Range GetXRange(int y, int range)
        {
            if (y == Sensor.Y) return new Range(Math.Max(0, Left.X), Math.Min(range, Right.X));
            
            var manIx = Manhatten - Math.Abs(Sensor.Y - y);
            return new Range(Math.Max(0, Sensor.X - manIx), Math.Min(range, Sensor.X + manIx));
        }
    }

    public record Range(int Min, int Max);

    public BigInteger Challenge2(string[] input)
    {
        BigInteger result = 0;
        
        var coverage = new List<Coverage>();

        foreach (var line in input)
        {
            var parts = line //Sensor at x=2, y=18: closest beacon is at x=-2, y=15
                .Replace("Sensor at x=", "")
                .Replace(" y=", "")
                .Replace(": closest beacon is at x=", "|")
                .Replace(" y=", "").Split("|");
            var s = Point.From(parts[0], ",");
            var b = Point.From(parts[1], ",");
            coverage.Add(new Coverage(s, b));
        }

        var range = 4000000;

        for (int y = 0; y < range; y++)
        {
            var c = coverage
                .Where(c => c.Top.Y <= y && c.Bottom.Y >= y)
                .Select(c => c.GetXRange(y, range))
                .OrderBy(r => r.Min).ThenBy(r => r.Max);

            var prev = 0;
            foreach (var r in c)
            {
                if (r.Min < prev && r.Max < prev) continue; // prevent overlap range
                if (Math.Max(r.Min, prev) != prev) break;
                prev = r.Max;
            }

            if (prev != range)
            {
                result = new BigInteger(range)*new BigInteger(prev+1) + new BigInteger(y);
                break;
            }
        }
        
        return result;
    }
}