namespace AdventOfCode2022.Done;

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

            Rps opponent = (Rps)(line[0] - 64); // A=65 rock, B=66 paper, C=67  scissors
            Rps you = (Rps)(line[2] - 87); // X=88, Y=89, Z=90

            if (opponent == you)
            {
                sum += (int)you;
                sum += 3;
            }
            else if (you == Rps.Rock && opponent == Rps.Sciccors
                     || you == Rps.Sciccors && opponent == Rps.Paper
                     || you == Rps.Paper && opponent == Rps.Rock)
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

            Rps opponent = (Rps)(line[0] - 64); // A=65 rock, B=66 paper, C=67  scissors
            int doe = (int)line[2] - 87; // X=88, Y=89, Z=90

            Rps you = opponent; // default draw
            if (doe == 1)
            {
                // lose
                if (opponent == Rps.Paper) you = Rps.Rock;
                else if (opponent == Rps.Sciccors) you = Rps.Paper;
                else you = Rps.Sciccors;
            }
            else if (doe == 3)
            {
                // win
                if (opponent == Rps.Paper) you = Rps.Sciccors;
                else if (opponent == Rps.Sciccors) you = Rps.Rock;
                else you = Rps.Paper;
            }

            if (opponent == you)
            {
                sum += (int)you;
                sum += 3;
            }
            else if (you == Rps.Rock && opponent == Rps.Sciccors
                     || you == Rps.Sciccors && opponent == Rps.Paper
                     || you == Rps.Paper && opponent == Rps.Rock)
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
    
    public enum Rps : int
    {
        Rock = 1,
        Paper = 2,
        Sciccors = 3
    }
}

