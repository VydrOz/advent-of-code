using Lib;

if (args.Length == 1 && SplitDate(args[0], out var date))
{
    if (date.d > 0)
    {
        var solution = SolutionResolver.GetSolution(date.y, date.d);
        if (solution != null)
        {
            solution?.Print();
        }
        else
        {
            error($"Solution for Year {date.y} and Day {date.d} doesn't exist");
        }
        
        return;
    }
    else
    {
        foreach (var solution in SolutionResolver.FetchSolutions(date.y))
        {
            solution?.Print();
        }

        return;
    }
}

error("Invalid Command");

void error(string msg)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(msg);
    Console.ResetColor();
}

bool SplitDate(string commandDate, out DateCommand dateCommand)
{
    bool success = false;
    dateCommand = new DateCommand(-1, -1);
    try
    {    
        var splitArgs = commandDate.Split('/');
        if (splitArgs.Length == 1)
        {
            dateCommand = new DateCommand(int.Parse(splitArgs[0]), 0);
            success = true;
        }
        else if (splitArgs.Length == 2)
        {
            dateCommand = new DateCommand(int.Parse(splitArgs[0]), int.Parse(splitArgs[1]));
            success = true;
        }
    }
    catch (Exception ex)
    {
        error(ex.Message);
    }

    return success;
}

record DateCommand(int y, int d);