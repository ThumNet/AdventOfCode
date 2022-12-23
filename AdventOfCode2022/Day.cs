using System.Text.RegularExpressions;
using Helpers;

namespace AdventOfCode2022;

public class Day
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var puzzle = new Puzzle(input.Take(input.Length - 2).ToList(), input.Last());
        puzzle.Walk();

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

        public Puzzle(List<string> map, string directions)
        {
            _maxX = map.Max(s => s.Length);
            _maxY = map.Count;
            _map = TwoDimensionalArray.Create(_maxY, _maxX, ' ');
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
                }
            }
        }

        private void Move(int amount)
        {
            Console.WriteLine($"Move {amount}");
            for (int i = 0; i < amount; i++)
            {
                _map[_pos.Y, _pos.X] = DirectionInd();

                Draw();
                var next = GetNextPos();
                var c = _map[next.Y, next.X];
                if (c == '#') break;
                if (c == ' ') WrapAround();
                else _pos = next;
            }
        }

        private void Draw()
        {
            Console.Clear();
            for (int i = 0; i < _maxY; i++)
            {
                Console.WriteLine(_map.ItemsInRow(i));
            }
        }

        private char DirectionInd()
        {
            if (_direction.Equals(Right)) return '>';
            if (_direction.Equals(Left)) return '<';
            if (_direction.Equals(Up)) return '^';
            return 'v';
        }

        private (bool, Point) GetNextPos()
        {
            var next = _pos + _direction;
            if (_direction.Equals(Up))
            {
                if (_pos.Y == 0 || _map[next.Y, next.X] == ' ')
                {
                    // wrapdown
                    return (false, next);
                }

                return (true, next);
            }

            if (_direction.Equals(Down))
            {
                if (_pos.Y == _maxY || _map[next.Y, next.X] == ' ')
                {
                    // wrapup
                    return (false, next);
                }

                return (true, next);
            }

            if (_direction.Equals(Left))
            {
                if (_pos.X == 0 || _map[next.Y, next.X] == ' ')
                {
                    // wrapright
                    return (false, next);
                }

                return (true, next);
            }

            // Right
            if (_pos.X == _maxX || _map[next.Y, next.X] == ' ')
            {
                // wrapleft
                return (false, next);
            }
            return (true, next);
        }

        private void WrapAround()
        {
            if (_direction.Equals(Right)) WrapLeft();
            else if (_direction.Equals(Left)) WrapRight();
            else if (_direction.Equals(Up)) WrapDown();
            else WrapUp();
        }

        private void WrapDown()
        {
            var y = _pos.Y;
            while (y++ < _maxY)
            {
                if (_map[y, _pos.X] == ' ') break;
            }

            _pos = new Point(_pos.X, y--);
        }

        private void WrapUp()
        {
            var y = _pos.Y;
            while (y-- > 0)
            {
                if (_map[y, _pos.X] == ' ') break;
            }

            _pos = new Point(_pos.X, y++);
        }

        private void WrapLeft()
        {
            var x = -1;
            while (x++ < _maxX)
            {
                if (_map[_pos.Y, x] != ' ') break;
            }

            _pos = new Point(x--, _pos.Y);
        }

        private void WrapRight()
        {
            var x = _;
            while (x 1 < _maxX)
            {
                if (_map[_pos.Y, x++] != ' ') break;
            }

            _pos = new Point(x--, _pos.Y);
        }

        private bool IsGrid()
        {
            return _map[_pos.Y, _pos.X] != ' ';
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

        public void Walk()
        {
            var r = new Regex(@"(\d+|L|R)", RegexOptions.Compiled);
            var matches = r.Matches(_directions);
            foreach (Match m in matches)
            {
                if (m.Value == "R") TurnRight();
                else if (m.Value == "L") TurnLeft();
                else Move(int.Parse(m.Value));
            }

            Console.WriteLine(_pos);
        }
    }

    public long Challenge2(string[] input)
    {
        long result = 0;


        return result;
    }
}