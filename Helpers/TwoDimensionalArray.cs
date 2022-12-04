// ReSharper disable once CheckNamespace
namespace System;

public static class TwoDimensionalArray
{
    public static int[,] Create(int rows, int columns) => new int[rows, columns];

    public static int Sum(this int[,] array) => array.Cast<int>().Sum();

    public static int[] ItemsInRow(this int[,] array, int rowIndex)
    {
        var result = new List<int>();
        for (var i = 0; i < array.GetLength(1); i++)
        {
            result.Add(array[rowIndex, i]);
        }

        return result.ToArray();
    }
    
    public static int[] ItemsInColumn(this int[,] array, int columnIndex)
    {
        var result = new List<int>();
        for (var i = 0; i < array.GetLength(0); i++)
        {
            result.Add(array[i, columnIndex]);
        }

        return result.ToArray();
    }
}