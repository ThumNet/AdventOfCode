namespace AvantOfCode2022.Done;

public class Day2
{
    public int Challenge1(string[] lines)
    {
        int sum = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            rps opponent = (rps)(line[0] - 64); // A=65 rock, B=66 paper, C=67  scissors
            rps you = (rps)(line[2] - 87); // X=88, Y=89, Z=90

            if (opponent == you)
            {
                sum += (int)you;
                sum += 3;
            }
            else if (you == rps.Rock && opponent == rps.Sciccors
                     || you == rps.Sciccors && opponent == rps.Paper
                     || you == rps.Paper && opponent == rps.Rock)
            {
                sum += (int)you;
                sum += 6;
            }
            else
            {
                sum += (int)you;
            }
        }

        return sum;
    }

    public int Challenge2(string[] lines)
    {
        int sum = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            rps opponent = (rps)(line[0] - 64); // A=65 rock, B=66 paper, C=67  scissors
            int doe = (int)line[2] - 87; // X=88, Y=89, Z=90

            rps you = opponent; // default draw
            if (doe == 1)
            {
                // lose
                if (opponent == rps.Paper) you = rps.Rock;
                else if (opponent == rps.Sciccors) you = rps.Paper;
                else you = rps.Sciccors;
            }
            else if (doe == 3)
            {
                // win
                if (opponent == rps.Paper) you = rps.Sciccors;
                else if (opponent == rps.Sciccors) you = rps.Rock;
                else you = rps.Paper;
            }

            if (opponent == you)
            {
                sum += (int)you;
                sum += 3;
            }
            else if (you == rps.Rock && opponent == rps.Sciccors
                     || you == rps.Sciccors && opponent == rps.Paper
                     || you == rps.Paper && opponent == rps.Rock)
            {
                sum += (int)you;
                sum += 6;
            }
            else
            {
                sum += (int)you;
            }
        }

        return sum;
    }
    
    public enum rps : int
    {
        Rock = 1,
        Paper = 2,
        Sciccors = 3
    }
}

