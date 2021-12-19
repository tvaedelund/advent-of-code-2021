using System.Diagnostics;

var sw = Stopwatch.StartNew();
var input = "target area: x=20..30, y=-10..-5";

// const int minX = 20;
// const int maxX = 30;
// const int minY = -10;
// const int maxY = -5;

const int minX = 111;
const int maxX = 161;
const int minY = -154;
const int maxY = -101;

Console.WriteLine($"Part 1: {Solve(input)}");
Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

// sw.Restart();
// Console.WriteLine();
// Console.WriteLine($"Part 2: {Solve(input)}");
// Console.WriteLine($"Time: {sw.ElapsedMilliseconds}ms");

int Solve(string input)
{
    var max = 0;

    for (int xV = 2; xV <= maxX; xV++)
    {
        for (int yV = 2; yV < 300; yV++)
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
                    break;
                }

                next = GetNextPos(next);
            }
        }
    }

    return max;
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
