
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdventOfCode2022;

public class Day
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var sum = 0;
        var ix = 1;
        for (var i = 0; i < input.Length; i += 3)
        {
            if (Compare(input[i], input[i + 1])) sum += ix;
            ix++;
        }

        result = sum;
        return result;
    }

    private bool Compare(string left, string right)
    { 
        var result = Compare(JsonConvert.DeserializeObject<JArray>(left), 
            JsonConvert.DeserializeObject<JArray>(right));
        Console.WriteLine($"{left} vs {right} -> {result}");
        return result;
    }

    private bool Compare(JArray left, JArray right)
    {
        for (var i = 0; i < left.Count(); i++)
        {
            if (i >= right.Count)
            {
                break;
            }
            var l = left[i];
            var r = right[i];
            if (l.Type == JTokenType.Array && r.Type == JTokenType.Integer)
            {
                Console.WriteLine("Mixed types; convert right");
                r = new JArray(r);
            }
            else if (l.Type == JTokenType.Integer && r.Type == JTokenType.Array)
            {
                Console.WriteLine("Mixed types; convert left");
                l = new JArray(l);
            }

            if (l.Type == JTokenType.Array && r.Type == JTokenType.Array)
            {
                if (!Compare((JArray)l, (JArray)r))
                {
                    return false;
                }
            }
            else if (l.Value<int>() > r.Value<int>())
            {
                Console.WriteLine("Right side is smaller");
                return false;
            }
        }
        
        if (left.Count() > right.Count())
        {
            Console.WriteLine("Right side ran out of items");
            return false;
        }

        return true;
    }


    public long Challenge2(string[] input)
    {
        long result = 0;
        
        return result;
    }
}