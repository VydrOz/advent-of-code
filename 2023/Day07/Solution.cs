namespace AdventOfCode.Y2023.Day07;

using Lib;
using System.Linq;

class Solution : SolutionBase
{
    public Solution() : base(07, 2023, "Camel Cards", true) { }

    protected override string SolvePartOne()
    {
        var hands = GetHands()
            .OrderBy(GetHandType)
            .ThenByDescending(GetHandPower);


        foreach (var h in hands)
        {
            Console.WriteLine(h.cards);
        }

        return hands.Select((h, i) => h.bid * (i + 1))
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
                    bid: int.Parse(l[1]) 
            ));
    }

    int GetHandType(Hand hand)
    {
        return int.Parse(hand.cards
            .Select(c => hand.cards.Count(cc => cc == c))
            .Order()
            .Aggregate("", (c,n) => c + n));
    }

    long GetHandPower(Hand hand)
    {
        return long.Parse(hand.cards.Select(c => "23456789TJQKA".IndexOf(c)).Aggregate("", (c, n) => c + n));
    }
}

record Hand(string cards, long bid);

//public class HandComparer : IComparer<string>
//{
//    private const string Cards = "23456789TJQKA";

//#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
//    public int Compare(string x, string y)
//#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
//    {
//        for (int i = 0; i < 6; i++)
//        {
//            var iOne = Cards.IndexOf(x[i]);
//            var iTwo = Cards.IndexOf(y[i]);

//            var compare = iOne.CompareTo(iTwo);

//            if (compare != 0) return compare;
//        }

//        return 0;
//    }
}