
namespace AdventOfCode2023;

public class Day05
{
    public long Challenge1(string[] input)
    {
        long result = 0;
        var seeds = input[0][6..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        var maps = ParseMaps(input);
        
        result = FindLowest(seeds, maps);
        return result;
    }

    private static long FindLowest(List<long> seeds, List<Map> maps)
    {
        var from = "seed";
        var lowest = long.MaxValue;
         
        foreach (var seed in seeds)
        {
            var converted = ConvertWithMaps(maps, seed);
            if (converted < lowest)
            {
                lowest = converted;
            }
        }

        return lowest;
    }

    private static long ConvertWithMaps(List<Map> maps, long seed)
    {
        var lookFor = seed;
        foreach (var map in maps)
        {
            var r = map.Ranges.FirstOrDefault(r => r.IsContainedBy(lookFor));
            if (r != null)
            {
                lookFor = r.Convert(lookFor);
            }
        }

        return lookFor;
    }

    private static List<Map> ParseMaps(string[] input)
    {
        var maps = new List<Map>();
        var m = new Map();
        for (int i = 2; i < input.Length; i++)
        {
            var line = input[i];
            if (string.IsNullOrEmpty(line))
            {
                maps.Add(m);
                m = new Map();
                continue;
            }

            if (line.EndsWith(" map:"))
            {
                var parts = line[..^5].Split("-to-");
                m.From = parts[0];
                m.To = parts[1];
                continue;
            }

            var r = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            m.Ranges.Add(new MappedRange
            {
                Dest = r[0],
                Source = r[1],
                Range = r[2],
            });
        }

        maps.Add(m);
        return maps;
    }

    public class Map
    {
        public string From { get; set; }
        public string To { get; set; }
        public List<MappedRange> Ranges { get; set; } = new();
    }

    public class MappedRange
    {
        public long Dest { get; set; }
        public long Source { get; set; }
        public long Range { get; set; }

        public bool IsContainedBy(long x)
        {
            return Source <= x && x <= (Source + Range);
        }

        public long Convert(long src)
        {
            var diff = src - Source;
            return Dest + diff;
        }
    }
    
    
    
    public long Challenge2(string[] input)
    {
        long result = 0;
        var seedRanges = input[0][6..].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        var maps = ParseMaps(input);

        long lowest = long.MaxValue;
        for (var i = 0; i < seedRanges.Count; i += 2)
        {
            Console.WriteLine(seedRanges[i] + " with range "  + seedRanges[i+1]);
            for (var j = 0; j < seedRanges[i+1]; j++)
            {
                long x = ConvertWithMaps(maps, seedRanges[i] + j);
                if (x < lowest)
                {
                    lowest = x;
                }
            }
        }

        result = lowest;
        return result;
    }
}