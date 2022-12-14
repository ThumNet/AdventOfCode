// ReSharper disable once CheckNamespace
namespace System;

public static class TwoDimensionalArray
{
    public static int[,] Create(int rows, int columns) => new int[rows, columns];
    
    public static T[,] Create<T>(int rows, int columns, T defaultValue)
    {
        var array = new T[rows, columns];
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                array[i, j] = defaultValue;
            }
        }
        return array;
    }

    public static int Sum<T>(this T[,] array, Func<T, int> selector) => array.Cast<T>().Sum(selector);

    public static T[] ItemsInRow<T>(this T[,] array, int rowIndex)
    {
        var result = new List<T>();
        for (var i = 0; i < array.GetLength(1); i++)
        {
            result.Add(array[rowIndex, i]);
        }

        return result.ToArray();
    }

    public static bool Contains<T>(this T[,] array, Func<T, bool> predicate)
    {
        for (var r = 0; r < array.GetLength(0); r++)
        for (var c = 0; c < array.GetLength(1); c++)
        {
            if (predicate(array[r,c])) return true;
        }
        return false;
    }
    
    public static (int row, int col) LocationOf<T>(this T[,] array, Func<T, bool> predicate)
    {
        for (var r = 0; r < array.GetLength(0); r++)
        for (var c = 0; c < array.GetLength(1); c++)
        {
            if (predicate(array[r,c])) return (r,c);
        }
        return (-1, -1);
    }
    
    public static T[] ItemsInColumn<T>(this T[,] array, int columnIndex)
    {
        var result = new List<T>();
        for (var i = 0; i < array.GetLength(0); i++)
        {
            result.Add(array[i, columnIndex]);
        }

        return result.ToArray();
    }
}