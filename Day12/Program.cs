using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Solve(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

// sw.Restart();
// Console.WriteLine();
// Console.WriteLine($"Part 2: {Solve(input)}");
// Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve(string[] input)
{
    var caves = GetConnections(input);

    var result = TraverseCaves(caves, "start", new()); ;

    return result;
}

int TraverseCaves(Dictionary<string, string[]> caves, string current, List<string> visitedCaves)
{
    visitedCaves.Add(current);

    if (current == "end")
    {
        return 1;
    }

    var count = 0;

    foreach (var caveToVisit in caves[current])
    {
        var bigCave = caveToVisit.ToUpper() == caveToVisit;
        var visited = visitedCaves.Select(v => v).Contains(caveToVisit);

        if (bigCave || !visited)
        {
            count += TraverseCaves(caves, caveToVisit, new(visitedCaves));
        }
    }

    return count;
}

Dictionary<string, string[]> GetConnections(string[] data)
{
    var cartesian =
        from line in input
        let caves = line.Split("-")
        from c in new[] { (From: caves[0], To: caves[1]), (From: caves[1], To: caves[0]) }
        select c;

    var connections =
        from connection in cartesian
        group connection by connection.From into grp
        select new KeyValuePair<string, string[]>(grp.Key, grp.Select(connection => connection.To).ToArray());

    return connections.ToDictionary(x => x.Key, x => x.Value);
}
