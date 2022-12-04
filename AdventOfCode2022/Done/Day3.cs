namespace AvantOfCode2022.Done;

public class Day3
{
    public int Challenge1(string[] input)
    {
        var result = 0;

        foreach (var line in input)
        {
            var mid = line.Length / 2;
            var left = line[..mid];
            var right = line[mid..];
            var intersect = left.ToCharArray().Intersect(right.ToCharArray()).Single();
            if (intersect >= 97 && intersect <= 122)
            {
                // a = 97 -> 1 
                // z = 122 -> 26
                result += (intersect - 96);
            }
            else
            {
                // A = 65 -> 27
                // Z = 90 -> 52
                result += (intersect - 38);
            }
            
            Console.WriteLine(intersect);
        }
        
        return result;
    }
    
    public int Challenge2(string[] input)
    {
        var result = 0;

        var counter = 0;
        List<string> combos = new List<string>();

        for (var i = 0; i < input.Length; i += 3)
        {
            var intersect= input[i].ToCharArray().Intersect(input[i+1].ToCharArray()).Intersect(input[i+2].ToCharArray()).Single();
            Console.WriteLine(intersect);
            if (intersect >= 97 && intersect <= 122)
            {
                // a = 97 -> 1 
                // z = 122 -> 26
                result += (intersect - 96);
            }
            else
            {
                // A = 65 -> 27
                // Z = 90 -> 52
                result += (intersect - 38);
            }
        }

        return result;
    }
}