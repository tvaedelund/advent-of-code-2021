using System.Diagnostics;

var sw = Stopwatch.StartNew();
var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part 1: {Solve(input, 10)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

// sw.Restart();
// Console.WriteLine();
// Console.WriteLine($"Part 2: {Solve(input, 100)}");
// Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve(string[] input, int v)
{
    var template = input[0];
    var formula = input[2..]
        .Select(x => x.Split(" -> "));

    for (int step = 1; step <= v; step++)
    {
        template = GetResult(formula, template);
    }

    var elements = template
        .GroupBy(c => c)
        .Select(grp => new
        {
            chr = grp.Key,
            cnt = grp.Count()
        })
        .OrderBy(grp => grp.cnt);

    return elements.Last().cnt - elements.First().cnt;
}

string GetResult(IEnumerable<string[]> formula, string template)
{
    var result = from t in Enumerable.Zip(template, template.Skip(1))
                 join f in formula
                 on new { a = t.First, b = t.Second }
                 equals new { a = f[0][0], b = f[0][1] }
                 into gj
                 from subf in gj.DefaultIfEmpty()
                 select new { a = t.First, b = subf?[1] ?? string.Empty, c = t.Second };

    var test = result.Aggregate(string.Empty, (a, c) => $"{a}{c.a}{c.b}{(!string.IsNullOrEmpty(c.b) ? string.Empty : c.c)}");

    return $"{test}{template.Last()}";
}