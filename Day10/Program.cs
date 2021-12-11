using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part 1: {Solve(input, true)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

sw.Restart();
Console.WriteLine($"Part 2: {Solve(input, false)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

long Solve(string[] input, bool getSyntaxErrorScore)
{
    var result = new List<long>();

    foreach (var line in input)
    {
        result.Add(GetScore(line, getSyntaxErrorScore));
    }

    if (getSyntaxErrorScore)
    {
        return result.Sum();
    }

    var part2 = result.Where(x => x > 0);
    return part2.OrderBy(x => x).ElementAt(part2.Count() / 2);
}

long GetScore(string line, bool getSyntaxErrorScore)
{
    var chunks = new[]
    {
        ('(', ')', 3, 1),
        ('[', ']', 57, 2),
        ('{', '}', 1197, 3),
        ('<', '>', 25137, 4)
    };

    var stack = new Stack<char>();

    foreach (var ch in line)
    {
        // opening
        if (chunks.Select(c => c.Item1).Any(c => c == ch))
        {
            stack.Push(ch);
        }
        // closing
        else
        {
            // if matching opening exists remove it from stack
            var chunk = chunks.Single(c => c.Item2 == ch);

            if (stack.Peek() == chunk.Item1)
            {
                stack.Pop();
            }
            // else it's corrupt
            else
            {
                // part 1
                return getSyntaxErrorScore ? chunk.Item3 : 0;
            }
        }
    }

    // part 2
    return getSyntaxErrorScore ? 0 : stack.Select(x => chunks
                    .Single(c => c.Item1 == x).Item4)
                .Aggregate(0L, (a, v) => a * 5 + v);
}
