namespace AdventOfCode2021;

public class Day7
{
    public int Challenge1(string[] input)
    {
        int result = 0;

        var crabs = input[0].Split(',').Select(int.Parse).ToArray();
        var fuelCost = new List<int>();
        for (int i = 0; i < crabs.Max(); i++)
        {
            fuelCost.Add(CalcFuelCostForPosition(i, crabs));
        }

        result = fuelCost.Min();
        
        
        return result;
    }

    private int CalcFuelCostForPosition(int pos, int[] crabs)
    {
        return crabs.Select(c => Math.Abs(pos - c)).Sum();
    }

    public int Challenge2(string[] input)
    {
        int result = 0;

        var crabs = input[0].Split(',').Select(int.Parse).ToArray();
        var fuelCost = new List<int>();
        for (int i = 0; i < crabs.Max(); i++)
        {
            fuelCost.Add(CalcFuelIncrementingCostForPosition(i, crabs));
        }

        result = fuelCost.Min();
        
        return result;
    }
    
    private int CalcFuelIncrementingCostForPosition(int pos, int[] crabs)
    {
        return crabs.SelectMany(c => Enumerable.Range(1, Math.Abs(pos - c))).Sum();
    }

}