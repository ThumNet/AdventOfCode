namespace AvantOfCode2022;

public class Day6
{
    public int Challenge1(string[] input)
    {
        var result = 0;

        foreach (var line in input)
        {
            var x = FindMarker(line, 4);
            x.Dump();
        }

        return result;
    }

    private int FindMarker(string line, int len)
    {
        for (var i = 0; i < line.Length - len; i++)
        {
            if (line[i..(i+len)].Distinct().Count() == len)
            {
                return i + len;
            }
        }

        return -1;
    }

    public int Challenge2(string[] input)
    {
        var result = 0;

        foreach (var line in input)
        {
            return FindMarker(line, 14);
        }
        
        return result;
    }
}