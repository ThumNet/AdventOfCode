namespace AdventOfCode2022;

public class Day21
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var monkeys = ReadInput(input);
        var newKnown = new HashSet<string>();
        do
        {
            foreach (var (m, dep) in monkeys.Unknown)
            {
                if (monkeys.Known.ContainsKey(dep.Left))
                {
                    monkeys.Unknown[m].LeftNr = monkeys.Known[dep.Left];
                }

                if (monkeys.Known.ContainsKey(dep.Right))
                {
                    monkeys.Unknown[m].RightNr = monkeys.Known[dep.Right];
                }

                if (dep.IsComplete)
                {
                    monkeys.Known[m] = dep.GetValue();
                    newKnown.Add(m);
                }
            }

            foreach (var s in newKnown)
            {
                monkeys.Unknown.Remove(s);
            }
        } while (monkeys.Unknown.Count > 0);

        result = monkeys.Known["root"];

        return result;
    }

    private Monkeys ReadInput(string[] input)
    {
        var result = new Monkeys();
        foreach (var line in input)
        {
            var parts = line.Split(": ");
            if (parts[1].Contains(' '))
            {
                var deps = parts[1].Split(' ');
                result.Unknown[parts[0]] = new DependsOn(deps[0], deps[1][0], deps[2]);
            }
            else
            {
                result.Known[parts[0]] = long.Parse(parts[1]);
            }
        }

        return result;
    }

    public class Monkeys
    {
        public Dictionary<string, long> Known = new();
        public Dictionary<string, DependsOn> Unknown = new();
    }

    public record DependsOn(string Left, char Action, string Right)
    {
        public long? LeftNr { get; set; }
        public long? RightNr { get; set; }
        public bool IsComplete => LeftNr.HasValue && RightNr.HasValue;

        public long GetValue()
        {
            if (!IsComplete) return 0;
            return Action switch
            {
                '+' => LeftNr.Value + RightNr.Value,
                '-' => LeftNr.Value - RightNr.Value,
                '*' => LeftNr.Value * RightNr.Value,
                '/' => LeftNr.Value / RightNr.Value,
                _ => 0
            };
        }
    }

    public long Challenge2(string[] input)
    {
        long result = 0;

        var initial = ReadInput2(input);
        var newKnown = new HashSet<string>();
        
        // With trail and error brute force :(
        //      left changes = 105.786.316.064.631 ==> humn 34294
        //For humn:114390    LEFT:105.786.314.786.004 vs RIGHT:51928434600306
        //For humn:200009684     LEFT:105.783.123.713.733 vs RIGHT:51928434600306
        //For humn:5000000004295 LEFT:25.967.722.439.220 vs RIGHT:51928434600306
        //For humn:3372610019343 LEFT:51946918570866 vs RIGHT:51928434600306
        // For humn:3372610019344 LEFT:51946918570863 vs RIGHT:51928434600306
        // For humn:3372610019345 LEFT:51946918570845 vs RIGHT:51928434600306
        // For humn:3372610019346 LEFT:51946918570830 vs RIGHT:51928434600306
        // For humn:3372610019347 LEFT:51946918570812 vs RIGHT:51928434600306
        // For humn:3372610019348 LEFT:51946918570800 vs RIGHT:51928434600306
        // For humn:3372610019349 LEFT:51946918570779 vs RIGHT:51928434600306
        // For humn:3372610019350 LEFT:51946918570767 vs RIGHT:51928434600306
        // For humn:3372610019351 LEFT:51946918570749 vs RIGHT:51928434600306
        // For humn:3372610019352 LEFT:51946918570731 vs RIGHT:51928434600306
        // For humn:3372610019353 LEFT:51946918570716 vs RIGHT:51928434600306
        // For humn:3372610019354 LEFT:51946918570698 vs RIGHT:51928434600306
        //For humn:3373769000011 LEFT:51928416929379 vs RIGHT:51928434600306
        //For humn:3373766100011 LEFT:51928463224158 vs RIGHT:51928434600306
        //For humn:3373766210815 LEFT:51928461455316 vs RIGHT:51928434600306
        // answer = 3373767893067
        // right steady = 51.928.434.600.306
        Monkeys2 monkeys;
        long humn = 3373767859000;
        int counter = 0;
        do
        {
            monkeys = initial.ForHuman(++humn);
            do
            {
                newKnown.Clear();

                foreach (var (m, dep) in monkeys.Unknown)
                {
                    if (monkeys.Known.ContainsKey(dep.Left))
                    {
                        monkeys.Unknown[m].LeftNr = monkeys.Known[dep.Left];
                    }

                    if (monkeys.Known.ContainsKey(dep.Right))
                    {
                        monkeys.Unknown[m].RightNr = monkeys.Known[dep.Right];
                    }

                    if (dep.IsComplete)
                    {
                        monkeys.Known[m] = dep.GetValue();
                        newKnown.Add(m);
                    }
                }

                foreach (var s in newKnown)
                {
                    monkeys.Unknown.Remove(s);
                }
            } while (monkeys.Unknown.Count > 0);

            Console.WriteLine($"For humn:{humn} LEFT:{monkeys.Known[monkeys.RootLeft]} vs RIGHT:{monkeys.Known[monkeys.RootRight]}");

            // var dif = monkeys.Known[monkeys.RootRight] - monkeys.Known[monkeys.RootLeft];
            // Console.WriteLine(dif);
            // if (dif == 0) break;
            //if (counter++ == 10) break;
        } while (monkeys.Known[monkeys.RootLeft] != monkeys.Known[monkeys.RootRight]);

        result = humn;

        return result;
    }

    private Monkeys2 ReadInput2(string[] input)
    {
        var result = new Monkeys2();
        foreach (var line in input)
        {
            var parts = line.Split(": ");
            if (parts[0] == "humn")
            {
                continue;
            }

            if (parts[1].Contains(' '))
            {
                var deps = parts[1].Split(' ');
                if (parts[0] == "root")
                {
                    result.RootLeft = deps[0];
                    result.RootRight = deps[2];
                }
                else
                {
                    result.Unknown[parts[0]] = new DependsOn(deps[0], deps[1][0], deps[2]);
                }
            }
            else
            {
                result.Known[parts[0]] = long.Parse(parts[1]);
            }
        }
        
        return result;
    }

    public class Monkeys2
    {
        public Dictionary<string, long> Known = new();
        public Dictionary<string, DependsOn> Unknown = new();

        public string RootLeft { get; set; }
        public string RootRight { get; set; }

        public Monkeys2 ForHuman(long humanValue)
        {
            var m = new Monkeys2
            {
                Known = Known.ToDictionary(k => k.Key, v => v.Value),
                Unknown = Unknown.ToDictionary(
                    k => k.Key, 
                    v => new DependsOn(v.Value.Left, v.Value.Action, v.Value.Right)),
                RootLeft = RootLeft,
                RootRight = RootRight
            };
            m.Known["humn"] = humanValue;
            return m;
        }
    }
}