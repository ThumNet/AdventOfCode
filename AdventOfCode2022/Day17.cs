namespace AdventOfCode2022;

public class Day17
{
    public enum RockTypes
    {
        // ####
        HLine = 0,
        //  #
        // ###
        //  #
        Plus,
        //   #
        //   #
        // ###
        L,
        // #
        // #
        // #
        // #
        VLine,
        // ##
        // ##
        Box
    }

    public class FallingRock
    {
        public FallingRock(RockTypes type, int level)
        {
            Left = 2;
            Type = type;
            Level = level;
        }

        public RockTypes Type { get; }
        public int Level { get; set; }

        public int Left { get; set; }

        public void Push(char direction)
        {
            if (direction == '<' && Left > 0) Left--;
            if (direction == '>' && Left + Width() < 7) Left++;
        }

        public bool Fall(List<string> levels)
        {
            //if (Level == 0) return true;
            
            if (levels.Count < Level)
            {
                Level--;
                return false;
            }
            
            // now check if block fits on line
            var rockLines = GetLineFilling();
            if (levels.Count == 0)
            {
                levels.AddRange(rockLines);
                return true;
            }
            
            if (FitsOnLine(levels[Level-1], rockLines.Last()))
            {
                Level--;
                return false;
            }
            
            levels.AddRange(rockLines);
            return true;
        }

        private bool FitsOnLine(string level, string rockLine)
        {
            if (level.Length != 7) throw new ArgumentOutOfRangeException(nameof(level), level);
            if (rockLine.Length != 7) throw new ArgumentOutOfRangeException(nameof(rockLine), rockLine);
            
            for (int i = 0; i < 7; i++)
            {
                if (level[i] == '#' && rockLine[i] == '#') return false;
            }

            return true;
        }

        public string[] GetLineFilling()
        {
            var fill = new string(' ', Left);
            return Type switch
            {
                RockTypes.HLine => new[] { Fill($"{fill}####") },
                RockTypes.Plus => new[] { Fill($"{fill} #"), Fill($"{fill}###"), Fill($"{fill} #") },
                RockTypes.L => new[] { Fill($"{fill}  #"), Fill($"{fill}  #"), Fill($"{fill}###") },
                RockTypes.VLine => new[] { Fill($"{fill}#"), Fill($"{fill}#"),Fill($"{fill}#"),Fill($"{fill}#") },
                RockTypes.Box => new[] { Fill($"{fill}##"), Fill($"{fill}##") },
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private string Fill(string input)
        {
            var len = input.Length;
            if (len == 7) return input;
            var add = 7 - len;
            return input + new string(' ', add);
        }

        public int Width()
        {
            return Type switch
            {
                RockTypes.HLine => 4,
                RockTypes.Plus => 3,
                RockTypes.L => 3,
                RockTypes.VLine => 1,
                RockTypes.Box => 2,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public int Height()
        {
            return Type switch
            {
                RockTypes.HLine => 1,
                RockTypes.Plus => 3,
                RockTypes.L => 3,
                RockTypes.VLine => 4,
                RockTypes.Box => 2,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public class Game
    {
        private List<string> _levels = new();

        public void Simulate(string pushes)
        {
            var rockIx = 0;
            var rock = new FallingRock((RockTypes)rockIx, 4);
            Draw(rock);
            do
            {
                foreach (var push in pushes)
                {
                    if (rock.Fall(_levels))
                    {
                        rockIx++;
                        rock = new FallingRock((RockTypes)(rockIx % 5), _levels.Count + 4);
                        Draw(rock);
                    }

                    rock.Push(push);
                    Draw(rock);
                }
            } while (rockIx <= 2022);
        }

        private void Draw(FallingRock rock)
        {
            Console.Clear();
            foreach (var line in rock.GetLineFilling())
            {
                Console.WriteLine(line);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            
            _levels.Reverse();
            foreach (var line in _levels)
            {
                Console.WriteLine(line);
            }

            _levels.Reverse();
        }
    }
    
    public int Challenge1(string[] input)
    {
        int result = 0;

        var pushes = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";
        var g = new Game();
        g.Simulate(pushes);

        return result;
    }
    
    public int Challenge2(string[] input)
    {
        int result = 0;

        return result;
    }
}