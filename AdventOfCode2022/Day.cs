using System.Text;
using System.Text.RegularExpressions;
using Helpers;

namespace AdventOfCode2022;

public class Day
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var puzzle = new Puzzle(input.Take(input.Length - 2).ToList(), input.Last());
        result = puzzle.Walk();

        return result;
    }

    public class Puzzle
    {
        private readonly string _directions;
        private char[,] _map;
        private int _maxX;
        private int _maxY;
        private Point Right = new(1, 0);
        private Point Down = new(0, 1);
        private Point Left = new(-1, 0);
        private Point Up = new(0, -1);

        private Point _direction;
        private Point _pos;
        private readonly char[,] _draw;

        public Puzzle(List<string> map, string directions)
        {
            _maxX = map.Max(s => s.Length);
            _maxY = map.Count;
            _map = TwoDimensionalArray.Create(_maxY, _maxX, ' ');
            _draw = TwoDimensionalArray.Create(_maxY, _maxX, ' ');
            FillMap(map);
            _directions = directions;
            _direction = Right;
            _pos = new Point(map[0].IndexOf('.'), 0);
        }

        private void FillMap(List<string> map)
        {
            for (int y = 0; y < _maxY; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    _map[y, x] = map[y][x];
                    _draw[y, x] = map[y][x];
                }
            }
        }

        private void Move(int amount)
        {
            Console.WriteLine($"Move {amount}");
            for (int i = 0; i < amount; i++)
            {
                _draw[_pos.Y, _pos.X] = DirectionInd();

                //Draw();
                var (can, next) = GetNextPos();
                if (!can) break;
                var c = _map[next.Y, next.X];
                if (c == '#') break;
                _pos = next;
            }
        }

        private void Draw()
        {
            for (int i = 0; i < _maxY; i++)
            {
                Console.WriteLine(new string(_draw.ItemsInRow(i)));
            }
        }

        private char DirectionInd()
        {
            return _direction.Equals(Right) ? '>' : _direction.Equals(Left) ? '<' : _direction.Equals(Up) ? '^' : 'v';
        }

        public char At(Point p) => _map[p.Y, p.X];

        private (bool, Point) GetNextPos()
        {
            var next = _pos + _direction;
            if (_direction.Equals(Up))
            {
                if (_pos.Y == 0 || _map[next.Y, next.X] == ' ')
                {
                    var p = _pos + Down;
                    while (p.Y < _maxY-1)
                    {
                        if (At(p + Down) == ' ') return (At(p) == '.', p);
                        p += Down;
                    }
                    
                    return (At(p) == '.', p);
                }

                return (true, next);
            }

            if (_direction.Equals(Down))
            {
                if (_pos.Y == _maxY-1 || _map[next.Y, next.X] == ' ')
                {
                    var p = _pos + Up;
                    while (p.Y > 0)
                    {
                        if (At(p + Up) == ' ') return (At(p) == '.', p);
                        p += Up;
                    }
                    
                    return (At(p) == '.', p);
                }

                return (true, next);
            }

            if (_direction.Equals(Left))
            {
                if (_pos.X == 0 || _map[next.Y, next.X] == ' ')
                {
                    var p = _pos + Right;
                    while (p.X < _maxX-1)
                    {
                        if (At(p + Right) == ' ') return (At(p) == '.', p);
                        p += Right;
                    }
                    
                    return (At(p) == '.', p);
                }

                return (true, next);
            }

            // Right
            if (_pos.X == _maxX-1 || _map[next.Y, next.X] == ' ')
            {
                var p = _pos + Left;
                while (p.X > 0)
                {
                    if (At(p + Left) == ' ') return (At(p) == '.', p);
                    p += Left;
                }
                return (At(p) == '.', p);
            }
            return (true, next);
        }

        private void TurnRight()
        {
            Console.WriteLine("Turn right");
            if (Equals(_direction, Right)) _direction = Down;
            else if (Equals(_direction, Down)) _direction = Left;
            else if (Equals(_direction, Left)) _direction = Up;
            else _direction = Right;
        }

        private void TurnLeft()
        {
            Console.WriteLine("Turn left");
            if (Equals(_direction, Right)) _direction = Up;
            else if (Equals(_direction, Down)) _direction = Right;
            else if (Equals(_direction, Left)) _direction = Down;
            else _direction = Left;
        }

        public long Walk()
        {
            var r = new Regex(@"(\d+|L|R)", RegexOptions.Compiled);
            var matches = r.Matches(_directions);
            foreach (Match m in matches)
            {
                if (m.Value == "R") TurnRight();
                else if (m.Value == "L") TurnLeft();
                else Move(int.Parse(m.Value));
                //Console.ReadLine();
            }
            Draw();
            var dirC = _direction.Equals(Right) ? 0 : _direction.Equals(Left) ? 2 : _direction.Equals(Up) ? 3 : 1;
            return 1000 * (_pos.Y + 1) + 4 * (_pos.X + 1) + dirC;
        }
    }

    public long Challenge2(string[] input)
    {
        long result = 0;


        return result;
    }
}