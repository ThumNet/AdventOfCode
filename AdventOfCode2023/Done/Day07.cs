namespace AdventOfCode2023;

public class Day07
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var hands = input.Select(Hand.Parse).ToList();
        var ordered = hands.OrderDescending();
        //ordered.Dump(true);

        var rank = hands.Count;
        foreach (var h in ordered)
        {
            Console.WriteLine($"{h.Cards} {h.BidAmount} => {rank}");
            result += (h.BidAmount * rank);
            rank--;
        }

        return result;
    }
    
    private const int FiveOfAKind = 10;
    private const int FourOfAKind = 8;
    private const int FullHouse = 6;
    private const int ThreeOffAKind = 5;
    private const int TwoPair = 4;
    private const int OnePair = 2;
    private const int HighCard = 1;
    

    public class Hand : IComparable<Hand>
    {
        private List<char> CardScores = new() { '2', '3', '4', '5','6','7','8','9', 'T', 'J', 'Q', 'K', 'A' };
        
        public string Cards { get; }
        public int BidAmount { get; }
        
        public int CardType { get; private set; }

        private Hand(string cards, int bidAmount)
        {
            Cards = cards;
            BidAmount = bidAmount;
            CardType = DetermineCardType(Cards);
        }

        public static int DetermineCardType(string cards)
        {
            var dict = new Dictionary<char, int>();
            foreach (var c in cards)
            {
                if (dict.ContainsKey(c))
                {
                    dict[c]++;
                }
                else
                {
                    dict[c] = 1;
                }
            }

            var x = dict.ToList();
            return x.Count switch
            {
                1 => FiveOfAKind,
                2 when x[0].Value is 1 or 4 => FourOfAKind,
                2 => FullHouse,
                3 when x.Any(c => c.Value == 3) => ThreeOffAKind,
                3 => TwoPair,
                4 => OnePair,
                _ => HighCard
            };
        }

        public static Hand Parse(string line)
        {
            var p = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return new Hand(p[0], int.Parse(p[1]));
        }

        public int CompareTo(Hand? other)
        {
            //less -1
            //equal 0
            //more 1
            if (other == null)
            {
                return 1;
            }

            if (Cards == other.Cards)
            {
                return 0;
            }

            if (CardType == other.CardType)
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    if (Cards[i] != other.Cards[i])
                    {
                        return CardScores.IndexOf(Cards[i]) > CardScores.IndexOf(other.Cards[i]) ? 1 : -1;
                    }
                }

                return 0;
            }

            return CardType > other.CardType ? 1 : -1;
        }
    }

    public class HandWithJoker : IComparable<HandWithJoker>
    {
        private List<char> CardScores = new() { 'J', '2', '3', '4', '5','6','7','8','9', 'T', 'Q', 'K', 'A' };
        
        public string Cards { get; }
        public int BidAmount { get; }
        
        public int CardType { get; private set; }

        private HandWithJoker(string cards, int bidAmount)
        {
            Cards = cards;
            BidAmount = bidAmount;
            CardType = DetermineCardType();
        }
        
        public static HandWithJoker Parse(string line)
        {
            var p = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return new HandWithJoker(p[0], int.Parse(p[1]));
        }

        private int DetermineCardType()
        {
            if (!Cards.Contains('J'))
            {
                return Hand.DetermineCardType(Cards);
            }

            var jCount = Cards.Count(c => c == 'J');
            if (jCount >= 4)
            {
                return FiveOfAKind;
            }
            
            Console.WriteLine("filtering J");
            var cardsNoJoker = Cards.Replace("J", "");
            var dict = new Dictionary<char, int>();
            foreach (var c in cardsNoJoker)
            {
                if (dict.ContainsKey(c))
                {
                    dict[c]++;
                }
                else
                {
                    dict[c] = 1;
                }
            }

            if (dict.Count == 1)
            {
                return FiveOfAKind;
            }

            if (dict.Count == 4)
            {
                return OnePair;
            }
            
            var maxSame = dict.Select(x => x.Value).OrderDescending().First();
            return (maxSame + jCount) switch
            {
                4 => FourOfAKind,
                3 when dict.Count is 2 => FullHouse,
                3 => ThreeOffAKind
            };
        }

        public int CompareTo(HandWithJoker? other)
        {
            //less -1
            //equal 0
            //more 1
            if (other == null)
            {
                return 1;
            }

            if (Cards == other.Cards)
            {
                return 0;
            }

            if (CardType == other.CardType)
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    if (Cards[i] != other.Cards[i])
                    {
                        return CardScores.IndexOf(Cards[i]) > CardScores.IndexOf(other.Cards[i]) ? 1 : -1;
                    }
                }

                return 0;
            }

            return CardType > other.CardType ? 1 : -1;
        }
    }
    
    public long Challenge2(string[] input)
    {
        long result = 0;
        var hands = input.Select(HandWithJoker.Parse).ToList();
        var ordered = hands.OrderDescending();
        ordered.Dump(true);

        var rank = hands.Count;
        foreach (var h in ordered)
        {
            Console.WriteLine($"{h.Cards} {h.BidAmount} => {rank}");
            result += (h.BidAmount * rank);
            rank--;
        }
        
        return result;
    }
}