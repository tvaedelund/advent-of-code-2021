using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllLines("____input.txt");
Console.WriteLine($"Part 1: {Solve(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

// sw.Restart();
// Console.WriteLine();
// Console.WriteLine($"Part 2: {Solve(input)}");
// Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve(string[] input)
{
    var caves = GetCaves(input)
        .Where(c => c.to != "start" && c.from != "end")
        .ToList();

    foreach (var cave in caves.Where(c => c.from == "start"))
    {
        var test = TraverseCaves(caves, cave, new() { cave.from }, 0);
    }

    return 0;    
}

int TraverseCaves(IEnumerable<Cave> caves, Cave current, List<string> visitedCaves, int count)
{
    var bigCave = current.from.ToUpper() == current.from;
    var visited = visitedCaves.Select(v => v).Contains(current.to);
    var endCave = current.to == "end";

    if (endCave)
    {
        count++;
    }
    else if (bigCave || !visited)
    {
        visitedCaves.Add(current.to);

        var cavesToVisit = caves.Where(c => c.from == current.to);

        foreach (var caveToVisit in cavesToVisit)
        {
            count = TraverseCaves(caves, caveToVisit, visitedCaves, count);
        }
    }

    return count;
}

IEnumerable<Cave> GetCaves(string[] data)
{
    foreach (var line in data)
    {
        var splitLine = line.Split('-');
        yield return new Cave(splitLine[0], splitLine[1]);
        yield return new Cave(splitLine[1], splitLine[0]);
    }
}

record Cave(string from, string to);