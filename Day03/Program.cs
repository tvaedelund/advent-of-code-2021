var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part 1: {Solve1(input)}");
Console.WriteLine($"Part 2: {Solve2(input)}");

int Solve1(string[] input)
{
    var minMax = GetMinMax(input);

    var min = Convert.ToInt32(string.Join("", minMax.Select(x => x.Min)), 2);
    var max = Convert.ToInt32(string.Join("", minMax.Select(x => x.Max)), 2);

    return min * max;
}

int Solve2(string[] input)
{
    var oxygen = GetValue(input, 1);
    var co2 = GetValue(input, 0);

    return oxygen * co2;
}

int GetValue(string[] input, int equalVal)
{
    var filtered = input;

    for (int i = 0; i < input[0].Length; i++)
    {
        if (filtered.Length > 1)
        {
            var minMax = GetMinMax(filtered, equalVal);
            filtered = equalVal == 1
                ? filtered.Where(x => int.Parse(x[i].ToString()) == minMax.ElementAt(i).Max).ToArray()
                : filtered.Where(x => int.Parse(x[i].ToString()) == minMax.ElementAt(i).Min).ToArray();
        }
    }

    return Convert.ToInt32(string.Join("", filtered), 2);
}

IEnumerable<MinMax> GetMinMax(string[] input, int equalVal = 0)
{
    var minMax = Enumerable.Range(0, input[0].Length)
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
    .Select(x =>
    {
        if (x[0].cnt == x[1].cnt)
        {
            return new MinMax(equalVal, equalVal);
        }
        return new MinMax(x[0].key, x[1].key);
    });

    return minMax;
}

record MinMax(int Min, int Max);
