namespace AdventOfCode.Y2023.Day03;

using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Solutions;

class Solution : SolutionBase
{
    public Solution() : base(03, 2023, "Gear Ratios", true) { }

    protected override string SolvePartOne()
    {   
        var lines = Input.ToList();
        var specialChars = @"*$#+@-=/\";
        var result = 0;
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var matches = Regex.Matches(line, @".(\d+).").Select(m => m);
            foreach (var match in matches)
            {
                var index = line.IndexOf(match.Value, 0);
                var before = lines.ElementAtOrDefault(i - 1)?[index..(index + match.Length)] ?? "";
                var after = lines.ElementAtOrDefault(i + 1)?[index..(index + match.Length)] ?? "";

                if(match.Value.Any(specialChars.Contains) || before.Any(specialChars.Contains) || after.Any(specialChars.Contains))
                {
                    result += int.Parse(Regex.Match(match.Value, @"(\d+)").Value);
                }
            }
        }

        return result.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}