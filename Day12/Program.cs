using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Solve(input, false)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

sw.Restart();
Console.WriteLine();
Console.WriteLine($"Part 2: {Solve(input, true)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve(string[] input, bool p2)
{
    var caves = GetConnections(input);

    var result = TraverseCaves(caves, "start", new(), p2); ;

    return result;
}

int TraverseCaves(Dictionary<string, string[]> caves, string current, Dictionary<string, int> visitedCaves, bool p2)
{
    visitedCaves[current] = visitedCaves.GetValueOrDefault(current) + 1;

    if (current == "end")
    {
        return 1;
    }

    var count = 0;

    foreach (var caveToVisit in caves[current].Where(c => c != "start"))
    {
        var bigCave = caveToVisit.ToUpper() == caveToVisit;
        var visited = visitedCaves.GetValueOrDefault(caveToVisit);
        var anyVisitedTwice = visitedCaves.Any(c => c.Key.ToLower() == c.Key && c.Value > 1);

        if (bigCave || visited == 0 || (p2 && !anyVisitedTwice))
        {
            count += TraverseCaves(caves, caveToVisit, new(visitedCaves), p2);
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
