using System;

namespace Helpers;

public class IntRange
{
    public IntRange(int nr1, int nr2)
    {
        Start = nr1 < nr2 ? nr1 : nr2;
        End = nr1 < nr2 ? nr2 : nr1;
    }

    public IntRange(string nr1, string nr2)
        : this(int.Parse(nr1), int.Parse(nr2))
    {
    }
    
    public int Start { get; }
    public int End { get; }

    public bool HasOverlap(IntRange other)
    {
        return Start >= other.Start && Start <= other.End;
    }

    public bool IsContainedBy(IntRange other)
    {
        return Start >= other.Start && End <= other.End;
    }


    public int SumOfRange() => SumOfRange(Start, End);
    
    /// <summary>
    /// Sum of Range start - end
    /// For example (start=1, end=10) 1,2,3,4,5,6,7,8,9,10 = 55
    /// Or (start=5, end=8)  5,6,7,8 = 26
    /// </summary>
    public static int SumOfRange(int start, int end)
    {
        return start == 1 
            ? end * (end + 1) / 2
            : SumOfRange(1, end) - SumOfRange(1, start-1);
    }

    public override string ToString()
    {
        return $"{Start}-{End}";
    }

    public static IntRange FromString(string input)
    {
        var parts = input.Split("-");
        if (parts.Length != 2)
        {
            throw new ArgumentOutOfRangeException(nameof(input), $"Syntax expected: 1-5, got {input}");
        }

        return new IntRange(parts[0], parts[1]);
    }
}