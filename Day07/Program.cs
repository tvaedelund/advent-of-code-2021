using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllText("input.txt")
    .Split(',')
    .Select(int.Parse)
    .ToArray();

Console.WriteLine($"Part1: {Solve1(input)}");
Console.WriteLine($"Part2: {Solve2(input)}");
Console.WriteLine($"TID DET TOG: {sw.ElapsedMilliseconds}ms");

int Solve1(int[] data)
{
    return Enumerable.Range(data.Min(), data.Max())
        .AsParallel()
        .Min(step => data.Sum(pos => Math.Abs(pos - step)));
}

long Solve2(int[] data)
{
    return Enumerable.Range(data.Min(), data.Max())
        .AsParallel()
        .Min(step => data.Sum(pos => GetFuel(Math.Abs(pos - step))));
}

long GetFuel(int fuel)
{
    return (fuel * (fuel + 1L )) / 2;
}
