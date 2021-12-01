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
    var result = 0;

    for (int i = 0; i < data.Length - 1; i++)
    {
        result += (data[i + 1] > data[i]) ? 1 : 0;
    }

    return result;
}

int Solve2(int[] data)
{
    var result = 0;

    for (int i = 2; i < data.Length - 1; i++)
    {
        result += (data[i - 1] + data[i] + data[i + 1] > data[i - 2] + data[i - 1] + data[i]) ? 1 : 0;
    }

    return result;
}