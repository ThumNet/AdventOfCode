namespace AdventOfCode2021;

public class Day1
{
    public int Challenge1(string[] input)
    {
        int result = 0;
        
        var prev = int.Parse(input[0]);
        for (var i = 1; i < input.Length; i++)
        {
            var current = int.Parse(input[i]);
            if (current > prev)
            {
                result += 1;
            }

            prev = current;
        }

        return result;
    }
    
    public int Challenge2(string[] input)
    {
        int result = 0;

        var groups = new List<int[]>();
        for (var i= 0; i<input.Length -2;i++)
            groups.Add(new[] { int.Parse(input[i]), int.Parse(input[i+1]), int.Parse(input[i+2]) });

        var prev = groups[0].Sum();
        for (var x = 1; x < groups.Count; x++)
        {
            var current = groups[x].Sum();
            if (current > prev)
            {
                result += 1;
            }

            prev = current;
        }
        return result;
    }
}