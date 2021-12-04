var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part1: {Solve(input)}");

int Solve(string[] data)
{
    var boards = GetBoards(data);
    var numbers = data.First().Split(',');

    var result = 0;

    foreach (var num in numbers)
    {
        boards = PlayMumber(boards, num);

        var hasWinner = HasWinner(boards);
        if (hasWinner.win)
        {
            result = int.Parse(num) * GetBoardValue(boards, hasWinner.bNum);
            break;
        }
    }

    return result;
}

int GetBoardValue(string[][][] boards, int bNum)
{
    var result = 0;

    for (int r = 0; r < boards[bNum].Length; r++)
    {
        result += boards[bNum][r].Where(x => !string.IsNullOrWhiteSpace(x)).Sum(x => int.Parse(x));
    }

    return result;
}

string[][][] GetBoards(IEnumerable<string> data)
{
    return data
        .Skip(1)
        .Select((s, i) => new { Value = s, Index = i })
        .GroupBy(x => x.Index / 6)
        .Select(grp => grp
            .Skip(1)
            .Select(x => x.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .ToArray()
        )
        .ToArray();
}

string[][][] PlayMumber(string[][][] boards, string num)
{
    for (int b = 0; b < boards.Length; b++)
    {
        for (int r = 0; r < boards[b].Length; r++)
        {
            for (int c = 0; c < boards[b][r].Length; c++)
            {
                if (boards[b][r][c] == num)
                {
                    boards[b][r][c] = string.Empty;
                }
            }
        }
    }

    return boards;
}

(bool win, int bNum) HasWinner(string[][][] boards)
{
    for (int b = 0; b < boards.Length; b++)
    {
        for (int r = 0; r < boards[b].Length; r++)
        {
            if (boards[b][r].All(x => x == string.Empty))
            {
                // Horizontal winner
                return (true, b);
            }

            // This works since the board is perfect square (5x5)...
            var col = Enumerable.Range(0, boards[b].Length)
                .Select(c => boards[b][c][r]);

            if (col.All(x => x == string.Empty))
            {
                // Vertical winner
                return (true, b);
            }
        }
    }

    return (false, 0);
}
