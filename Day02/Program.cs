var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part 1: {Solve1(input)}");
Console.WriteLine($"Part 2: {Solve2(input)}");

int Solve1(string[] input)
{
    var result = input
        .Select(x => x.Split(' '))
        .Select(x => new
        {
            c = x[0],
            p = int.Parse(x[1])
        })
        .Select(x => new
        {
            depth = x.c == "up" ? -x.p : x.c == "down" ? x.p : 0,
            pos = x.c == "forward" ? x.p : 0
        });

    return result.Sum(x => x.depth) * result.Sum(x => x.pos);
}

int Solve2(string[] input)
{
    var instrs = input
        .Select(x => x.Split(' '))
        .Select(x => new
        {
            c = x[0],
            p = int.Parse(x[1])
        });

    var d = 0;
    var p = 0;
    var a = 0;

    foreach (var x in instrs)
    {
        a += x.c == "up" ? -x.p : x.c == "down" ? x.p : 0;
        d += x.c == "forward" ? x.p * a : 0;
        p += x.c == "forward" ? x.p : 0;
    }

    return d * p;
}