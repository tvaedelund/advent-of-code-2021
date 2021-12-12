using System.Diagnostics;

Console.WriteLine("AoC 2021 Day01");

var input = File.ReadAllLines("input.txt")
    .Select(int.Parse);

var sw = Stopwatch.StartNew();

Console.WriteLine($"Part 1: {DepthIncreases(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

sw.Restart();
Console.WriteLine($"Part 2: {DepthIncreases(SlidingWindows(input))}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int DepthIncreases(IEnumerable<int> measurements) => (
        from m in Enumerable.Zip(measurements, measurements.Skip(1))
        where m.Second > m.First
        select 1
    ).Sum();

IEnumerable<int> SlidingWindows(IEnumerable<int> measurements) => (
        from m in Enumerable.Zip(measurements, measurements.Skip(1), measurements.Skip(2))
        select m.First + m.Second + m.Third
    );
