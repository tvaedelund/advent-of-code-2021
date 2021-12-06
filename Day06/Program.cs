var input = File.ReadAllText("input.txt")
    .Split(',')
    .Select(int.Parse)
    .ToList();

Console.WriteLine($"Part1: {Solve(input)}");

int Solve(List<int> data)
{
    for (int day = 1; day <= 80; day++)
    {
        var newBorns = new List<int>();
        data = data.Select(f =>
        {
            if (--f < 0)
            {
                newBorns.Add(8);
                return 6;
            }

            return f;
        })
        .ToList();

        data.AddRange(newBorns);
    }

    return data.Count();
}