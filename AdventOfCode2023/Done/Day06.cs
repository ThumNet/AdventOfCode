namespace AdventOfCode2023;

public class Day06
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var times = input[0]
            .Split(':')[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
        var distances = input[1]
            .Split(':')[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        var score = 1;
        for (int i = 0; i < times.Length; i++)
        {
            var minDistance = distances[i];
            var time = times[i];
            var speed = 1;
            var wins = 0;
            for (int t = 1; t < time; t++)
            {
                //Console.WriteLine($"{time} - {t} = {(time-t)} => {speed}");
                var currentDistance = (time - t) * speed;
                if (minDistance < currentDistance)
                {
                    wins++;
                }

                speed++;
            }

            score *= wins;
        }

        result = score;
        return result;
    }
    public long Challenge2(string[] input)
    {
        long result = 0;
        var time = long.Parse(input[0].Split(':')[1].Replace(" ", ""));
        var minDistance = long.Parse(input[1].Split(':')[1].Replace(" ", ""));

        long speed = 1;
        long wins = 0;
        for (long t = 1; t < time; t++)
        {
            //Console.WriteLine($"{time} - {t} = {(time-t)} => {speed}");
            var currentDistance = (time - t) * speed;
            if (minDistance < currentDistance)
            {
                wins++;
            }

            speed++;
        }

        result = wins;
        return result;
    }
}