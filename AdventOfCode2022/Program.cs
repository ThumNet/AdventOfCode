using System.Diagnostics;
using AvantOfCode2022;

var input = File.ReadAllLines(@"input.txt");

var day = new Day();

var sw = Stopwatch.StartNew();
Console.WriteLine($"Challenge 1: {day.Challenge1(input)}");
sw.Stop();
Console.WriteLine($"Time taken: {sw.Elapsed.ToString()}");
Console.WriteLine();

sw = Stopwatch.StartNew();
Console.WriteLine($"Challenge 2: {day.Challenge2(input)}");
sw.Stop();
Console.WriteLine($"Time taken: {sw.Elapsed.ToString()}");
