using System.Text.Json;

// ReSharper disable once CheckNamespace
namespace System;

public static class SystemExtensions
{
    /// <summary>
    /// Display object as JSON in console
    /// </summary>
    public static void Dump(this object obj, bool pretty = false)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = pretty
        };
        var json = JsonSerializer.Serialize(obj, options);
        Console.WriteLine(json);
    }
    
    /// <summary>
    /// Retrieve both min and max from 2 values
    /// </summary>
    public static (int min, int max) MinMax(this int value, int otherValue)
    {
        return value > otherValue ? (otherValue, value) : (value, otherValue);
    }
    
    /// <summary>
    /// Flatten a tree structure into a list
    /// </summary>
    public static IEnumerable<T> Flatten<T>(
        this IEnumerable<T> e
        ,   Func<T,IEnumerable<T>> f
    ) => e.SelectMany(c => f(c).Flatten(f)).Concat(e);
}