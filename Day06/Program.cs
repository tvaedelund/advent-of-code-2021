using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllText("input.txt")
    .Split(',')
    .Select(int.Parse)
    .GroupBy(x => x)
    .Select(g => new Generation
    {
        age = g.Key,
        cnt = g.Count()
    })
    .ToList();

Console.WriteLine($"Part1: {Solve(input, 80)}");
Console.WriteLine($"Part2: {Solve(input, 256)}");
Console.WriteLine($"TID DET TOG: {sw.ElapsedMilliseconds}ms");

long Solve(List<Generation> data, int days)
{
    for (int day = 1; day <= days; day++)
    {
        data = data.Select(x => new Generation
        {
            age = x.age - 1,
            cnt = x.cnt
        })
        .ToList();

        var babies = data.Where(x => x.age == -1);
        data.Add(new Generation { age = 8, cnt = babies.Sum(x => x.cnt) });
        data.Add(new Generation { age = 6, cnt = babies.Sum(x => x.cnt) });
        data.RemoveAll(x => x.age == -1);
    }

    return data.Sum(x => x.cnt);
}

record Generation
{
    public int age { get; set; }
    public long cnt { get; set; }
};