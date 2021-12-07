var input = File.ReadAllText("input.txt")
    .Split(',')
    .Select(int.Parse);

Console.WriteLine($"Part1: {Solve1(input)}");
Console.WriteLine($"Part2: {Solve2(input)}");

int Solve1(IEnumerable<int> data)
{
    var max = data.Max();
    var min = data.Min();

    var steps = new List<(int step, int fuel)>();

    for (int i = min; i < max; i++)
    {
        steps.Add((i, data.Sum(x => x >= i ? x - i : i - x)));
    }

    return steps.OrderBy(x => x.fuel).First().fuel;
}

long Solve2(IEnumerable<int> data)
{
    var max = data.Max();
    var min = data.Min();

    var steps = new List<(int step, long fuel)>();

    for (int i = min; i < max; i++)
    {
        steps.Add((i, data.Sum(x => GetFuel(x >= i ? x - i : i - x))));
    }

    return steps.OrderBy(x => x.fuel).First().fuel;
}

long GetFuel(int fuel)
{
    var result = 0l;

    for (int i = 1; i <= fuel; i++)
    {
        result += i;
    }

    return result;
}
