using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllLines("input.txt");

var chunks = new[]
{
    ('(', ')', 3, 1),
    ('[', ']', 57, 2),
    ('{', '}', 1197, 3),
    ('<', '>', 25137, 4)
};

var result = Solve(input);
Console.WriteLine($"Part 1: {result.p1}");
Console.WriteLine($"Part 2: {result.p2}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

(int p1, long p2) Solve(string[] input)
{
    var p1 = new List<int>();
    var p2 = new List<long>();

    foreach (var line in input)
    {
        var stack = new Stack<char>();
        var isCorrupt = false;

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
                    p1.Add(chunk.Item3);
                    isCorrupt = true;
                    break;
                }
            }
        }

        if (!isCorrupt)
        {
            // part 2
            p2.Add(stack.Select(x => chunks
                    .Single(c => c.Item1 == x).Item4)
                .Aggregate(0L, (a, v) => a * 5 + v));
        }
    }

    return (p1.Sum(), p2.OrderBy(x => x).ElementAt(p2.Count / 2));
}
