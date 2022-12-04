namespace AdventOfCode2021;

public class Day2
{
    public int Challenge1(string[] input)
    {
        int result = 0;

        var hor = 0;
        var vert = 0;
        foreach (var line in input)
        {
            var parts = line.Split(" ");
            var direction = parts[0];
            var amount = int.Parse(parts[1]);

            switch (direction)
            {
                case "forward":
                    hor += amount;
                    break;
                case "down":
                    vert += amount;
                    break;
                case "up":
                    vert -= amount;
                    break;
            }
        }

        result = hor * vert;

        return result;
    }
    
    public int Challenge2(string[] input)
    {
        int result = 0;

        var hor = 0;
        var vert = 0;
        var aim = 0;
        foreach (var line in input)
        {
            var parts = line.Split(" ");
            var direction = parts[0];
            var amount = int.Parse(parts[1]);

            switch (direction)
            {
                case "forward":
                    hor += amount;
                    vert += (aim * amount);
                    break;
                case "down":
                    aim += amount;
                    break;
                case "up":
                    aim -= amount;
                    break;
            }
        }

        result = hor * vert;
        
        return result;
    }
}