using Helpers;

namespace AdventOfCode2022.Done;

public class Day4
{
    public int Challenge1(string[] input)
    {
        var result = 0;

        for (var i = 0; i < input.Length; i++)
        {
            var numbers = input[i].Split(",");

            var a = IntRange.FromString(numbers[0]);
            var b = IntRange.FromString(numbers[1]);
            
            if (a.IsContainedBy(b) || b.IsContainedBy(a))
            {
                result += 1;
            }
        }

        return result;
    }

    public int Challenge2(string[] input)
    {
        var result = 0;

        for (var i = 0; i < input.Length; i++)
        {
            var numbers = input[i].Split(",");

            var a = IntRange.FromString(numbers[0]);
            var b = IntRange.FromString(numbers[1]);
            
            if (a.HasOverlap(b) || b.HasOverlap(a))
            {
                result += 1;
            }
        }

        return result;
    }
}