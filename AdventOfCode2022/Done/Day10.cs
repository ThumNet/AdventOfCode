namespace AdventOfCode2022;

public class Day10
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var sys = new Sys();
        foreach (var line in input)
        {
            if (line == "noop")
            {
                sys.Cycle++;
                CheckCycle(sys);
            }
            else
            {
                sys.Cycle++;
                CheckCycle(sys);
                var x = int.Parse(line.Split(' ')[1]);
                sys.X += x;
                sys.Cycle++;
                CheckCycle(sys);
            }
        }

        result = sys.Registers.Sum();
        
        return result;
    }

    public class Sys
    {
        public List<long> Registers { get; set; } = new();
        public int CycleCheck { get; set; } = 20;
        public int X { get; set; } = 1;
        public long Cycle { get; set; } = 1;
    }

    private void CheckCycle(Sys sys)
    {
        if (sys.Cycle % sys.CycleCheck == 0)
        {
            sys.Registers.Add(sys.CycleCheck * sys.X);
            Console.WriteLine($"cycle {sys.Cycle} => {sys.Registers.Last()}");
            sys.CycleCheck += 40;
        }
    }

    public int Challenge2(string[] input)
    {
        int result = 0;

        var sys = new Sys2();
        Console.Write('#'); // HACK, why the first one is missing i don't know...
        foreach (var line in input)
        {
            if (line == "noop")
            {
                DrawPixel(sys);
                sys.Cycle++;
            }
            else
            {
                DrawPixel(sys);
                sys.Cycle++;
                var x = int.Parse(line.Split(' ')[1]);
                sys.X += x;
                DrawPixel(sys);
                sys.Cycle++;
            }
        }

        return result;
    }
    
    public class Sys2
    { 
        public int X { get; set; } = 1;
        public long Cycle { get; set; } = 1;
        public int SpritePos { get; set; } = 1;
    }

    public void DrawPixel(Sys2 sys)
    {
        var output = (sys.X == sys.SpritePos -1 || sys.X == sys.SpritePos || sys.X == sys.SpritePos + 1) 
            ? '#' : '.';
        Console.Write(output);
        
        sys.SpritePos += 1;
        if (sys.SpritePos % 40 == 0)
        {
            Console.WriteLine();
            sys.SpritePos = 0;
        }
    }
}