namespace Helpers;

public interface IGraphNode
{
    public int Nr { get; }
}

public class Graph
{
    private readonly LinkedList<IGraphNode>[] _adj;
    private readonly bool _direct;
    private readonly int _v;

    public Graph(int v, bool direct)
    {
        _adj = new LinkedList<IGraphNode>[v];
        for (var i = 0; i < _adj.Length; i++) _adj[i] = new LinkedList<IGraphNode>();
        _v = v;
        _direct = direct;
    }

    public void Add_Edge(IGraphNode v, IGraphNode w)
    {
        _adj[v.Nr].AddLast(w);
        if (!_direct) _adj[w.Nr].AddLast(v);
    }

    public void BreadthFirstSearch(IGraphNode s)
    {
        var visited = new bool[_v];
        for (var i = 0; i < _v; i++)
            visited[i] = false;

        // Create a queue for BFS
        var queue1 = new LinkedList<IGraphNode>();
        visited[s.Nr] = true;
        queue1.AddLast(s);

        while (queue1.Any())
        {
            // Dequeue a vertex from queue and print it
            s = queue1.First();
            Console.Write(s + " ");

            queue1.RemoveFirst();
            var list = _adj[s.Nr];
            foreach (var val in list)
            {
                if (!visited[val.Nr])
                {
                    visited[val.Nr] = true;
                    queue1.AddLast(val);
                }
            }
        }
    }
}

