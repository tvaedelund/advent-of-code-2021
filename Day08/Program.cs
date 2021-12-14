using System.Diagnostics;

var sw = Stopwatch.StartNew();

var input = File.ReadAllLines("input.txt")
    .Select(line => line
        .Split(" | ")
        .Select(item => item.Split(' '))
        .ToArray()
    )
    .ToArray();

// Key: Digit
// Value: Length
var digits = new Dictionary<int, int>
{
    {1, 2},
    {4, 4},
    {7, 3},
    {8, 7},
};

Console.WriteLine($"Part1: {Solve1(input)}");
Console.WriteLine($"Part2: {Solve2(input)}");
Console.WriteLine($"TID DET TOG: {sw.ElapsedMilliseconds}ms");

int Solve1(string[][][] input)
{
    var result = input.Select(entry => entry[1])
        .SelectMany(value => value)
        .Select(value => digits
            .Where(digit => digit.Value == value.Length)
        )
        .Where(found => found.Any());

    return result.Count();
}

int Solve2(string[][][] input)
{
    var result = 0;

    foreach (var line in input)
    {
        var digits = GetPatterns(line);
        result += GetValue(digits, line[1]);
    }

    return result;
}

// 0 vs 4 == 3
//"abcefg".Count(x => "bcdf".Any(y => y == x)).Dump();
// 6 vs 1 == 1
//"abdefg".Count(x => "cf".Any(y => y == x)).Dump();
// 5 vs 6 == 5
//"abdfg".Count(x => "abdefg".Any(y => y == x)).Dump();
// 3 vs 9 == 5
//"acdfg".Count(x => "abcdgf".Any(y => y == x)).Dump();

Dictionary<int, string> GetPatterns(string[][] line)
{
    var patterns = new Dictionary<int, string>();

    // We need to find length 6 before length 5
    foreach (var pattern in line[0].OrderByDescending(x => x.Length))
    {
        switch (pattern.Length)
        {
            case 2:
                patterns.Add(1, pattern);
                break;
            case 3:
                patterns.Add(7, pattern);
                break;
            case 4:
                patterns.Add(4, pattern);
                break;
            case 5:
                if (Compare(pattern, patterns[6]) == 5)
                {
                    patterns.Add(5, pattern);
                }
                else if (Compare(pattern, patterns[9]) == 5)
                {
                    patterns.Add(3, pattern);
                }
                else
                {
                    patterns.Add(2, pattern);
                }
                break;
            case 6:
                if (Compare(pattern, line[0].Single(d => d.Length == 2)) == 1)
                {
                    patterns.Add(6, pattern);
                }
                else if (Compare(pattern, line[0].Single(d => d.Length == 4)) == 3)
                {
                    patterns.Add(0, pattern);
                }
                else
                {
                    patterns.Add(9, pattern);
                }
                break;
            case 7:
                patterns.Add(8, pattern);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(pattern));
        }
    }
 
    return patterns;
}

int Compare(string pattern, string compareTo)
{
    return pattern.Count(x => compareTo.Any(y => y == x));
}

int GetValue(Dictionary<int, string> patterns, string[] digits)
{
    var result = string.Empty;

    foreach (var digit in digits)
    {
        result += patterns.Single(x => x.Value.OrderBy(y => y).SequenceEqual(digit.OrderBy(y => y))).Key;
    }

    return int.Parse(result);
}
