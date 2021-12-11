using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllLines("input.txt");

var chunks = new[]
{
    ('(', ')', 3),
    ('[', ']', 57),
    ('{', '}', 1197),
    ('<', '>', 25137)
};

Console.WriteLine($"Part 1: {Solve(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve(string[] input)
{
    var result = new List<int>();

    foreach (var line in input)
    {
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
                // if matching opening exists
                var chunk = chunks.Single(c => c.Item2 == ch);
                if (stack.Peek() == chunk.Item1)
                {
                    stack.Pop();
                }
                else
                {
                    result.Add(chunk.Item3);
                    break;
                }
            }
        }
    }

    return result.Sum();
}


