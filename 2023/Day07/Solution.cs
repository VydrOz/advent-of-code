namespace AdventOfCode.Y2023.Day07;

using AdventOfCode.Y2023.Day04;
using Lib;
using System.Linq;

class Solution : SolutionBase
{
    public Solution() : base(07, 2023, "Camel Cards") { }

    protected override string SolvePartOne()
    {
        return GetHands()
            .OrderBy(GetHandType)
            .ThenBy(h => GetHandPower(h, "_23456789TJQKA"))
            .Select((h, i) => h.bid * (i + 1))
            .Sum()
            .ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    IEnumerable<Hand> GetHands()
    {
        return Input.Split("\n")
            .Select(l => l.Split(" "))
            .Select(l =>
                new Hand(
                    cards: l[0],
                    bid: long.Parse(l[1]) 
            ));
    }

    long GetHandType(Hand hand)
        => Parse(hand.cards.Select(c => hand.cards.Count(cc => cc == c)).OrderDescending());

    long GetHandPower(Hand hand, string cards)
        => Parse(hand.cards.Select(c => cards.IndexOf(c).ToString("00")));

    long Parse<T>(IEnumerable<T> list)
        => long.Parse(list.Aggregate("", (c,n) => c + n));
}

record Hand(string cards, long bid);