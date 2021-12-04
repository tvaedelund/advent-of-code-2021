using System.Diagnostics;

var input = File.ReadAllLines("input.txt");

var sw = Stopwatch.StartNew();
var result = Solve(input);
Console.WriteLine($"Part1: {result.p1}");
Console.WriteLine($"Part2: {result.p2}");
Console.WriteLine($"TID DET TOG: {sw.ElapsedMilliseconds}ms");

(int p1, int p2) Solve(string[] data)
{
    var boards = GetBoards(data);
    var numbers = data.First().Split(',');

    var wins = new List<(int winningNum, int boardValue, int boardNum)>();

    foreach (var num in numbers)
    {
        boards = PlayMumber(boards, num);

        var player = HasWinner(boards, wins.Select(w => w.boardNum).ToList());
        if (player.win)
        {
            foreach (var win in player.bNum)
            {
                wins.Add((int.Parse(num), GetBoardValue(boards, win), win));
            }
        }
    }

    var firstWinner = wins.First();
    var lastWinner = wins.Last();

    return (firstWinner.winningNum * firstWinner.boardValue, lastWinner.winningNum * lastWinner.boardValue);
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
            // Already won on this board so skip
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
