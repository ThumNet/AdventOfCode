namespace AdventOfCode2022;

public class Day16
{
    public long Challenge1(string[] input)
    {
        long result = 0;

        var valves = ParseInput(input);
        result = DepthFirstSearch(valves, "AA");

        return result;
    }

    private long DepthFirstSearch(IEnumerable<Valve> valves, string startValve, int maxDepth = 30,
        int maxExplore = 1000)
    {
        var flowRates = valves.ToDictionary(k => k.Name, v => v.Flowrate);
        var neighbours = valves.ToDictionary(k => k.Name, v => v.Neighbors);

        var currentDepth = 0;

        var queue = new List<(int depth, int flowrate, string node, HashSet<string> open)>();
        queue.Add((currentDepth, 0, startValve, new HashSet<string>()));


        while (queue.Count > 0)
        {
            //Console.WriteLine($"{queue.Count}, {queue[0].node}");
            if (queue[0].depth > currentDepth)
            {
                currentDepth = queue[0].depth;
                queue = queue.OrderByDescending(x => x.flowrate).Take(maxExplore).ToList();
            }

            var (depth, flowrate, node, open) = queue[0];
            queue.RemoveAt(0);
            if (depth == maxDepth) return flowrate;

            if (!open.Contains(node) && flowRates[node] > 0)
            {
                var remaining = maxDepth - depth - 1;
                var newFlowRate = flowrate + flowRates[node] * remaining;
                queue.Add((depth + 1, newFlowRate, node, new HashSet<string>(open) { node }));
            }

            foreach (var neighbour in neighbours[node])
            {
                queue.Add((depth + 1, flowrate, neighbour, new HashSet<string>(open)));
            }
        }

        return 0;
    }

    private IEnumerable<Valve> ParseInput(string[] input)
    {
        foreach (var line in input)
        {
            var parts = line.Replace("Valve ", "")
                .Replace(" has flow rate", "")
                .Replace(" tunnels lead to valves ", "")
                .Replace(" tunnel leads to valve ", "")
                .Split(new[] { '=', ';' });
            yield return new Valve(parts[0], int.Parse(parts[1]), parts[2].Split(", "));
        }
    }

    private record Valve(string Name, int Flowrate, string[] Neighbors);


    public long Challenge2(string[] input)
    {
        long result = 0;
        var valves = ParseInput(input);
        result = DepthFirstSearchPart2(valves, "AA");
        return result;
    }

    private long DepthFirstSearchPart2(IEnumerable<Valve> valves, string startValve, int maxDepth = 26,
        int maxExplore = 10000)
    {
        var flowRates = valves.ToDictionary(k => k.Name, v => v.Flowrate);
        var neighbours = valves.ToDictionary(k => k.Name, v => v.Neighbors);

        var currentDepth = 0;

        var queue = new List<(int depth, int flowrate, (string nodeMe, string nodeEle), HashSet<string> open)>();
        queue.Add((currentDepth, 0, (startValve, startValve), new HashSet<string>()));


        while (queue.Count > 0)
        {
            if (queue[0].depth > currentDepth)
            {
                currentDepth = queue[0].depth;
                queue = queue.OrderByDescending(x => x.flowrate).Take(maxExplore).ToList();
            }

            var (depth, flowrate, (nodeMe, nodeEle), open) = queue[0];
            queue.RemoveAt(0);
            if (depth == maxDepth) return flowrate;

            var actionsMe = new List<(bool openValve, string newNode)>();
            if (!open.Contains(nodeMe) && flowRates[nodeMe] > 0) actionsMe.Add((true, nodeMe));
            foreach (var n in neighbours[nodeMe])
            {
                actionsMe.Add((false, n));
            }

            var actionsEle = new List<(bool openValve, string newNode)>();
            if (!open.Contains(nodeEle) && flowRates[nodeEle] > 0) actionsEle.Add((true, nodeMe));
            foreach (var n in neighbours[nodeEle])
            {
                actionsEle.Add((false, n));
            }

            foreach (var (aMe, aEle) in Product(actionsMe, actionsEle))
            {
                var newFlowRate = flowrate;
                var newOpen = new HashSet<string>(open);
                var newNodes = (nodeMe, nodeEle);
                if (aMe.openValve)
                {
                    newFlowRate += flowRates[nodeMe] * (maxDepth - depth - 1);
                    newOpen.Add(nodeMe);
                }
                else newNodes.nodeMe = aMe.newNode;

                if (aEle.openValve)
                {
                    if (!newOpen.Contains(nodeEle))
                    {
                        newFlowRate += flowRates[nodeEle] * (maxDepth - depth - 1);
                        newOpen.Add(nodeEle);
                    }
                }
                else newNodes.nodeEle = aEle.newNode;

                queue.Add((depth + 1, newFlowRate, newNodes, newOpen));
            }
        }

        return 0;
    }

    public static List<Tuple<T, T>> Product<T>(List<T> a, List<T> b)
        where T : struct
    {
        List<Tuple<T, T>> result = new List<Tuple<T, T>>();

        foreach (T t1 in a)
        {
            foreach (T t2 in b)
                result.Add(Tuple.Create<T, T>(t1, t2));
        }

        return result;
    }
}