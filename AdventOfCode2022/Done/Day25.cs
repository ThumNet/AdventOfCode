namespace AdventOfCode2022;

public class Day25
{
    public long Challenge1(string[] input)
    {
        long result = 0;
        //2,147,483,647
        //62,200,114,669

        var sum = input.Select(l => SnafuToLong(l)).Sum();
        Console.WriteLine($"sum={sum}");
        Console.WriteLine($"snafu={LongToSnafu(sum)}");

        LongToSnafu(1747);
        LongToSnafu(906);
        LongToSnafu(198);
        LongToSnafu(11);
        LongToSnafu(201);
        LongToSnafu(31);
        LongToSnafu(1257);
        LongToSnafu(32);
        LongToSnafu(353);
        LongToSnafu(107);
        LongToSnafu(7);
        LongToSnafu(3);
        LongToSnafu(37);

        return result;
    }

    public long SnafuToLong(string snafu)
    {
        var snafuToNr = new Dictionary<char, int> { { '=', -2 }, { '-', -1 }, { '0', 0 }, { '1', 1 }, { '2', 2 } };

        long res = 0;
        var len = snafu.Length;
        for (var i = len - 1; i >= 0; i--)
        {
            var d = snafuToNr[snafu[len - 1 - i]];
            if (i == 0) res += d;
            else
            {
                res += d * (long)Math.Pow(5, i);
            }
        }

        return res;
    }

    public string LongToSnafu(long number)
    {
        var nrToSnafu = new Dictionary<long, char> { { -2, '=' }, { -1, '-' }, { 0, '0' }, { 1, '1' }, { 2, '2' } };
        var snafu = "";
        while (number > 0)
        {
            var tmp = number % 5;
            if (!nrToSnafu.ContainsKey(tmp))
            {
                tmp -= 5;
                number += 5;
            }

            snafu += nrToSnafu[tmp];
            number /= 5;
        }

        return string.Join("", snafu.Reverse());
    }

    public long Challenge2(string[] input)
    {
        long result = 0;

        return result;
    }
}