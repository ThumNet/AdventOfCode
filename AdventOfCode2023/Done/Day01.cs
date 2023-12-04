namespace AdventOfCode2023;

public class Day01
{
    public long Challenge1(string[] input)
    {
        long result = 0;
        var nrs = new List<int>();

        foreach (var line in input)
        {
            char left = '-', right = '-';
            foreach (var c in line)
            {
                if (char.IsNumber(c))
                {
                    if (left == '-')
                    {
                        left = c;
                    }
                    else
                    {
                        right = c;
                    }
                }
            }

            if (right == '-')
            {
                right = left;
            }
            
            nrs.Add(int.Parse($"{left}{right}"));
        }

        result = nrs.Sum();
        return result;
    }
    public long Challenge2(string[] input)
    {
        long result = 0;
        var nrs = new List<int>();
        
        var options = new List<string>(){ "1", "2", "3", "4", "5", "6", "7", "8", "9", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        
        foreach (var line in input)
        {
            var left = GetLeft(line, options);
            var right = GetRight(line, options);
            var nr1 = options.IndexOf(left) % 9 + 1;
            var nr2 = options.IndexOf(right) % 9 + 1;
            Console.WriteLine($"{left}-{right}");
            nrs.Add(nr1*10 + nr2);
        }
        
        result = nrs.Sum();
        return result;
    }

    private string GetLeft(string line, List<string> options)
    {
        int i = line.Length;
        string r = "";
        foreach (var o in options)
        {
            var li = line.IndexOf(o);
            if (li != -1 && li < i)
            {
                i = li;
                r = o;
            }
        }

        return r;
    }
    
    private string GetRight(string line, List<string> options)
    {
        int i = -1;
        string r = "";
        foreach (var o in options)
        {
            var li = line.LastIndexOf(o);
            if (li > i)
            {
                i = li;
                r = o;
            }
        }

        return r;
    }
}