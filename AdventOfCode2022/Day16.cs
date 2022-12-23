using System.Xml;
using Helpers;

namespace AdventOfCode2022;

public class Day16
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var g = new Graph(input.Length+1, false);
        var nodes = new Dictionary<string, Valve>();
        var tunnels = new Dictionary<string, string[]>();
        var ix = 1;
        foreach (var line in input)
        {
            //Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
            var parts = line.Replace("Valve ", "")
                .Replace(" has flow rate", "")
                .Replace(" tunnels lead to valves ", "")
                .Replace(" tunnel leads to valve ", "")
                .Split(new[] { '=', ';' });
            var n = new Valve(parts[0], int.Parse(parts[1]), ix++, parts[2].Split(", "));
            nodes.Add(parts[0], n);
            tunnels.Add(n.Name, n.Connections);
        }

        var dists = new Dictionary<string, (int, Dictionary<string, int>)>();
        var notempty = new List<string>();
        var visited = new List<string>();
        
        foreach (var n in nodes.Values)
        {
            if (n.Name != "AA" && n.Flowrate == 0) continue;
            if (n.Name != "AA") notempty.Add(n.Name);

            dists[n.Name] = (0, new Dictionary<string, int> {{ "AA", 0 }});
            visited.Add(n.Name);

            var queue = new Queue<(int, string)>();
            queue.Enqueue((0, n.Name));

            while (queue.Count > 0)
            {
                var (dist, pos) = queue.Dequeue();
                foreach (var neighbor in tunnels[pos])
                {
                    if (visited.Contains(neighbor)) continue;
                    visited.Add(neighbor);

                    if (nodes.ContainsKey(neighbor))
                        dists[n.Name].Item2[neighbor] = dist + 1;
                    queue.Append((dist + 1, neighbor));
                }
            }

            dists[n.Name].Item2.Remove(n.Name);
            if (n.Name != "AA") dists[n.Name].Item2.Remove("AA");
        }

        var indexes = new Dictionary<string, int>();
        for (var i = 0; i < notempty.Count; i++)
        {
            indexes[notempty[i]] = i;
        }

        var cache = new Dictionary<(int time, string valve, int bitmask), int>();

        int dfs(int time, string valve, int bitmask)
        {
            if (cache.ContainsKey((time, valve, bitmask))) return cache[(time, valve, bitmask)];

            var maxval = 0;
            foreach (var neighbor in dists[valve].Item2.Keys)
            {
                var bit = 1 << indexes[neighbor];
                if ((bitmask & bit) != 0) continue;

                var remtime = time - dists[valve].Item2[neighbor] - 1;
                if (remtime <= 0) continue;

                maxval = Math.Max(maxval, dfs(remtime, neighbor, bitmask | bit) + nodes[neighbor].Flowrate * remtime);
            }

            cache[(time, valve, bitmask)] = maxval;
            return maxval;
        }

        result = dfs(30, "AA", 0);
        return result;
    }

    public record Valve(string Name, int Flowrate, int Nr, string[] Connections) : IGraphNode
    {
        public override string ToString()
        {
            return Name;
        }
    }

    public long Challenge2(string[] input)
    {
        long result = 0;

        
        return result;
    }
}