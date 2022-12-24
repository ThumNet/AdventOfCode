using System.Text;
using System.Text.RegularExpressions;
using Helpers;
using Newtonsoft.Json.Serialization;

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

        private void Move(int amount, bool isPart2)
        {
            Console.WriteLine($"Move {amount}");
            for (int i = 0; i < amount; i++)
            {
                _draw[_pos.Y, _pos.X] = DirectionInd();

                //Draw();
                var (can, next) = isPart2 ? GetNextPosPart2() : GetNextPos();
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
                    while (p.Y < _maxY - 1)
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
                if (_pos.Y == _maxY - 1 || _map[next.Y, next.X] == ' ')
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
                    while (p.X < _maxX - 1)
                    {
                        if (At(p + Right) == ' ') return (At(p) == '.', p);
                        p += Right;
                    }

                    return (At(p) == '.', p);
                }

                return (true, next);
            }

            // Right
            if (_pos.X == _maxX - 1 || _map[next.Y, next.X] == ' ')
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

        public long Walk(bool isPart2 = false)
        {
            var r = new Regex(@"(\d+|L|R)", RegexOptions.Compiled);
            var matches = r.Matches(_directions);
            foreach (Match m in matches)
            {
                if (m.Value == "R") TurnRight();
                else if (m.Value == "L") TurnLeft();
                else Move(int.Parse(m.Value), isPart2);
                //Console.ReadLine();
            }

            Draw();
            var dirC = _direction.Equals(Right) ? 0 : _direction.Equals(Left) ? 2 : _direction.Equals(Up) ? 3 : 1;

            return 1000 * (_pos.Y + 1) + 4 * (_pos.X + 1) + dirC;
        }

        private const int CubeSize = 50;
        private const int L1 = CubeSize;
        private const int T1 = 0;
        private const int T2 = 0;
        private const int R2 = CubeSize * 3 - 1;
        private const int B2 = CubeSize - 1;
        private const int L3 = CubeSize;
        private const int R3 = CubeSize * 2 - 1;
        private const int L4 = 0;
        private const int T4 = CubeSize * 2;
        private const int R5 = CubeSize * 2 - 1;
        private const int B5 = CubeSize * 3 - 1;
        private const int L6 = 0;
        private const int R6 = CubeSize - 1;
        private const int B6 = CubeSize * 4 - 1;

        private (bool can, Point next) GetNextPosPart2()
        {
            var next = _pos + _direction;
            if (_direction.Equals(Up))
            {
                switch (_pos)
                {
                    case { Y: T1, X: >= CubeSize and < 2 * CubeSize }:
                        return Can(L6, _pos.X + 2 * CubeSize, Right);

                    case { Y: T2, X: >= 2 * CubeSize and < 3 * CubeSize }:
                        return Can(_pos.X - 2 * CubeSize, B6, Up);

                    case { Y: T4, X: >= 0 and < CubeSize }:
                        return Can(L3, _pos.X + CubeSize, Right);
                }
            }

            if (_direction.Equals(Down))
            {
                switch (_pos)
                {
                    case { Y: B2, X: >= 2 * CubeSize and < 3 * CubeSize }:
                        return Can(R3, _pos.X - CubeSize, Left);

                    case { Y: B5, X: >= CubeSize and < 2 * CubeSize }:
                        return Can(R6, _pos.X + 2 * CubeSize, Left);

                    case { Y: B6, X: >= 0 and < CubeSize }:
                        return Can(_pos.X + 2 * CubeSize, T2, Down);
                }
            }

            if (_direction.Equals(Left))
            {
                switch (_pos)
                {
                    case { X: L1, Y: >= 0 and < CubeSize }:
                        return Can(L4, _pos.Y + 2 * CubeSize, Right);

                    case { X: L3, Y: >= CubeSize and < 2 * CubeSize }:
                        return Can(_pos.Y - CubeSize, T4, Down);

                    case { X: L4, Y: >= 2 * CubeSize and < 3 * CubeSize }:
                        return Can(L1, _pos.Y - 2 * CubeSize, Right);

                    case { X: L6, Y: >= 3 * CubeSize and < 4 * CubeSize }:
                        return Can(_pos.Y - 2 * CubeSize, T1, Down);
                }
            }

            if (_direction.Equals(Right))
            {
                switch (_pos)
                {
                    case { X: R2, Y: >= 0 and < CubeSize }:
                        return Can(R5, _pos.Y + 2 * CubeSize, Left);

                    case { X: R3, Y: >= CubeSize and < 2 * CubeSize }:
                        return Can(_pos.Y + CubeSize, B2, Up);

                    case { X: R5, Y: >= 2 * CubeSize and < 3 * CubeSize }:
                        return Can(R2, _pos.Y - 2 * CubeSize, Left);

                    case { X: R6, Y: >= 3 * CubeSize and < 4 * CubeSize }:
                        return Can(_pos.Y - 2 * CubeSize, B5, Up);
                }
            }

            return (At(next) == '.', next);
        }

        private (bool can, Point next) Can(int x, int y, Point dir)
        {
            var next = new Point(x, y);
            if (_map[y, x] == '#') return (false, next);

            _direction = dir;
            return (true, next);
        }
    }

    public long Challenge2(string[] input)
    {
        long result = 0;

        var puzzle = new Puzzle(input.Take(input.Length - 2).ToList(), input.Last());
        result = puzzle.Walk(true);

        return result;
    }
}