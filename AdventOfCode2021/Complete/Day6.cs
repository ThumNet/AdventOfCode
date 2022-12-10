namespace AdventOfCode2021;

public class Day6
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var state = new long[9];
        var start = input[0].Split(',').Select(int.Parse).ToArray();
        foreach (var i in start)
        {
            state[i] += 1;
        }
        state.Dump();
        result = CalcFishCountAfterDays(80, state);
        
        return result;
    }

    private long CalcFishCountAfterDays(int days, long[] state)
    {
        for (int i = 1; i <= days; i++)
        {
            var tmp = (long[])state.Clone();
            for (int j = 0; j < 8; j++)
            {
                tmp[j] = state[j + 1];
            }
            tmp[6] = state[7] + state[0];
            tmp[8] = state[0];
            state = tmp;
            //Console.WriteLine($"day {i}: {string.Join(',', state)}");
        }

        return state.Sum();
    }

    public long Challenge2(string[] input)
    {
        long result = 0;

        
        var state = new long[9];
        var start = input[0].Split(',').Select(int.Parse).ToArray();
        foreach (var i in start)
        {
            state[i] += 1;
        }
        state.Dump();
        result = CalcFishCountAfterDays(256, state);
        
        return result;
    }

}