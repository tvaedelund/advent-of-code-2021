using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllText("input.txt")
    .Split(',')
    .Select(int.Parse);

var result = Solve(input);

Console.WriteLine($"Part1: {result.p1}");
Console.WriteLine($"Part2: {result.p2}");
Console.WriteLine($"TID DET TOG: {sw.ElapsedMilliseconds}ms");

(long p1, long p2) Solve(IEnumerable<int> data)
{
    var max = data.Max();
    var min = data.Min();

    var p1 = new List<(int step, long fuel)>();
    var p2 = new List<(int step, long fuel)>();

    for (int i = min; i < max; i++)
    {
        p1.Add((i, data.Sum(x => x >= i ? x - i : i - x)));
        p2.Add((i, data.Sum(x => GetFuel(x >= i ? x - i : i - x))));
    }

    return (p1.OrderBy(x => x.fuel).First().fuel, p2.OrderBy(x => x.fuel).First().fuel);
}

long GetFuel(int fuel)
{
    return (fuel * (fuel + 1L )) / 2;
}
