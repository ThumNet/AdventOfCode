
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdventOfCode2022;

public class Day13
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
        return result.Value;
    }

    private bool? Compare(JArray left, JArray right)
    {
        for (var i = 0; i < left.Count(); i++)
        {
            if (i >= right.Count)
            {
                Console.WriteLine("no items right");
                break;
            }
            var l = left[i];
            var r = right[i];
            if (l.Type == r.Type && l.Type == JTokenType.Integer)
            {
                Console.WriteLine($"Compare {l} vs {r}");
                int lv = l.Value<int>();
                int rv = r.Value<int>();
                if (lv == rv) continue;
                Console.WriteLine("{0} side is smaller", lv < rv ? "Left" : "Right");
                return lv < rv;
            }
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
                var result = Compare((JArray)l, (JArray)r);
                if (result.HasValue) return result.Value;
            }
        }

        if (left.Count() > right.Count())
        {
            Console.WriteLine("Right side ran out of items");
            return false;
        }
        
        if (left.Count() < right.Count())
        {
            Console.WriteLine("Left side ran out of items");
            return true;
        }

        return null;
    }


    public long Challenge2(string[] input)
    {
        long result = 0;

        var items = input.Where(l => !string.IsNullOrEmpty(l)).ToList();
        items.Add("[[2]]");
        items.Add("[[6]]");
        
        items.Sort(CompareJaggedArrays);

        var index1 = items.IndexOf("[[2]]")+1;
        var index2 = items.IndexOf("[[6]]")+1;

        result = index1 * index2;
        
        return result;
    }

    public int CompareJaggedArrays(string left, string right)
    {
        var result = Compare(JsonConvert.DeserializeObject<JArray>(left), 
            JsonConvert.DeserializeObject<JArray>(right));
        return result.Value ? -1 : 1;
    }
}