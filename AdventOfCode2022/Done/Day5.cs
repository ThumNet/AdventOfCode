namespace AvantOfCode2022;

public class Day5
{
    public int Challenge1(string[] input)
    {
        var result = 0;

        var stacksLines = new List<string>();
        var moves = new List<Move>();
        var intoStacks = true;
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                intoStacks = false;
                continue;
            }

            if (intoStacks) stacksLines.Add(line);
            else moves.Add(ParseMove(line));
        }

        var stacks = CreateStacks(stacksLines);

        foreach (var move in moves)
        {
            for (var i = 0; i < move.Amount; i++)
            {
                var temp = stacks[move.From].Take(1);
                stacks[move.To].InsertRange(0, temp);
                stacks[move.From].RemoveRange(0, 1);
            }

            stacks.Dump();
        }

        var outcome = "";
        foreach (var s in stacks)
        {
            outcome += s[0];
        }

        Console.WriteLine($"OUTCOME: {outcome}");

        return result;
    }

    private List<List<char>> CreateStacks(List<string> stacksLines)
    {
        var stackCount = stacksLines.Last().Replace(" ", "").Length;
        var stacks = new List<List<char>>(stackCount);

        for (var i = 0; i < stacksLines.Count - 1; i++) // minus 1 because the numbers dont matter
        {
            var stackix = 0;
            for (var j = 1; j < stacksLines[i].Length; j += 4)
            {
                if (i == 0) stacks.Add(new List<char>());
                stacks[stackix++].Add(stacksLines[i][j]);
            }
        }

        for (var i = 0; i < stacksLines.Count - 1; i++) // minus 1 because the numbers dont matter
        {
            stacks[i] = stacks[i].Where(s => s != ' ').ToList();
        }

        stacks.Dump();

        return stacks;
    }

    private Move ParseMove(string line)
    {
        var parts = line.Split(" ");
        return new Move
        {
            Amount = int.Parse(parts[1]),
            From = int.Parse(parts[3]) - 1,
            To = int.Parse(parts[5]) - 1,
        };
    }

    public int Challenge2(string[] input)
    {
        var result = 0;

        var stacksLines = new List<string>();
        var moves = new List<Move>();
        var intoStacks = true;
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                intoStacks = false;
                continue;
            }

            if (intoStacks) stacksLines.Add(line);
            else moves.Add(ParseMove(line));
        }

        var stacks = CreateStacks(stacksLines);

        foreach (var move in moves)
        {
            var temp = stacks[move.From].Take(move.Amount);
            stacks[move.To].InsertRange(0, temp);
            stacks[move.From].RemoveRange(0, move.Amount);

            stacks.Dump();
        }

        var outcome = "";
        foreach (var s in stacks)
        {
            outcome += s[0];
        }

        Console.WriteLine($"OUTCOME 2: {outcome}");

        return result;
    }

    public class Move
    {
        public int Amount { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}