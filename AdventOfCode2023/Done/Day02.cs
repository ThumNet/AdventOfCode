namespace AdventOfCode2023;

public class Day02
{
    public long Challenge1(string[] input)
    {
        long result = 0;
        int maxRed = 12, maxGreen = 13, maxBlue = 14;

        var games = new List<int>();

        foreach (var line in input)
        {
            var p = line.Split(":");
            var nr = int.Parse(p[0].Split(" ")[1]);
            var turns = p[1].Split(";", StringSplitOptions.RemoveEmptyEntries);
            var good = true;
            foreach (var t in turns)
            {
                var cubes = t.Split(",", StringSplitOptions.RemoveEmptyEntries);
                int rc= 0, gc = 0, bc = 0;
                foreach (var c in cubes)
                {
                    var cCount = c.Trim().Split(" ");
                    if (cCount[1] == "red" && int.Parse(cCount[0]) > maxRed) good = false;
                    if (cCount[1] == "green" && int.Parse(cCount[0]) > maxGreen) good = false;
                    if (cCount[1] == "blue" && int.Parse(cCount[0]) > maxBlue) good = false;
                }
            }

            if (good)
            {
                games.Add(nr);
            }
        }

        result = games.Sum();
        return result;
    }
    public long Challenge2(string[] input)
    {
        long result = 0;

        var games = new List<int>();

        foreach (var line in input)
        {
            var p = line.Split(":");
            var nr = int.Parse(p[0].Split(" ")[1]);
            var turns = p[1].Split(";", StringSplitOptions.RemoveEmptyEntries);
            
            int maxRed = 0, maxGreen = 0, maxBlue = 0;
            foreach (var t in turns)
            {
                var cubes = t.Split(",", StringSplitOptions.RemoveEmptyEntries);
                int rc= 0, gc = 0, bc = 0;
                foreach (var c in cubes)
                {
                    var cCount = c.Trim().Split(" ");
                    if (cCount[1] == "red" && int.Parse(cCount[0]) > maxRed) maxRed = int.Parse(cCount[0]);
                    if (cCount[1] == "green" && int.Parse(cCount[0]) > maxGreen) maxGreen = int.Parse(cCount[0]);
                    if (cCount[1] == "blue" && int.Parse(cCount[0]) > maxBlue) maxBlue = int.Parse(cCount[0]);
                }
            }

            games.Add(maxRed * maxGreen * maxBlue);
        }

        result = games.Sum();
        return result;
    }
}