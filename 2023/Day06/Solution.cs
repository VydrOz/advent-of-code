namespace AdventOfCode.Y2023.Day06;

using Lib;
using System.Text.RegularExpressions;

class Solution : SolutionBase
{
    public Solution() : base(06, 2023, "Wait For It") { }

    protected override string SolvePartOne()
    {
        var inputs = Input.Split("\n");
        (var times, var dists) = (ParseLongs(inputs[0]), ParseLongs(inputs[1]));

        return times
            .Select((t, i) => CalcNbWays(t, dists[i]))
            .Aggregate(1L, (x, y) => x * y)
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        var inputs = Input.Split("\n");
        (var time, var dist) = (ParseLong(inputs[0]), ParseLong(inputs[1]));

        return CalcNbWays(time, dist).ToString();
    }

    long CalcNbWays(long time, long dist)
        => Range(1, time - 2).Where(s => s * (time - s) > dist).Sum(s => 1);

    long[] ParseLongs(string input)
        => Regex.Matches(input, @"\d+").Select(m => long.Parse(m.Value)).ToArray();
    
    long ParseLong(string input)
        => long.Parse(Regex.Match(input.Replace(" ", ""), @"\d+").Value);

    IEnumerable<long> Range(int start, long count)
    {
        for (long i = start; i < count; i++)
        {
            yield return i;
        }
    }
}