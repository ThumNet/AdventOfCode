namespace AdventOfCode2021;

public class Day4
{
    public int Challenge1(string[] input)
    {
        int result = 0;

        var bingoNumbers = input[0].Split(",").Select(n => int.Parse(n));

        var boards = CreateBoards(input[2..]).ToArray();

        foreach (var nr in bingoNumbers)
        {
            foreach (var b in boards)
            {
                if (b.CheckNr(nr))
                {
                    result = CalculateResult(b, nr);
                    return result;
                }
            }
        }
        
        return result;
    }

    private int CalculateResult(Board board, int nr)
    {
        return board.Items.Cast<Item>().Where(x => x.Checked == false).Sum(x => x.Number) * nr;
    }

    private IEnumerable<Board> CreateBoards(string[] input)
    {
        var board = new Board();
        var r = 0;
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                yield return board;
                board = new Board();
                r = 0;
                continue;
            }
            var lineParts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            for (var c = 0; c < 5; c++)
            {
                board.Items[r, c] = new Item { Number = lineParts[c] };
            }

            r++;
        }
        yield return board;
    }

    public int Challenge2(string[] input)
    {
        int result = 0;

        var bingoNumbers = input[0].Split(",").Select(n => int.Parse(n));

        var boards = CreateBoards(input[2..]).ToList();

        foreach (var nr in bingoNumbers)
        {
            var bc = boards.Count -1;
            for (var i = bc; i >= 0; i--)
            {
                if (boards[i].CheckNr(nr))
                {
                    if (boards.Count == 1)
                    {
                        return CalculateResult(boards[i], nr);
                    }
                    else
                    {
                        boards.RemoveAt(i);
                    }
                }
            }
        }

        return result;
    }


    public class Board
    {
        public Item[,] Items { get; set; } = new Item[5, 5];

        public bool CheckNr(int number)
        {
            if (Items.Contains(x => x.Number == number))
            {
                var (x, y) = Items.LocationOf(i => i.Number == number);
                Items[x, y].Checked = true;
                return BingoInRow(x) || BingoInColumn(y);
            }

            return false;
        }

        private bool BingoInColumn(int i)
        {
            return Items.ItemsInColumn(i).All(x => x.Checked);
        }

        private bool BingoInRow(int i)
        {
            return Items.ItemsInRow(i).All(x => x.Checked);
        }
    }

    public class Item
    {
        public int Number { get; set; }
        public bool Checked { get; set; }
    }
}