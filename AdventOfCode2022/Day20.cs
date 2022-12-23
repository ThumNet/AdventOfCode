namespace AdventOfCode2022;

public class Day20
{
    public long Challenge1(string[] input)
    {
        long result = 0;
        var original = input.Select(int.Parse).ToArray();
        var modified = input.Select(int.Parse).ToList();

        var prevLoc = 0;
        for (var i = 0; i < original.Length; i++)
        {
            var move = original[i];
            if (move == 0) continue;
            
            modified.RemoveAt(i);
            if (move < 0)
            {
                var tmp = prevLoc + move;
                if (tmp < 0) tmp = original.Length + tmp;
                modified.Insert(tmp, original[i]);
                prevLoc = tmp;

            }
            else
            {
                var tmp = prevLoc + move;
                modified.Insert(tmp, original[i]);
                prevLoc = tmp;

            }

            Console.WriteLine($"{move} moves");
            modified.Dump();
        }


        return result;
    }

    public long Challenge2(string[] input)
    {
        long result = 0;

        
        return result;
    }
}