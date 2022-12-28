using System.Collections.Immutable;
using System.Security.Cryptography;

namespace AdventOfCode2022;

public class Day17
{
    private class Point
    {
        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public Point(long x, long y)
        {
            X = x;
            Y = y;
        }

        public long X { get; set; }
        public long Y { get; set; }
    }
    
    private static HashSet<Point> GetRock(int rockIx, long y)
    {
        return rockIx switch
        {
            0 => new HashSet<Point> { new(2, y), new(3, y), new(4, y), new(5, y) },
            1 => new HashSet<Point> { new(3, y + 2), new(2, y + 1), new(3, y + 1), new(4,y+1), new(3, y) },
            2 => new HashSet<Point> { new(4, y + 2), new(4, y + 1), new(2, y), new(3, y), new(4, y) },
            3 => new HashSet<Point> { new(2, y + 3), new(2, y + 2), new(2, y + 1), new(2, y) },
            _ => new HashSet<Point> { new(2, y + 1), new(3, y + 1), new(2, y), new(3, y) }
        };
    }

    private static void MoveLeft(HashSet<Point> rock)
    {
        if (rock.Any(p => p.X == 0)) return;
        foreach (var p in rock) p.X -= 1;
    }

    private static void MoveRight(HashSet<Point> rock)
    {
        if (rock.Any(p => p.X == 6)) return;
        foreach (var p in rock) p.X += 1;
    }

    private static void MoveDown(HashSet<Point> rock)
    {
        foreach (var p in rock) p.Y -= 1;
    }

    private static void MoveUp(HashSet<Point> rock)
    {
        foreach (var p in rock) p.Y += 1;
    }

    public class Game
    {
        private HashSet<Point> _state = new()
            { new(0, 0), new(1, 0), new(2, 0), new(3, 0), new(4, 0), new(5, 0), new(6, 0), };

        private long _top = 0;

        public long Simulate(string pushes, long max)
        {
            var i = 0;
            var seen = new Dictionary<(int oldI, int oldRockIx, int hash), (long oldR, long oldY)>();
            long r = 0;
            long added = 0;
            while(r < max)
            {
                var rock = GetRock((int)(r%5), _top + 4);

                while (true)
                {
                    if (pushes[i] == '>')
                    {
                        MoveRight(rock);
                        if (_state.Overlaps(rock)) MoveLeft(rock);
                    }
                    else
                    {
                        MoveLeft(rock);
                        if (_state.Overlaps(rock)) MoveRight(rock);
                    }

                    i = (i + 1) % pushes.Length;

                    MoveDown(rock);
                    if (_state.Overlaps(rock))
                    {
                        MoveUp(rock);
                        foreach (var p in rock) _state.Add(p);
                        RemoveExcess(rock, r, max);
                        _top = _state.Max(r => r.Y);

                        var sr = (i, (int)(r%5), Signature());
                        if (seen.ContainsKey(sr) && max > 2022 && added == 0)
                        {
                            var (oldR, oldTop) = seen[sr];
                            var dtop = _top - oldTop;
                            var dr = r - oldR;
                            var amount = (max - _top) / dr;
                            added += amount * dtop;
                            r += amount * dr;
                            seen.Clear();
                        }

                        seen[sr] = (r, _top);
                        break;
                    }
                }

                r++;
            }

            return _top + added;
        }

        private void RemoveExcess(HashSet<Point> rock, long ix, long max)
        {
            if (ix % 1000000 == 0)
            {
                Console.WriteLine("{0:0.00}%",ix/(max * 1.0)*100);
            }

            if (ix % 1000 == 0)
            {
                long y = _top - 1000; 
                //Console.WriteLine($"Removing below {y}");
                _state.RemoveWhere(p => p.Y < y);
            }
        }

        private void Draw(int rockNr)
        {
            Console.WriteLine($"Rock: {rockNr}");
            for (var y = _top; y>=0; y--)
            {
                var line = "";
                for (long x = 0; x < 7; x++)
                {
                    line += _state.Contains(new Point(x, y)) ? '#' : ' ';
                }

                Console.WriteLine(line);
            }

            Console.WriteLine();
        }

        private int Signature()
        {
            var maxY = _state.Max(r => r.Y);
            return _state.Where(r => maxY - r.Y <= 30).GetHashCode();
        }
    }

    public long Challenge1(string[] input)
    {
        long result = 0;

        var pushes = input[0];
        var g = new Game();
        result = g.Simulate(pushes, 2022);

        return result;
    }

    public long Challenge2(string[] input)
    {
        long result = 0;

        var pushes = input[0];
        var g = new Game();
        result = g.Simulate(pushes, 1000000000000);
        
        return result;
    }
}