namespace AdventOfCode.Y2023.Day04;

using Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class Solution : SolutionBase
{
    public Solution() : base(04, 2023, "Scratchcards") { }

    protected override string SolvePartOne()
    {
        return GetCards()
            .Sum(c => c.Points)
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        var cards = GetCards();
        var counter = cards.ToDictionary(c => c.Index, c => 1);

        foreach (var card in cards)
        {
            var copies = cards.Skip(card.Index).Take(card.NbMatch);
            foreach (var c in copies)
            {
                counter[c.Index] += counter[card.Index];
            }
        }

        return counter.Sum(c => c.Value).ToString();
    }

    Card[] GetCards()
    {
        var regex = new Regex(@"Card\s*(\d+):(?:\s*(\d+))* \|(?:\s*(\d+))*");
        return 
            Input.Split("\n").Select(i =>
            {
                var matchGroups = regex.Matches(i).First().Groups;
                return new Card
                (
                    Index: int.Parse(matchGroups[1].Value),
                    WinningNumbers: matchGroups[2].Captures.Select(m => int.Parse(m.Value)),
                    Numbers: matchGroups[3].Captures.Select(m => int.Parse(m.Value))
                );
            })
            .ToArray();
    }
}

record Card (int Index, IEnumerable<int> WinningNumbers, IEnumerable<int> Numbers) {

    public int NbMatch => Numbers.Count(n => WinningNumbers.Contains(n));
    public double Points => NbMatch == 0 ? 0 : Math.Pow(2, Convert.ToDouble(NbMatch - 1));
};