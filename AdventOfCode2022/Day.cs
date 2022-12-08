using System.Security.Cryptography;

namespace AvantOfCode2022;

public class Day
{
    public int Challenge1(string[] input)
    {
        var result = 0;

        var forest = ParseForest(input);
        var maxY = forest.GetLength(0);
        var maxX = forest.GetLength(1);

        result += (maxX * 2) + ((maxY - 2) * 2);
        Console.WriteLine(result);

        for (int y = 1; y < maxY - 1; y++)
        {
            for (int x = 1; x < maxY - 1; x++)
            {
                var seen = CanBeSeen(forest, y, x);
                Console.WriteLine($"{forest[y, x]}->{seen}");
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
        for (int i = y-1; i > 0; i--)
        {
            if (forest[i, x] > treeHeight) return false;
        }

        return true;
    }

    private bool SeenFromBelow(int[,] forest, int treeHeight, int y, int x)
    {
        for (int i = y+1; i < forest.GetLength(0)-1; i++)
        {
            if (forest[i, x] > treeHeight) return false;
        }

        return true;
    }
    
    private bool SeenFromLeft(int[,] forest, int treeHeight, int y, int x)
    {
        for (int i = x-1; i > 0; i--)
        {
            if (forest[y, i] > treeHeight) return false;
        }

        return true;
    }
    
    private bool SeenFromRight(int[,] forest, int treeHeight, int y, int x)
    {
        for (int i = x+1; i < forest.GetLength(1)-1; i++)
        {
            if (forest[y, i] > treeHeight) return false;
        }

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

        return result;
    }
}