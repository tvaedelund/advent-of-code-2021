using System.Diagnostics;

var sw = Stopwatch.StartNew();
var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part 1: {Solve(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

// sw.Restart();
// Console.WriteLine();
// Console.WriteLine($"Part 2: {Solve(input, 100)}");
// Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

// https://www.youtube.com/watch?v=CerlT7tTZfY
// https://www.youtube.com/watch?v=6fIQT_y5GgA&t=1209s
int Solve(string[] data)
{
    var map = GetMap(data);

    var startPos = new Pos(0, 0);
    var endPos = new Pos(data[0].Length - 1, data.Length - 1);

    var accumulatedRiskMap = new Dictionary<Pos, int>();
    accumulatedRiskMap[startPos] = 0;

    var pq = new PriorityQueue<Pos, int>();
    pq.Enqueue(startPos, 0);

    while (true)
    {
        var current = pq.Dequeue();

        if (current == endPos)
        {
            break;
        }

        var adjecent = GetAdjacent(current).Where(a => map.ContainsKey(a));

        foreach (var a in adjecent)
        {
            var accumulatedRisk = accumulatedRiskMap[current] + map[a];
            if (accumulatedRisk < accumulatedRiskMap.GetValueOrDefault(a, int.MaxValue))
            {
                accumulatedRiskMap[a] = accumulatedRisk;
                pq.Enqueue(a, accumulatedRisk);
            }
        }
    }

    return accumulatedRiskMap[endPos];
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
        new Pos(pos.x, pos.y + 1),
        new Pos(pos.x + 1, pos.y),
        new Pos(pos.x, pos.y - 1),
        new Pos(pos.x - 1, pos.y),
    };
}

record Pos(int x, int y);