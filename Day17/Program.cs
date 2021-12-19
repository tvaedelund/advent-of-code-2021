using System.Diagnostics;

var sw = Stopwatch.StartNew();

// Test input
// const int minX = 20;
// const int maxX = 30;
// const int minY = -10;
// const int maxY = -5;

// Actual input
const int minX = 111;
const int maxX = 161;
const int minY = -154;
const int maxY = -101;

var result = Solve();

Console.WriteLine($"Part 1: {result.p1}");
Console.WriteLine($"Part 2: {result.p2}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

(int p1, int p2) Solve()
{
    var max = 0;
    var count = 0;

    for (int xV = 1; xV <= maxX; xV++)
    {
        for (int yV = minY; yV < 200; yV++)
        {
            var yTracker = new HashSet<int>();
            var current = new Probe(0, 0, xV, yV);
            var next = GetNextPos(current);

            while (!IsOutOfBounds(next))
            {
                yTracker.Add(next.yP);

                if (IsInTargetArea(next))
                {
                    max = yTracker.Max() > max ? yTracker.Max() : max;
                    count++;
                    break;
                }

                next = GetNextPos(next);
            }
        }
    }

    return (max, count);
}

Probe GetNextPos(Probe probe)
{
    int xP = probe.xP + probe.xV;
    int yP = probe.yP + probe.yV;
    int xV = probe.xV + (probe.xV > 0 ? -1 : probe.xV < 0 ? 1 : 0);
    int yV = probe.yV - 1;

    return new Probe(xP, yP, xV, yV);
}

bool IsOutOfBounds(Probe probe)
{
    return probe.xP > maxX || probe.yP < minY;
}

bool IsInTargetArea(Probe probe)
{
    return (probe.xP is >= minX and <= maxX) && (probe.yP is <= maxY and >= minY);
}

record Probe(int xP, int yP, int xV, int yV);
