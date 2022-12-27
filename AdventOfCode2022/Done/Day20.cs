namespace AdventOfCode2022;

public class Day20
{
    public long Challenge1(string[] input)
    {
        long result = 0;
        var ix = 0;
        var original = new List<(int pos, long nr)>();
        foreach (var line in input)
        {
            original.Add((ix++, long.Parse(line)));
        }

        var shuffled = Shuffle(original, original);

        result = FindChecksum(shuffled);
        return result;
    }

    private long FindChecksum(List<(int pos, long nr)> shuffled)
    {
        var indexes = new [] {1000, 2000, 3000};
        long sum = 0;
        var zeroAt = shuffled.IndexOf(shuffled.Find(s => s.nr == 0));
        foreach (var i in indexes)
        {
            sum += shuffled[(zeroAt + i) % shuffled.Count].nr;
        }

        return sum;
    }

    private List<(int pos, long nr)> Shuffle(List<(int pos, long nr)> numbers, List<(int pos, long nr)> original)
    {
        for (var i = 0; i < original.Count; i++)
        {
            var item = original[i];
            var ix = numbers.IndexOf(item);
            var tmp = numbers.ToArray()[..ix].Concat(numbers.ToArray()[(ix+1)..]).ToList();
            var newpos = (int)((ix + item.nr) % tmp.Count);
            if (newpos < 0) newpos += tmp.Count; // mod can return - value, if so we need positive pos
            
            if (newpos == 0) tmp.Add(item);
            else tmp.Insert(newpos, item);
            numbers = tmp;
        }

        return numbers;
    }

    public long Challenge2(string[] input)
    {
        long result = 0;

        long key = 811589153;
        var ix = 0;
        var original = new List<(int pos, long nr)>();
        foreach (var line in input)
        {
            original.Add((ix++, key * long.Parse(line)));
        }
        
        var tmp = new List<(int pos, long nr)>(original);
        for (int i = 0; i < 10; i++)
        {
            tmp = Shuffle(tmp, original);
        }
        result = FindChecksum(tmp);

        return result;
    }
}