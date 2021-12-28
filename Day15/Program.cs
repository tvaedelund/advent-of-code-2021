using System.Diagnostics;

var sw = Stopwatch.StartNew();
var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part 1: {Solve(GetMap(input, 1))}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

sw.Restart();
Console.WriteLine();
Console.WriteLine($"Part 2: {Solve(GetMap(input, 5))}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

// https://www.youtube.com/watch?v=CerlT7tTZfY
// https://www.youtube.com/watch?v=6fIQT_y5GgA&t=1209s
int Solve(Dictionary<Pos, int> map)
{
    var startPos = new Pos(0, 0);
    var endPos = new Pos(map.Keys.MaxBy(p => p.x)!.x, map.Keys.MaxBy(p => p.y)!.y);

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

Dictionary<Pos, int> GetMap(string[] data, int times)
{
    var ySize = data.Length;
    var xSize = data[0].Length;

    var map = from y in Enumerable.Range(0, ySize * times)
              from x in Enumerable.Range(0, xSize * times)

              let distance = (y / ySize) + (x / xSize)
              let riskLevel = int.Parse(data[y % ySize][x % xSize].ToString())
              let newRiskLevel = (riskLevel + distance - 1) % 9 + 1

              select new KeyValuePair<Pos, int>(new Pos(x, y), newRiskLevel);

    return map.ToDictionary(x => x.Key, x => x.Value);
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