namespace AdventOfCode.Y2023.Day01;

using System.Text.RegularExpressions;
using AdventOfCode.Solutions;

public class Solution : SolutionBase
{
    public Solution() : base(01, 2023, "Trebuchet?!") { }

    protected override string SolvePartOne()
    {
        return Solve(@"\d");
    }

    protected override string SolvePartTwo()
    {
        return Solve(@"\d|one|two|three|four|five|six|seven|eight|nine");
    }

    private string Solve(string regex)
    {
        return Input
            .Select(l => new
            {
                first = Regex.Match(l, regex),
                second = Regex.Match(l, regex, RegexOptions.RightToLeft)
            })
            .Sum(o => MatchValue(o.first.Value) * 10 + MatchValue(o.second.Value))
            .ToString();
    }

    private int MatchValue(string s) => s switch
    {
        "one" => 1,
        "two" => 2,
        "three" => 3,
        "four" => 4,
        "five" => 5,
        "six" => 6,
        "seven" => 7,
        "eight" => 8,
        "nine" => 9,
        _ => int.Parse(s)
    };
}