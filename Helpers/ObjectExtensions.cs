using System.Text.Json;

// ReSharper disable once CheckNamespace
namespace System;

public static class ObjectExtensions
{
    public static void Dump(this object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        Console.WriteLine(json);
    }
}