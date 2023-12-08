namespace AdventOfCode2023;

public class Day08
{
    public long Challenge1(string[] input)
    {
        long result = 0;
        return 0;

        var directions = input[0].ToArray();
        var map = ParseMap(input);

        var start = "AAA";
        var end = "ZZZ";
        var step = 0;
        var current = start;
        do
        {
            var d = directions[step++ % directions.Length];
            current = d == 'L' ? map[current].Item1 : map[current].Item2;
        } while (current != end);

        result = step;
        return result;
    }
    public long Challenge2(string[] input)
    {
        long result = 0;
        
        var directions = input[0].ToArray();
        var map = ParseMap(input);

        var starts = map.Keys.Where(k => k.EndsWith('A')).ToArray();

        long step = 0;
        var currents = starts.Select(x => x).ToArray();
        do
        {
            var d = directions[step++ % directions.Length];
            for (int i = 0; i < currents.Length; i++)
            {
                currents[i] = d == 'L' ? map[currents[i]].Item1 : map[currents[i]].Item2;
            }
        } while (!currents.All(c => c.EndsWith('Z')));

        result = step;        
        return result;
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