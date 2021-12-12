using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Solve(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

// sw.Restart();
// Console.WriteLine();
// Console.WriteLine($"Part 2: {Solve(input)}");
// Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve(string[] input)
{
    return 0;
}
