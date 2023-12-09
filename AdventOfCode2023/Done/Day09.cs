namespace AdventOfCode2023;

public class Day09
{
    public long Challenge1(string[] input)
    {
        long result = 0;
        foreach (var line in input)
        {
            var nrs = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var next = FindNext(nrs);
            Console.WriteLine(next);
            Console.WriteLine();
            result += next;
            //return result;
        }
        
        return result;
    }

    private long FindNext(int[] nrs)
    {
        var lastDiffs = new List<int>();
        var diffs = new List<int>();
        var set = nrs.Select(x => x).ToArray();
        do
        {
            for (int i = 1; i < set.Length; i++)
            {
                diffs.Add(set[i] - set[i-1]);
            }
            lastDiffs.Add(diffs.LastOrDefault());

            set = diffs.ToArray();
            diffs.Clear();
        } while (set.Any(d => d != 0));

        Console.WriteLine($"{nrs[^1]} + {lastDiffs.Sum()}");
        return nrs[^1] + lastDiffs.Sum();
    }

    public long Challenge2(string[] input)
    {
        long result = 0;
        foreach (var line in input)
        {
            var nrs = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var next = FindNext(nrs.Reverse().ToArray());
            Console.WriteLine(next);
            Console.WriteLine();
            result += next;
            //return result;
        }
        
        return result;
    }
}