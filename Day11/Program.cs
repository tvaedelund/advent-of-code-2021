using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part1: {Solve(GetOctos(input), 100)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

sw.Restart();
Console.WriteLine($"Part2: {Solve(GetOctos(input), int.MaxValue)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve(Dictionary<Pos, int> data, int steps)
{
    var result = 0;
    var step = 0;

    while (step++ < steps)
    {
        var flashed = new List<Pos>();
        var queue = new Queue<Pos>();

        // increase all and add flashers to queue
        foreach (var key in data.Keys)
        {
            data[key]++;
            if (data[key] == 10)
            {
                queue.Enqueue(key);
            }
        }

        // while theres's any flashers
        while (queue.Any())
        {
            var pos = queue.Dequeue();
            flashed.Add(pos);

            // gets adjacent only within range
            var adjacent = GetAdjacent(pos).Where(x => data.ContainsKey(x));
            foreach (var key in adjacent)
            {
                data[key]++;
                if (data[key] == 10)
                {
                    queue.Enqueue(key);
                }
            }
        }

        // reset flashers
        foreach (var pos in flashed)
        {
            data[pos] = 0;
        }

        // part 2
        if (data.Sum(o => o.Value) == 0)
        {
            return step;
        }

        // part 1
        result += flashed.Count();
    }

    return result;
}

Dictionary<Pos, int> GetOctos(string[] data)
{
    return (from y in Enumerable.Range(0, 10)
            from x in Enumerable.Range(0, 10)
            select new KeyValuePair<Pos, int>(new Pos(x, y), int.Parse(data[y][x].ToString())))
            .ToDictionary(x => x.Key, x => x.Value);
}

IEnumerable<Pos> GetAdjacent(Pos pos)
{
    var adjacent = new[]
    {
        new Pos(pos.x, pos.y - 1),
        new Pos(pos.x + 1, pos.y - 1),
        new Pos(pos.x + 1, pos.y),
        new Pos(pos.x + 1, pos.y + 1),
        new Pos(pos.x, pos.y + 1),
        new Pos(pos.x - 1, pos.y + 1),
        new Pos(pos.x - 1, pos.y),
        new Pos(pos.x - 1, pos.y - 1),
    };

    return adjacent;
}

record Pos(int x, int y);