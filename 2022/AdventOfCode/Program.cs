using AvantOfCode;

var input = File.ReadAllLines(@"input.txt");

var day = new Day();
var outcome = day.Handle1(input);
Console.WriteLine(outcome);