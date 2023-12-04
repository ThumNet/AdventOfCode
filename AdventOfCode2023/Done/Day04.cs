namespace AdventOfCode2023;

public class Day04
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var cards = input.Select(ScratchCard.FromLine);
        result = cards.Sum(c => c.Score());
        

        return result;
    }

    public class ScratchCard
    {
        public int Nr { get; }
        public List<int> Winning { get; }
        public List<int> Yours { get; }

        private ScratchCard(int nr, List<int> winning, List<int> yours)
        {
            Nr = nr;
            Winning = winning;
            Yours = yours;
        }

        public static ScratchCard FromLine(string line)
        {
            var p = line.Split(":");
            var nr = int.Parse(p[0].Split(' ', StringSplitOptions.RemoveEmptyEntries)[1]);
            p = p[1].Split('|');
            var winning = p[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var yours = p[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            return new ScratchCard(nr, winning, yours);
        }

        public int Score()
        {
            var overlapping = OverlappingNumbers();
            var score = 0;
            if (overlapping > 0)
            {
                score = 1;
                var i = 1;
                while (i < overlapping)
                {
                    score *= 2;
                    i++;
                }
            }
            return score;
        }

        public int OverlappingNumbers()
        {
            return Yours.Intersect(Winning).Count();
        }

        public int CardCount { get; set; } = 1;
    }
    
    public long Challenge2(string[] input)
    {
        long result = 0;

        var originalCards = input.Select(ScratchCard.FromLine).ToList();
        var counts = Enumerable.Repeat(1, originalCards.Count).ToArray();
        for (int o = 0; o < originalCards.Count; o++)
        {
            var overlapping = originalCards[o].OverlappingNumbers();
            Console.WriteLine($"looking {o+1} => overlap {originalCards[o].OverlappingNumbers()} : copies {originalCards[o].CardCount}");
            for (int c = 0; c < originalCards[o].CardCount; c++)
            {
                for (var i = 1; i<= overlapping; i++)
                {
                    if (o+i < originalCards.Count)
                    {
                        var card = originalCards[o + i];
                    
                        card.CardCount +=1;
                        // Console.WriteLine($"card {card.Nr} #{card.CardCount}");
                    }
                }
            }
            
        }

        //originalCards.Dump();

        result = originalCards.Sum(x => x.CardCount);
        return result;
    }
}