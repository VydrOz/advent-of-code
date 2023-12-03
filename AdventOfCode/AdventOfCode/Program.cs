using AdventOfCode.Solutions;

if (args.Length == 1)
{
    var splitArgs = args[0].Split('/');
    if (int.TryParse(splitArgs[0], out int year))
    {
        if (splitArgs.Length == 2 && int.TryParse(splitArgs[1], out int day))
        {
            var solution = SolutionResolver.GetSolution(year, day);
            solution?.Print();
            return;
        }
        else
        {
            foreach (var solution in SolutionResolver.FetchSolutions(year))
            {
                solution?.Print();
            }
            return;
        }
    }
}

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("Invalid Command");
Console.ResetColor();