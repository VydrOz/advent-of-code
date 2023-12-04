namespace Lib;

public static class SolutionResolver
{
    public static SolutionBase? GetSolution(int year, int day)
    {
        var type = Type.GetType($"AdventOfCode.Y{year}.Day{day:D2}.Solution");
        if (type != null && Activator.CreateInstance(type) is SolutionBase solution)
        {
            return solution;
        }

        return null;
    }

    public static IEnumerable<SolutionBase?> FetchSolutions(int year)
    {
        foreach (int day in Enumerable.Range(1, 25).ToArray())
        {
            yield return GetSolution(year, day);
        }
    }
}