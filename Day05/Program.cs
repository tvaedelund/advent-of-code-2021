using System.Diagnostics;

var input = File.ReadAllLines("input.txt");

var sw = Stopwatch.StartNew();

Console.WriteLine($"Part1: {Solve1(input)}");
Console.WriteLine($"Part2: {Solve2(input)}");
Console.WriteLine($"TID DET TOG: {sw.ElapsedMilliseconds}ms");

int Solve1(string[] input)
{
    var endPoints = GetEndPoints(input)
        .Where(x => x.ElementAt(0).x == x.ElementAt(1).x || x.ElementAt(0).y == x.ElementAt(1).y);

    var lines = GetLines(endPoints);

    return GetResult(lines);
}

int Solve2(string[] input)
{
    var endPoints = GetEndPoints(input);

    var lines = GetLines(endPoints);

    return GetResult(lines);
}

IEnumerable<IEnumerable<Point>> GetEndPoints(string[] data)
{
    return data
    .Select(row => row
        .Split(" -> ")
        .Select(coord => coord
            .Split(',')
            .Select(int.Parse)
        )
        .Select(coord => new Point(
            coord.ElementAt(0),
            coord.ElementAt(1)
        ))
        .OrderBy(coord => coord.x)
        .ThenBy(coord => coord.y)
    );
}

IEnumerable<Point> GetLines(IEnumerable<IEnumerable<Point>> points)
{
    var lines = new List<Point>();

    foreach (var point in points)
    {
        var diffX = point.ElementAt(1).x - point.ElementAt(0).x;
        var diffY = point.ElementAt(1).y - point.ElementAt(0).y;
        var diff = (diffX > diffY) ? diffX : diffY;

        var line = new List<Point>();
        for (int i = 0; i <= diff; i++)
        {
            line.Add(new Point(point.ElementAt(0).x + diffX / diff * i, point.ElementAt(0).y + diffY / diff * i));
        }
        lines.AddRange(line);
    }

    return lines;
}

int GetResult(IEnumerable<Point> lines)
{
    return lines
	.GroupBy(coord => new { coord.x, coord.y })
	.Select(grp => new
	{
		key = grp.Key,
		cnt = grp.Count()
	})
	.Count(grp => grp.cnt > 1);
}

record Point(int x, int y);