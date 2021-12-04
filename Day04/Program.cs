
var input = File.ReadAllLines("input.txt");

// Console.WriteLine($"Part1: {Solve1(input)}");
Console.WriteLine($"Part2: {Solve2(input)}");

int Solve1(string[] data)
{
    var boards = GetBoards(data);
    var numbers = data.First().Split(',');

    var result = 0;

    foreach (var num in numbers)
    {
        boards = PlayMumber(boards, num);

        var hasWinner = HasWinner(boards, new());
        if (hasWinner.win)
        {
            result = int.Parse(num) * GetBoardValue(boards, hasWinner.bNum.First());
            break;
        }
    }

    return result;
}

int Solve2(string[] data)
{
    var boards = GetBoards(data);
    var numbers = data.First().Split(',');

    var rounds = new List<(int winningNum, int boardValue, int boardNum)>();
    var skip = new List<int>();

    foreach (var num in numbers)
    {
        boards = PlayMumber(boards, num);

        var winner = HasWinner(boards, skip);
        if (winner.win)
        {
            foreach (var win in winner.bNum)
            {
                skip.Add(win);
                rounds.Add((int.Parse(num), GetBoardValue(boards, win), win));
            }
        }
    }

    var lastWinner = rounds.Last();

    return lastWinner.winningNum * lastWinner.boardValue;
}

List<string[][]> GetBoards(IEnumerable<string> data)
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
        .ToList();
}

List<string[][]> PlayMumber(List<string[][]> boards, string num)
{
    for (int b = 0; b < boards.Count; b++)
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

(bool win, List<int> bNum) HasWinner(List<string[][]> boards, List<int> skip)
{
    var winners = new List<int>();

    for (int b = 0; b < boards.Count; b++)
    {
        if (skip.Any(s => s == b))
        {
            continue;
        }

        for (int r = 0; r < boards[b].Length; r++)
        {
            if (boards[b][r].All(x => x == string.Empty))
            {
                // Horizontal winner
                winners.Add(b);
            }

            // This works since the board is perfect square (5x5)...
            var col = Enumerable.Range(0, boards[b].Length)
                .Select(c => boards[b][c][r]);

            if (col.All(x => x == string.Empty))
            {
                // Vertical winner
                winners.Add(b);
            }
        }
    }

    if (winners.Any())
    {
        return (true, winners);
    }

    return (false, new());
}

int GetBoardValue(List<string[][]> boards, int bNum)
{
    return Enumerable
        .Range(0, boards[bNum].Length)
        .Select(r => boards[bNum][r]
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Sum(x => int.Parse(x))
        )
        .Sum();
}
