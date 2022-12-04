namespace AdventOfCode2021;

public class Day3
{
    public int Challenge1(string[] input)
    {
        int result = 0;

        var nrlines = input.Length;
        var linelen = input[0].Length;
        var cols = new List<char[]>(linelen);
        for (var x = 0; x < linelen; x++)
        {
            for (var y = 0; y < nrlines; y++)
            {
                if (x == 0)
                {
                    cols.Add(new char[nrlines]);
                }
                cols[x][y] = input[y][x];
            }
        }

        var gamma = "";
        var epsilon = "";
        
        for (var j = 0; j < linelen; j++)
        {
            var oneCount = cols[j].Count(c => c == '1');
            var zeroCount = cols[j].Count(c => c == '0');

            if (oneCount > zeroCount)
            {
                gamma += '1';
                epsilon += '0';
            }
            else
            {
                gamma += '0';
                epsilon += '1';
            }
        }

        Console.WriteLine(gamma);
        Console.WriteLine(epsilon);
        
        result = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);

        return result;
    }
    
    public int Challenge2(string[] input)
    {
        int result = 0;

        var remaining = (string[])input.Clone();
        var linelen = input[0].Length;
        var oxygen = "";
        char most;
        for (var x = 0; x < linelen; x++)
        {
            most = CalcMostChar(remaining, x);
            remaining = remaining.Where(s => s[x] == most).ToArray();
            if (remaining.Length == 1)
            {
                oxygen = remaining[0];
                break;
            }
        }
        
        remaining = (string[])input.Clone();
        char least;
        string co2 = "";
        for (var x = 0; x < linelen; x++)
        {
            least = CalcMostChar(remaining, x) == '1' ? '0' : '1';
            remaining = remaining.Where(s => s[x] == least).ToArray();
            if (remaining.Length == 1)
            {
                co2 = remaining[0];
                break;
            }
        }
        
        Console.WriteLine(oxygen);
        Console.WriteLine(co2);

        result = Convert.ToInt32(oxygen, 2) * Convert.ToInt32(co2, 2);
        
        return result;
    }

    private char CalcMostChar(string[] remaining, int i)
    {
        var count1 = 0;
        var count0 = 0;
        foreach (var s in remaining)
        {
            if (s[i] == '0') count0++;
            if (s[i] == '1') count1++;
        }
        return count1 >= count0 ? '1' : '0';
    }
}