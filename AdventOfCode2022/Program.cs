using AvantOfCode;

var input = File.ReadAllLines(@"input.txt");

var day = new Day();

Console.WriteLine($"Challenge 1: {day.Challenge1(input)}");
Console.WriteLine($"Challenge 2: {day.Challenge2(input)}");