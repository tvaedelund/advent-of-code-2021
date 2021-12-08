var input = File.ReadAllLines("input.txt")
    .Select(line => line
        .Split(" | ")
        .Select(item => item.Split(' '))
        .ToArray()
    )
    .ToArray();

var digits = new Dictionary<int, int>
{
    {0, 6},
    {1, 2},
    {2, 5},
    {3, 6},
    {4, 4},
    {5, 5},
    {6, 6},
    {7, 3},
    {8, 7},
    {9, 6},
};

Console.WriteLine($"Part1: {Solve(input, new[] {1, 4, 7, 8})}");

int Solve(string[][][] input, int[] digitsToFind)
{
    var result = input.Select(entry => entry[1])
        .SelectMany(value => value)
        .Select(value => digits
            .Where(digit => digit.Value == value.Length && digitsToFind.Any(dtf => dtf == digit.Key))
        )
        .Where(found => found.Any());

    return result.Count();
}