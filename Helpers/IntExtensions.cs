// ReSharper disable once CheckNamespace
namespace System;

public static class IntExtensions
{
    public static (int min, int max) MinMax(this int value, int otherValue)
    {
        return value > otherValue ? (otherValue, value) : (value, otherValue);
    }
}