namespace AdventOfCode2022;

public class Day
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var monkeys = ParseMonkeysFromInput(input);
        var rounds = 20;
        Monkey monkey;
        for (int r = 0; r < rounds; r++)
        {
            // var itemsPerMonkey = new List<List<int>>(monkeys.Count);
            // for (int m = 0; m < monkeys.Count; m++)
            // {
            //     itemsPerMonkey.Add(new List<int>());
            // }

            for (int m = 0; m < monkeys.Count; m++)
            {
                monkey = monkeys[m];
                if (monkey.Items.Count == 0) continue;
                var (trueItems, falseItems) = monkey.InspectItems();
                
                // if (trueItems.Length > 0) { itemsPerMonkey[monkey.WhenTrueToMonkey].AddRange(trueItems); }
                // if (falseItems.Length > 0) { itemsPerMonkey[monkey.WhenFalseToMonkey].AddRange(falseItems); }
                if (trueItems.Length > 0) { monkeys[monkey.WhenTrueToMonkey].Items.AddRange(trueItems); }
                if (falseItems.Length > 0) { monkeys[monkey.WhenFalseToMonkey].Items.AddRange(falseItems); }
            }
            
            // // reassign all items after the round
            // for (int m = 0; m < monkeys.Count; m++)
            // {
            //     monkeys[m].Items = itemsPerMonkey[m];
            // }
        }

        var ordered = monkeys.OrderByDescending(m => m.InspectionCount).Take(2).ToList();
        result = ordered[0].InspectionCount * ordered[1].InspectionCount;
        
        return result;
    }

    public int Challenge2(string[] input)
    {
        int result = 0;
        
        var monkeys = ParseMonkeysFromInput(input);
        var rounds = 10000;
        Monkey monkey;
        var div = 1;
        for (int m = 0; m < monkeys.Count; m++)
        {
            div *= monkeys[m].DivisibleBy;
        }

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

            if (r is 1 or 20 or 1000 or 2000 or 3000 or 10000)
            {
                Console.WriteLine($"== After round {r} ==");
                for (int m = 0; m < monkeys.Count; m++)
                {
                    Console.WriteLine($"{m} inspected {monkeys[m].InspectionCount}");
                }
                Console.WriteLine();
            }
        }
        
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
            Items = items.Split(',').Select(n => int.Parse(n.Trim())).ToList();
            Operation = operation.Trim();
            DivisibleBy = divisibleBy;
            WhenTrueToMonkey = whenTrueToMonkey;
            WhenFalseToMonkey = whenFalseToMonkey;
            InspectionCount = 0;
        }

        public int InspectionCount { get; set; }

        public string Operation { get; set; }
        public int DivisibleBy { get; }
        public int WhenTrueToMonkey { get; }
        public int WhenFalseToMonkey { get; }

        public List<int> Items { get; set; }

        public (int[] trueItems, int[] falseItems) InspectItems()
        {
            var trueItems = new List<int>();
            var falseItems = new List<int>();
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
        
        public (int[] trueItems, int[] falseItems) InspectItemsWithDivider(int divider)
        {
            var trueItems = new List<int>();
            var falseItems = new List<int>();
            foreach (var level in Items)
            {
                InspectionCount+=1;
                var newLevel = PerformOperation(level) % divider;
                if (newLevel % DivisibleBy == 0) trueItems.Add(newLevel);
                else falseItems.Add(newLevel);
            }
            Items.Clear();
            return (trueItems.ToArray(), falseItems.ToArray());
        }

        private int PerformOperation(int level)
        {
            // possible operations
            // old + old
            // old * old
            // old * 8
            // old + 6
            var parts = Operation.Split(' ');
            var left = parts[0] == "old" ? level : int.Parse(parts[0]);
            var right = parts[2] == "old" ? level : int.Parse(parts[2]);
            return parts[1] == "*" ? left * right : left + right;
        }
    }
}