using Helpers;

namespace AvantOfCode2022;

public class Day9
{
    public int Challenge1(string[] input)
    {
        var result = 0;

        result = CalculateTailVisits(input, 1);

        return result;
    }

    public record Move(char Dir, int Amount);

    public class Area
    {
        public Area(int tailSize = 1)
        {
            Head = new Point(0, 0);
            Tails = new Point[tailSize];

            for (int i = 0; i < tailSize; i++)
            {
                Tails[i] = new Point(0, 0);
            }

            AddTailVisit();
        }

        public HashSet<Point> VisitedTail { get; set; } = new();
        public Point Head { get; set; }
        public Point[] Tails { get; set; }

        private void AddTailVisit()
        {
            VisitedTail.Add(Tails[Tails.Length - 1]);
        }

        public void MoveHead(char d)
        {
            if (d == 'U') Head.Up();
            else if (d == 'D') Head.Down();
            else if (d == 'R') Head.Right();
            else Head.Left();

            FollowHead(Head, Tails[0]);
            for (int i = 1; i < Tails.Length; i++)
            {
                FollowHead(Tails[i - 1], Tails[i]);
            }

            AddTailVisit();
            //Console.WriteLine($"{d} Head:{Head}, Tail:{Tail}");
        }

        public void PrintPos()
        {
            for (int x = -15; x < 15; x++)
            {
                for (int y = -15; y < 15; y++)
                {
                    var cur = new Point(y, x);
                    if (Equals(cur, Head)) Console.Write('H');
                    else if (Equals(cur, Tails[0])) Console.Write('1');
                    else if (Equals(cur, Tails[1])) Console.Write('2');
                    else if (Equals(cur, Tails[2])) Console.Write('3');
                    else if (Equals(cur, Tails[3])) Console.Write('4');
                    else if (Equals(cur, Tails[4])) Console.Write('5');
                    else if (Equals(cur, Tails[5])) Console.Write('6');
                    else if (Equals(cur, Tails[6])) Console.Write('7');
                    else if (Equals(cur, Tails[7])) Console.Write('8');
                    else if (Equals(cur, Tails[8])) Console.Write('9');
                    else Console.Write('.');
                }

                Console.WriteLine();
            }
        }

        private void FollowHead(Point toFollow, Point tail)
        {
            if (Math.Abs(toFollow.Y - tail.Y) >= 2 || Math.Abs(toFollow.X - tail.X) >= 2)
            {
                tail.Y += toFollow.Y == tail.Y ? 0 : toFollow.Y - tail.Y > 0 ? 1 : -1;
                tail.X += toFollow.X == tail.X ? 0 : toFollow.X - tail.X > 0 ? 1 : -1;
            }
        }
    }

    public int Challenge2(string[] input)
    {
        var result = 0;

        result = CalculateTailVisits(input, 9);

        return result;
    }

    private int CalculateTailVisits(string[] input, int numberOfTails)
    {
        var moves = input.Select(i =>
        {
            var parts = i.Split(' ');
            return new Move(parts[0][0], int.Parse(parts[1]));
        });
        var area = new Area(numberOfTails);
        foreach (var move in moves)
        {
            for (int i = 0; i < move.Amount; i++)
            {
                area.MoveHead(move.Dir);
            }
        }

        return area.VisitedTail.Count();
    }
}