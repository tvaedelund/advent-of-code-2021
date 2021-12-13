using System.Diagnostics;
using System.Text.RegularExpressions;

var sw = Stopwatch.StartNew();
var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part 1: {Solve(input, 1)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

// sw.Restart();
// Console.WriteLine();
// Console.WriteLine($"Part 2: {Solve(input)}");
// Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve(string[] input, int v)
{
    var dots = GetDots(input);
    var instructions = GetInstructions(input).Take(v);

    foreach (var instr in instructions)
    {
        dots = instr.dir == 'y' ? FoldY(dots, instr.line) : FoldX(dots, instr.line);
    }

    return dots.Count();
}

IEnumerable<Pos> FoldY(IEnumerable<Pos> dots, int v)
{
    var folded = new List<Pos>();
    var maxY = dots.Max(d => d.y);

    foreach (var dot in dots.Where(d => d.y > v))
    {
        folded.Add(dot with { y = Math.Abs(dot.y - maxY) });
    }

    foreach (var dot in dots.Where(d => d.y < v))
    {
        if (!folded.Any(d => d.x == dot.x && d.y == dot.y))
        {
            folded.Add(dot);
        }
    }

    return folded.OrderBy(d => d.y).ThenBy(d => d.x);
}

IEnumerable<Pos> FoldX(IEnumerable<Pos> dots, int v)
{
    var folded = new List<Pos>();
    var maxX = dots.Max(d => d.x);

    foreach (var dot in dots.Where(d => d.x > v))
    {
        folded.Add(dot with { x = Math.Abs(dot.x - maxX) });
    }

    foreach (var dot in dots.Where(d => d.x < v))
    {
        if (!folded.Any(d => d.x == dot.x && d.y == dot.y))
        {
            folded.Add(dot);
        }
    }

    return folded.Distinct();
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

record Pos(int x, int y);
record Instruction(char dir, int line);
