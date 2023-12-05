namespace AdventOfCode.Y2023.Day02;

using Lib;
using System.Text.RegularExpressions;

class Solution : SolutionBase
{
    public Solution() : base(02, 2023, "Cube Conundrum") { }

    protected override string SolvePartOne()
    {
        return Input.Split("\n").Select(ParseGame)
            .Where(g => g.Red <= 12 && g.Green <= 13 && g.Blue <= 14)
            .Sum(g => g.Id)
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        return Input.Split("\n").Select(ParseGame)
            .Sum(g => g.Red * g.Green * g.Blue)
            .ToString();
    }

    Game ParseGame(string line)
    {
        return new Game
        {
            Id = GetMatchedValue(line, @"Game (\d+)").First(),
            Red = GetMatchedValue(line, @"(\d+) red").Max(),
            Green = GetMatchedValue(line, @"(\d+) green").Max(),
            Blue = GetMatchedValue(line, @"(\d+) blue").Max(),
        };
    }

    IEnumerable<int> GetMatchedValue(string str, string regex)
        => Regex.Matches(str, regex)
            .Select(m => int.Parse(m.Groups[1].Value));
}

record Game
{
    public int Id;
    public int Red;
    public int Green;
    public int Blue;
}