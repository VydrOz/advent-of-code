namespace AdventOfCode.Y2023.Day03;

using System;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Solutions;
using static System.Net.Mime.MediaTypeNames;

class Solution : SolutionBase
{
    public Solution() : base(03, 2023, "Gear Ratios") { }

    protected override string SolvePartOne()
    {
        var rows = Input.ToArray();
        var specChars = Parse(rows, new Regex(@"[^.0-9]"));
        var numbers = Parse(rows, new Regex(@"\d+"));
        
        return numbers
            .Where(p => specChars.Any(s => AreAdjacent(s, p)))
            .Select(p => int.Parse(p.Text))
            .Sum()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        var rows = Input.ToArray();
        var gears = Parse(rows, new Regex(@"\*"));
        var numbers = Parse(rows, new Regex(@"\d+"));

        return gears
            .Select(g => numbers.Where(n => AreAdjacent(n, g)))
            .Where(n => n.Count() == 2)
            .Select(n => int.Parse(n.First().Text) * int.Parse(n.Last().Text))
            .Sum()
            .ToString();
    }

    /// <summary>
    /// return true if special char and number are on same column and (StartA <= EndB) and (EndA >= StartB)
    /// </summary>
    /// <param name="specPar"></param>
    /// <param name="numPart"></param>
    /// <returns></returns>
    bool AreAdjacent(Part specPar, Part numPart)
    {
        
        return Math.Abs(numPart.X - specPar.X) <= 1
            && numPart.Range >= specPar.Y
            && numPart.Y <= specPar.Range;
    }

    /// <summary>
    /// get the postion (x,y) of matching strings
    /// </summary>
    /// <param name="rows"></param>
    /// <param name="regex"></param>
    /// <returns></returns>
    Part[] Parse(string[] rows, Regex regex)
    {
        return rows.SelectMany((r, i) => 
                regex.Matches(r)
                .Select(m =>
                    new Part
                    {
                        Text = m.Value,
                        X = i,
                        Y = m.Index,
                    })
                .ToArray())
            .ToArray();
    }
}

record Part
{
    public string Text;
    public int X;
    public int Y;
    public int Range => Y + Text.Length;
}