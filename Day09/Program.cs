using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part1: {Solve1(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

sw.Restart();
Console.WriteLine($"Part2: {Solve2(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve1(string[] data)
{
    var result = 0;
    var map = GetMap(data);

    foreach (var pos in map)
    {
        var adjacent = GetAdjacent(pos.Key).Where(key => map.ContainsKey(key));

        result += adjacent.All(key => map[key] > pos.Value) ? pos.Value + 1 : 0;
    }

    return result;
}


int Solve2(string[] data)
{
    var result = new List<int>();
    var map = GetMap(data);

    foreach (var pos in map)
    {
        var adjacent = GetAdjacent(pos.Key).Where(key => map.ContainsKey(key));
        var queue = new Queue<Pos>();
        var count = 0;
        var visited = new HashSet<Pos>();

        if (adjacent.All(key => map[key] > pos.Value))
        {
            queue.Enqueue(pos.Key);

            while (queue.Any())
            {
                var current = queue.Dequeue();
                count++;

                adjacent = GetAdjacent(current).Where(key => map.ContainsKey(key) && map[key] < 9 && map[key] > map[current] && !visited.Contains(key));
                foreach (var item in adjacent)
                {
                    queue.Enqueue(item);
                    visited.Add(item);
                }
            }
        }

        if (count > 0)
        {
            result.Add(count);
        }
    }

    return result.Where(x => x > 0).OrderByDescending(x => x).Take(3).Aggregate(1, (a, v) => a * v);
}

Dictionary<Pos, int> GetMap(string[] data)
{
    return (from y in Enumerable.Range(0, data.Length)
            from x in Enumerable.Range(0, data[0].Length)
            select new KeyValuePair<Pos, int>(new Pos(x, y), int.Parse(data[y][x].ToString())))
               .ToDictionary(x => x.Key, x => x.Value);
}

IEnumerable<Pos> GetAdjacent(Pos pos)
{
    return new[]
    {
        new Pos(pos.x, pos.y - 1),
        new Pos(pos.x + 1, pos.y),
        new Pos(pos.x, pos.y + 1),
        new Pos(pos.x - 1, pos.y),
    };
}

record Pos(int x, int y);