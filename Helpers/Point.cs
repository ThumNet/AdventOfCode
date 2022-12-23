namespace Helpers;

public class Point
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public override string ToString()
    {
        return $"x={X},y={Y}";
    }

    public void Up(int amount = 1) => Y -= amount;
    public void Down(int amount = 1) => Y += amount;
    public void Right(int amount = 1) => X += amount;
    public void Left(int amount = 1) => X -= amount;

    public bool MeetHorizontal(Point other) => X == other.X;
    public bool MeetVertical(Point other) => Y == other.Y;

    public static Point operator +(Point a, Point b)
    {
        return new Point(a.X + b.X, a.Y + b.Y);
    }

    /// <summary>
    /// Check if points could meet diagonally (45 degrees)
    /// </summary>
    public bool MeetDiagonal(Point other)
    {
        return Math.Abs(X - other.X) == Math.Abs(Y - other.Y);
    }

    /// <summary>
    /// Determine points between two points (including start and end).
    /// Will look Horizontal, Vertical and Diagonal (45 degrees)
    /// </summary>
    public IEnumerable<Point> GetPointsBetween(Point other)
    {
        if (MeetHorizontal(other))
        {
            var (min, max) = Y.MinMax(other.Y);
            for (var y = min; y <= max; y++) yield return new Point(X, y);
        }
        else if (MeetVertical(other))
        {
            var (min, max) = X.MinMax(other.X);
            for (var x = min; x <= max; x++) yield return new Point(x, Y);
        }
        else if (MeetDiagonal(other))
        {
            var xm = X > other.X ? +1 : -1;
            var ym = Y > other.Y ? +1 : -1;
            var move = Math.Abs(X - other.X);
            for (var i = 0; i <= move; i++)
            {
                yield return new Point(other.X + xm * i, other.Y + ym * i);
            }
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is Point other)
        {
            return other.X == X && other.Y == Y;
        }

        return base.Equals(obj);
    }

    protected bool Equals(Point other)
    {
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static Point From(string input, string separator)
    {
        var parts = input.Split(separator);
        if (parts.Length != 2)
            throw new ArgumentOutOfRangeException(nameof(input),
                $"Could not split '{input}' into 2 parts with separator '{separator}'");

        return new Point(int.Parse(parts[0]), int.Parse(parts[1]));
    }
}