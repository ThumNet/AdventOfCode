namespace AdventOfCode2022;

public class Day11
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var monkeys = ParseMonkeysFromInput(input);
        var rounds = 20;
        Monkey monkey;
        for (int r = 0; r < rounds; r++)
        {
            for (int m = 0; m < monkeys.Count; m++)
            {
                monkey = monkeys[m];
                if (monkey.Items.Count == 0) continue;
                var (trueItems, falseItems) = monkey.InspectItems();
                
                if (trueItems.Length > 0) { monkeys[monkey.WhenTrueToMonkey].Items.AddRange(trueItems); }
                if (falseItems.Length > 0) { monkeys[monkey.WhenFalseToMonkey].Items.AddRange(falseItems); }
            }
        }

        var ordered = monkeys.OrderByDescending(m => m.InspectionCount).Take(2).ToList();
        result = ordered[0].InspectionCount * ordered[1].InspectionCount;
        
        return result;
    }

    public long Challenge2(string[] input)
    {
        long result = 0;
        
        var monkeys = ParseMonkeysFromInput(input);
        var rounds = 10000;
        Monkey monkey;
        var div = 1;
        for (int m = 0; m < monkeys.Count; m++)
        {
            div *= monkeys[m].DivisibleBy;
        }

        Console.WriteLine($"divider = {div}");
        
        for (int r = 1; r <= rounds; r++)
        {
            for (int m = 0; m < monkeys.Count; m++)
            {
                monkey = monkeys[m];
                if (monkey.Items.Count == 0) continue;
                var (trueItems, falseItems) = monkey.InspectItemsWithDivider(div);
                
                if (trueItems.Length > 0) { monkeys[monkey.WhenTrueToMonkey].Items.AddRange(trueItems); }
                if (falseItems.Length > 0) { monkeys[monkey.WhenFalseToMonkey].Items.AddRange(falseItems); }
            }
        }
        
        var ordered = monkeys.OrderByDescending(m => m.InspectionCount).Take(2).ToList();
        result = ordered[0].InspectionCount * ordered[1].InspectionCount;
        
        return result;
    }
    
    private List<Monkey> ParseMonkeysFromInput(string[] input)
    {
        var monkeys = new List<Monkey>();
        for (var i = 0; i < input.Length; i += 7)
        {
            monkeys.Add(new Monkey(
                input[i+1].Split(':')[1],
                input[i+2].Split('=')[1],
                int.Parse(input[i+3].Split("by ")[1]),
                int.Parse(input[i+4].Split("monkey ")[1]),
                int.Parse(input[i+5].Split("monkey ")[1])
            ));
        }

        return monkeys;
    }

    public class Monkey
    {
        public Monkey(string items, string operation, int divisibleBy, int whenTrueToMonkey, int whenFalseToMonkey)
        {
            Items = items.Split(',').Select(n => long.Parse(n.Trim())).ToList();
            Operation = operation.Trim();
            DivisibleBy = divisibleBy;
            WhenTrueToMonkey = whenTrueToMonkey;
            WhenFalseToMonkey = whenFalseToMonkey;
            InspectionCount = 0;
        }

        public long InspectionCount { get; set; }

        public string Operation { get; set; }
        public int DivisibleBy { get; }
        public int WhenTrueToMonkey { get; }
        public int WhenFalseToMonkey { get; }

        public List<long> Items { get; set; }

        public (long[] trueItems, long[] falseItems) InspectItems()
        {
            var trueItems = new List<long>();
            var falseItems = new List<long>();
            foreach (var level in Items)
            {
                InspectionCount++;
                var newLevel = PerformOperation(level) / 3;
                if (newLevel % DivisibleBy == 0) trueItems.Add(newLevel);
                else falseItems.Add(newLevel);
            }
            Items.Clear();
            return (trueItems.ToArray(), falseItems.ToArray());
        }
        
        public (long[] trueItems, long[] falseItems) InspectItemsWithDivider(long divider)
        {
            var trueItems = new List<long>();
            var falseItems = new List<long>();
            foreach (var level in Items)
            {
                InspectionCount++;
                var newLevel = PerformOperation(level) % divider;
                if (newLevel % DivisibleBy == 0) trueItems.Add(newLevel);
                else falseItems.Add(newLevel);
            }
            Items.Clear();
            return (trueItems.ToArray(), falseItems.ToArray());
        }

        private long PerformOperation(long level)
        {
            // possible operations
            // old + old
            // old * old
            // old * 8
            // old + 6
            var parts = Operation.Split(' ');
            var left = parts[0] == "old" ? level : long.Parse(parts[0]);
            var right = parts[2] == "old" ? level : long.Parse(parts[2]);
            return parts[1] == "*" ? left * right : left + right;
        }
    }
}