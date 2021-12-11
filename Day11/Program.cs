var input = File.ReadAllLines("input.txt")
    .ToArray();

var octos = (from y in Enumerable.Range(0, 10)
             from x in Enumerable.Range(0, 10)
             select new KeyValuePair<Pos, int>(new Pos(x, y), int.Parse(input[y][x].ToString())))
            .ToDictionary(x => x.Key, x => x.Value);

Console.WriteLine($"Part1: {Solve(octos, 100)}");

int Solve(Dictionary<Pos, int> data, int steps)
{
    var result = 0;

    while (steps-- > 0)
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

            var adjacent = GetAdjacent(pos).Where(x => data.ContainsKey(x));
            foreach (var a in adjacent)
            {
                data[a]++;
                if (data[a] == 10)
                {
                    queue.Enqueue(a);
                }
            }
        }

        // reset flashers
        foreach (var pos in flashed)
        {
            data[pos] = 0;
        }

        result += flashed.Count();
    }

    return result;
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