using System.Diagnostics;
using System.Text.RegularExpressions;

var sw = Stopwatch.StartNew();
var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part 1: {Solve(input, 1)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

sw.Restart();
Console.WriteLine();
Console.WriteLine($"Part 2: {Solve(input, 100)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve(string[] input, int v)
{
    var dots = GetDots(input);
    var instructions = GetInstructions(input).Take(v);

    foreach (var instr in instructions)
    {
        if (instr.dir == 'y')
        {
            dots = FoldY(dots, instr.line);
        }
        else
        {
            dots = FoldX(dots, instr.line);
        }
    }

    if (v > 1)
    {
        PrintCode(dots);
    }

    return dots.Count();
}

IEnumerable<Pos> FoldY(IEnumerable<Pos> dots, int line)
{
    var folded = new List<Pos>();

    foreach (var dot in dots.Where(d => d.y > line))
    {
        folded.Add(dot with { y = Math.Abs(dot.y - line * 2) });
    }

    foreach (var dot in dots.Where(d => d.y < line))
    {
        if (!folded.Any(d => d.x == dot.x && d.y == dot.y))
        {
            folded.Add(dot);
        }
    }

    return folded;
}

IEnumerable<Pos> FoldX(IEnumerable<Pos> dots, int line)
{
    var folded = new List<Pos>();

    foreach (var dot in dots.Where(d => d.x > line))
    {
        folded.Add(dot with { x = Math.Abs(dot.x - line * 2) });
    }

    foreach (var dot in dots.Where(d => d.x < line))
    {
        if (!folded.Any(d => d.x == dot.x && d.y == dot.y))
        {
            folded.Add(dot);
        }
    }

    return folded;
}

IEnumerable<Pos> GetDots(string[] data)
{
    var regex = new Regex(@"(?<x>\d+),(?<y>\d+)");

    foreach (var line in data)
    {
        var match = regex.Match(line);
        if (match.Success)
        {
            yield return new Pos(int.Parse(match.Groups["x"].Value), int.Parse(match.Groups["y"].Value));
        }
    }
}

IEnumerable<Instruction> GetInstructions(string[] data)
{
    var regex = new Regex(@"(?<dir>\w)=(?<line>\d+)");

    foreach (var line in data)
    {
        var match = regex.Match(line);
        if (match.Success)
        {
            yield return new Instruction(match.Groups["dir"].Value[0], int.Parse(match.Groups["line"].Value));
        }
    }
}

void PrintCode(IEnumerable<Pos> dots)
{
    for (int y = 0; y < dots.Max(d => d.y) + 1; y++)
    {
        for (int x = 0; x < dots.Max(d => d.x) + 1; x++)
        {
            Console.Write(dots.Any(d => d.x == x && d.y == y) ? "#" : ".");
        }
        Console.WriteLine();
    }
}

record Pos(int x, int y);
record Instruction(char dir, int line);
