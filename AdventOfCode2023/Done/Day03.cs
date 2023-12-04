namespace AdventOfCode2023;

public class Day03
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var nrs = ReadNumbers(input);

        foreach (var nr in nrs)
        {
            if (SymbolAdjacent(nr, input))
            {
                result += nr.Number;
            }
            else
            {
                Console.WriteLine($"excluding: {nr.Number}");
            }
        }

        return result;
    }

    private List<NumberPos> ReadNumbers(string[] input)
    {
        var nrs = new List<NumberPos>();

        var lc = 0;
        foreach (var line in input)
        {
            var nr = "";
            var isNr = false;
            var nrS = -1;
            for (int i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (char.IsNumber(c))
                {
                    if (!isNr)
                    {
                        nrS = i;
                    }
                    isNr = true;
                    nr += c;
                }
                else
                {
                    if (isNr)
                    {
                        nrs.Add(new NumberPos
                        {
                            StartPos = nrS,
                            EndPos = i,
                            LineNr = lc,
                            Number = int.Parse(nr)
                        });
                        nr = "";
                    }
                    isNr = false;
                }
            }
            
            if (isNr)
            {
                nrs.Add(new NumberPos
                {
                    StartPos = nrS,
                    EndPos = line.Length,
                    LineNr = lc,
                    Number = int.Parse(nr)
                });
            }

            lc++;
        }

        return nrs;
    }

    private bool SymbolAdjacent(NumberPos nr, string[] input)
    {
        var ls = nr.LineNr == 0 ? 0 : nr.LineNr - 1;
        var le = nr.LineNr == input.Length - 1 ? nr.LineNr : nr.LineNr + 1;
        var ps = nr.StartPos == 0 ? 0 : nr.StartPos - 1;
        var pe = nr.EndPos == input[0].Length ? nr.EndPos : nr.EndPos + 1;
        var text = input[ls].Substring(ps, pe-ps) 
                   + input[nr.LineNr].Substring(ps, pe-ps)
                   + input[le].Substring(ps, pe-ps);
        var symbols = text.Replace(".", "").Replace(nr.Number.ToString(), "");
        //Console.WriteLine($"{text} => {symbols}");
        return symbols.Length > 0;

    }

    public class NumberPos
    {
        public int LineNr { get; set; }
        public int StartPos { get; set; }
        public int EndPos { get; set; }
        public int Number { get; set; }
    } 
    
    public long Challenge2(string[] input)
    {
        long result = 0;

        var nrs = ReadNumbers(input);
        var gears = ReadGears(input);

        gears.Dump();

        foreach (var g in gears)
        {
            var adjacentNrs = NumbersAdjacent(g, nrs).ToList();
            if (adjacentNrs.Count == 2)
            {
                result += adjacentNrs[0].Number * adjacentNrs[1].Number;
            }
        }
        
        return result;
    }

    private IEnumerable<NumberPos> NumbersAdjacent(GearPos gearPos, List<NumberPos> nrs)
    {
        foreach (var nr in nrs)
        {
            if (gearPos.IsAdjacent(nr))
            {
                yield return nr;
            }
        }
    }

    private List<GearPos> ReadGears(string[] input)
    {
        var gears = new List<GearPos>();
        for (int l = 0; l < input.Length; l++)
        {
            for (int i = 0; i < input[l].Length; i++)
            {
                if (input[l][i] == '*')
                {
                    gears.Add(new GearPos{ LineNr = l, Pos = i });
                }
            }
        }

        return gears;
    }

    public class GearPos
    {
        public int LineNr { get; set; }
        public int Pos { get; set; }

        public bool IsAdjacent(NumberPos nr)
        {
            var ls = nr.LineNr - 1;
            var le = nr.LineNr + 1;
            var ps = nr.StartPos - 1;
            var pe = nr.EndPos + 1;

            return LineNr >= ls && LineNr <= le 
                && Pos >= ps && Pos < pe;
        }
    }
}