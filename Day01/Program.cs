using System.Diagnostics;

Console.WriteLine("AoC 2021 Day01");

var input = File.ReadAllLines("input.txt")
    .Select(int.Parse)
    .ToArray();

var sw = Stopwatch.StartNew();

Console.WriteLine("Part 1");
Console.WriteLine($"Result: {Solve1(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

sw.Restart();
Console.WriteLine("Part 2");
Console.WriteLine($"Result: {Solve2(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve1(int[] data)
{
    return data.Aggregate((a, b) => b > a ? 1 : 0 );
}

int Solve2(int[] data)
{
    return data.Skip(2).Select((_, i) => (data[i - 1] + data[i] + data[i + 1] > data[i - 2] + data[i - 1] + data[i]) ? 1 : 0).Sum();
}