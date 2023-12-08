using System.Numerics;
using Helpers;

namespace AdventOfCode2023;

public class Day08
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var directions = input[0].ToArray();
        var map = ParseMap(input);

        result = StepsNeeded("AAA", "ZZZ", directions, map);
        return result;
    }
    public BigInteger Challenge2(string[] input)
    {
        BigInteger result = 0;
        
        var directions = input[0].ToArray();
        var map = ParseMap(input);
        var starts = map.Keys.Where(k => k.EndsWith('A')).ToArray();
        var ends = map.Keys.Where(k => k.EndsWith('Z')).ToArray();

        var costs = new Dictionary<(string start, string end), long>();
        foreach (var s in starts)
        {
            foreach (var e in ends)
            {
                var need = StepsNeeded(s, e, directions, map);
                if (need != 0)
                {
                    costs[(s, e)] = need;
                    Console.WriteLine($"{s} -> {e} = {costs[(s, e)]}");
                }
            }
        }

        var nrs = costs.Values.ToArray();
        BigInteger max = nrs.Aggregate<long, BigInteger>(1, (current, n) => current * n);
        bool isDivisable = false;
        do
        {
            isDivisable = false;
            for (int i = 2; i <= 9999; i++)
            {
                if (max % i == 0)
                {
                    var submax = max / i;
                    if (nrs.All(n => submax % n == 0))
                    {
                        Console.WriteLine($"{max} % {i} -> {submax}");
                        max = submax;
                        isDivisable = true;
                        break;
                    }
                }
            }
        } while (isDivisable);

        result = max;
        return result;
    }
    
    public BigInteger Challenge2_WithMathHelpers(string[] input)
    {
        BigInteger result = 0;
        
        var directions = input[0].ToArray();
        var map = ParseMap(input);
        var starts = map.Keys.Where(k => k.EndsWith('A')).ToArray();
        var ends = map.Keys.Where(k => k.EndsWith('Z')).ToArray();

        var costs = new Dictionary<(string start, string end), long>();
        foreach (var s in starts)
        {
            foreach (var e in ends)
            {
                var need = StepsNeeded(s, e, directions, map);
                if (need != 0)
                {
                    costs[(s, e)] = need;
                    Console.WriteLine($"{s} -> {e} = {costs[(s, e)]}");
                }
            }
        }

        var nrs = costs.Values.ToArray();
        return nrs.LeastCommonMultiple();
    }

    public static long StepsNeeded(string start, string end, char[] directions, Dictionary<string, (string, string)> map)
    {
        long step = 0;
        var current = start;
        do
        {
            var d = directions[step++ % directions.Length];
            current = d == 'L' ? map[current].Item1 : map[current].Item2;
            if (step % 1_000_000 == 0)
            {
                return 0;
            }
        } while (current != end);

        return step;
    }

    private Dictionary<string, (string, string)> ParseMap(string[] input)
    {
        var map = new Dictionary<string, (string, string)>();
        for (int i = 2; i < input.Length; i++)
        {
            var p = input[i]
                .Replace("(", "")
                .Replace(")", "")
                .Replace(",", "=")
                .Split("=", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.Trim())
                .ToArray();
            map.Add(p[0], (p[1], p[2]));
        }

        return map;
    }
}