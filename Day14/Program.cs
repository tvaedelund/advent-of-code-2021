using System.Diagnostics;

var sw = Stopwatch.StartNew();
var input = File.ReadAllLines("_input.txt");

Console.WriteLine($"Part 1: {Solve(input, 10)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

// sw.Restart();
// Console.WriteLine();
// Console.WriteLine($"Part 2: {Solve(input, 100)}");
// Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve(string[] input, int v)
{
    var polymer = Enumerable.Zip(input[0], input[0].Skip(1))
        .Select(p => $"{p.First}{p.Second}")
        .GroupBy(e => e)
        .Select(grp => new
        {
            chr = grp.Key,
            cnt = grp.Count()
        })
        .ToDictionary(grp => grp.chr, grp => grp.cnt);

    var elements = input[0].Select(chr => chr)
        .GroupBy(e => e)
        .Select(grp => new
        {
            chr = grp.Key,
            cnt = grp.Count()
        })
        .ToDictionary(grp => grp.chr, grp => grp.cnt);

    var formula = input[2..]
        .Select(x => x.Split(" -> "))
        .ToDictionary(x => x[0], x=> x[1][0]);

    for (int step = 1; step <= v; step++)
    {
        var found = new Dictionary<string, int>();
        foreach (var ab in polymer.Keys)
        {
            (char a, char n, char b, int count) = (ab[0], formula[ab], ab[1], found.GetValueOrDefault(ab));

            found[$"{a}{n}"] = polymer.GetValueOrDefault($"{a}{n}") + count;
            found[$"{n}{b}"] = polymer.GetValueOrDefault($"{n}{b}") + count;

            elements[n] = elements.GetValueOrDefault(n) + count;
        }
        polymer = found;
    }

    return elements.Values.Max() - elements.Values.Min();
}
