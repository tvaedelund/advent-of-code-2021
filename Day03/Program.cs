var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part 1: {Solve(input)}");

int Solve(string[] input)
{
    var minmax = Enumerable.Range(0, input[0].Length)
        .Select(i => input.Select(x => x[i]))
        .Select(x => x.GroupBy(y => y))
        .Select(g => g.Select(x => new
        {
            key = int.Parse(x.Key.ToString()),
            cnt = x.Count()
        })
            .OrderBy(x => x.cnt)
            .ToArray()
        )
        .Select(x => new MinMax(x[0].key, x[1].key));

    var min = Convert.ToInt32(string.Join("", minmax.Select(x => x.Min)), 2);
    var max = Convert.ToInt32(string.Join("", minmax.Select(x => x.Max)), 2);

    return min * max;
}

record MinMax(int Min, int Max);