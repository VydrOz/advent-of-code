namespace AdventOfCode.Y2023.Day05;

using Lib;
using System.Formats.Tar;
using System.Linq;
using System.Text.RegularExpressions;

class Solution : SolutionBase
{
    public Solution() : base(05, 2023, "If You Give A Seed A Fertilizer", false) { }

    protected override string SolvePartOne()
    {
        var almanac = GetAlmanac();

        return "";
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    Almanac GetAlmanac()
    {
        var sections = Input.Split("\n\n");

        var seeds = Regex.Matches(sections[0], @"\d+")
            .Select(m => long.Parse(m.Value))
            .ToArray();

        // foreach line of section (first line is the title "x-to-y maps:")
        // get array of entries for each section
        var maps = sections.Skip(1)
            .Select(s => s.Split("\n")
                .Skip(1)
                .Select(l => l.Split(" ").Select(long.Parse).ToArray()) // list of long[] => destination, source, range
                .Select(p =>
                    new Entry(
                        new Range(p[0], p[2] + p[0] - 1),
                        new Range(p[1], p[2] + p[1] - 1)))
                .ToArray()
            )
            .Select(entries => new Map(entries))
            .ToArray();

        return new Almanac(seeds, maps);
    }
}

record Range(long start, long end);
record Entry(Range destination, Range source);
record Map(Entry[] entries);
record Almanac(long[] seeds, Map[] maps);