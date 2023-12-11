namespace AdventOfCode.Y2023.Day08;

using Lib;
using System.Text.RegularExpressions;

class Solution : SolutionBase
{
    public Solution() : base(08, 2023, "Haunted Wasteland", false) { }

    protected override string SolvePartOne()
    {
        var lines = Input.Split("\r\n");

        var steps = lines[0];
        var mapping = lines.Skip(2)
            .Select(l => Regex.Matches(l, @"([A-Z]{3})").Select(m => m.Value).ToArray())
            .ToDictionary(x => x[0], x => new { left = x[1], right = x[2]});

        var key = "AAA";
        var i = 0;
        var count = 0;
        while (key != "ZZZ")
        {
            key = steps[i] == 'L' ? mapping[key].left : mapping[key].right;
            count++;
            if (++i >= steps.Length) i = 0;
        }

        return count.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}