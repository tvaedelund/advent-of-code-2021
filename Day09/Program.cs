using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllLines("input.txt")
    .Select(l => l.ToCharArray().Select(c => (int)c - 48).ToArray())
    .ToArray();

var visited = new List<(int y, int x)>();

Console.WriteLine($"Part1: {Solve1(input)}");
Console.WriteLine($"Part2: {Solve2(input)}");
Console.WriteLine($"TID DET TIG: {sw.ElapsedMilliseconds}ms");

int Solve1(int[][] data)
{
    var lowPoints = new List<int>();
    for (int y = 0; y < data.Length; y++)
    for (int x = 0; x < data[y].Length; x++)
    {
        if (IsLowPoint(y, x, data))
        {
            lowPoints.Add(int.Parse(data[y][x].ToString()));
        }
    }

    return lowPoints.Select(x => x + 1).Sum();
}

bool IsLowPoint(int y, int x, int[][] data)
{
    var adjacent = new[] {
        y > 0 ? data[y - 1][x] : 9,
        x > 0 ? data[y][x - 1] : 9,
        x < data[y].Length - 1 ? data[y][x + 1] : 9,
        y < data.Length - 1 ? data[y + 1][x] : 9,
    };

    return adjacent.All(c => c > data[y][x]);
}

int Solve2(int[][] data)
{
    var result = new List<int>();
    for (int y = 0; y < data.Length; y++)
    for (int x = 0; x < data[y].Length; x++)
    {
        if (IsLowPoint(y, x, data))
        {
            result.Add(GetBasin(y, x, data, 1));
        }
    }
    return result.OrderByDescending(x => x).Take(3).Aggregate((r, c) => r * c);
}

int GetBasin(int y, int x, int[][] data, int cnt)
{
    if (visited.Any(p => p.y == y && p.x == x))
    {
        return 0;
    }

    visited.Add((y, x));

    var xs = x > 0 ? data[y][x - 1] : 9;
    if (xs < 9)
    {
        cnt += GetBasin(y, x - 1, data, 1);
    }

    var xm = x < data[y].Length - 1? data[y][x + 1] : 9;
    if (xm < 9)
    {
        cnt += GetBasin(y, x + 1, data, 1);
    }

    var ys = y > 0 ? data[y - 1][x] : 9;
    if (ys < 9)
    {
        cnt += GetBasin(y - 1, x, data, 1);
    }

    var ym = y < data.Length - 1 ? data[y + 1][x] : 9;
    if (ym < 9)
    {
        cnt += GetBasin(y + 1, x, data, 1);
    }

    return cnt;
}