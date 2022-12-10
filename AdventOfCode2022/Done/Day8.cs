
namespace AdventOfCode2022.Done;

public class Day8
{
    public int Challenge1(string[] input)
    {
        var result = 0;

        var forest = ParseForest(input);
        var maxY = forest.GetLength(0);
        var maxX = forest.GetLength(1);

        result += (maxX * 2) + ((maxY - 2) * 2);
        // Console.WriteLine(result);

        for (int y = 1; y < maxY - 1; y++)
        {
            for (int x = 1; x < maxY - 1; x++)
            {
                var seen = CanBeSeen(forest, y, x);
                // Console.WriteLine($"{forest[y, x]}->{seen}");
                if (seen)
                {
                    result += 1;
                }
            }
        }

        return result;
    }

    private bool CanBeSeen(int[,] forest, int y, int x)
    {
        var treeHeight = forest[y, x];

        return SeenFromAbove(forest, treeHeight, y, x)
               || SeenFromBelow(forest, treeHeight, y, x)
               || SeenFromLeft(forest, treeHeight, y, x)
               || SeenFromRight(forest, treeHeight, y, x);
    }

    private bool SeenFromAbove(int[,] forest, int treeHeight, int y, int x)
    {
        for (int i = y-1; i >= 0; i--)
        {
            if (treeHeight <= forest[i, x]) return false;
        }

        // Console.WriteLine($"{treeHeight} above");
        return true;
    }

    private bool SeenFromBelow(int[,] forest, int treeHeight, int y, int x)
    {
        for (int i = y+1; i < forest.GetLength(0); i++)
        {
            if (treeHeight <= forest[i, x]) return false;
        }

        // Console.WriteLine($"{treeHeight} below");
        return true;
    }
    
    private bool SeenFromLeft(int[,] forest, int treeHeight, int y, int x)
    {
        for (int i = x-1; i >= 0; i--)
        {
            if (treeHeight <= forest[y, i]) return false;
        }

        // Console.WriteLine($"{treeHeight} left");
        return true;
    }
    
    private bool SeenFromRight(int[,] forest, int treeHeight, int y, int x)
    {
        for (int i = x+1; i < forest.GetLength(1); i++)
        {
            if (treeHeight <= forest[y, i]) return false;
        }

        // Console.WriteLine($"{treeHeight} ({x},{y}) right");
        return true;
    }

    private int[,] ParseForest(string[] input)
    {
        var result = new int[input.Length, input[0].Length];
        for (var y=0; y<input.Length;y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                result[y, x] = int.Parse(input[y][x]+"");
            }
        }

        return result;
    }

    public int Challenge2(string[] input)
    {
        var result = 0;
        
        var forest = ParseForest(input);
        var maxY = forest.GetLength(0);
        var maxX = forest.GetLength(1);

        // return CalcScore(forest, 3, 2);
        for (int y = 1; y < maxY - 1; y++)
        {
            for (int x = 1; x < maxX - 1; x++)
            {
                var score = CalcScore(forest, y, x);
                if (score > result)
                {
                    result = score;
                }
            }
        }

        return result;
    }

    private int CalcScore(int[,] forest, int y, int x)
    {
        var treeHeight = forest[y, x];
        return CalcUp(forest, treeHeight, y, x)
               * CalcLeft(forest, treeHeight, y, x)
               * CalcDown(forest, treeHeight, y, x)
               * CalcRight(forest, treeHeight, y, x);
    }

    private int CalcUp(int[,] forest, int treeHeight, int y, int x)
    {
        var see = 0;
        var i = y - 1;
        do
        {
            see++;
        } while (i > 0 && forest[i--, x] < treeHeight);

        Console.WriteLine($"UP: {treeHeight} => {see}");

        return see;
    }
    
    private int CalcDown(int[,] forest, int treeHeight, int y, int x)
    {
        var see = 0;
        var i = y + 1;
        do
        {
            if (i < forest.GetLength(0)) see++;
        } while (i < forest.GetLength(0) && forest[i++, x] < treeHeight);
        
        Console.WriteLine($"DOWN: {treeHeight} => {see}");
        return see;
    }
    
    private int CalcLeft(int[,] forest, int treeHeight, int y, int x)
    {
        var see = 0;
        var i = x - 1;
        do
        {
            see++;
        } while (i > 0 && forest[y, i--] < treeHeight);
        
        Console.WriteLine($"LEFT: {treeHeight} => {see}");
        return see;
    }
    
    private int CalcRight(int[,] forest, int treeHeight, int y, int x)
    {
        var see = 0;
        var i = x + 1;
        do
        {
            if (i < forest.GetLength(1)) see++;
        } while (i < forest.GetLength(1) && forest[y, i++] < treeHeight);

        Console.WriteLine($"RIGHT: {treeHeight} => {see}");

        return see;
    }
}