using System.Diagnostics;

namespace Lib;

public abstract class SolutionBase
{
    public int Day { get; }
    public int Year { get; }
    public string Title { get; }
    public bool Debug { get; set; }
    public string Input => LoadInput(Debug);
    public string DebugInput => LoadInput(true);

    public SolutionResult Part1 => Solve(SolvePartOne);
    public SolutionResult Part2 => Solve(SolvePartTwo);

    private protected SolutionBase(int day, int year, string title, bool useDebugInput = false)
    {
        Day = day;
        Year = year;
        Title = title;
        Debug = useDebugInput;
    }

    public IEnumerable<SolutionResult> SolveAll()
    {
        yield return Solve(SolvePartOne);
        yield return Solve(SolvePartTwo);
    }

    SolutionResult Solve(Func<string> SolverFunction)
    {
        if (Debug && DebugInput?.Count() < 1)
        {
            throw new Exception("DebugInput is null or empty");
        }
        else if (Input?.Count() < 1)
        {
            throw new Exception("Input is null or empty");
        }

        try
        {
            var timer = Stopwatch.StartNew();
            var result = SolverFunction();
            timer.Stop();
            return string.IsNullOrEmpty(result)
                ? SolutionResult.Empty
                : new SolutionResult { Answer = result, Time = timer.Elapsed };
        }
        catch (Exception)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
                return SolutionResult.Empty;
            }
            else
            {
                throw;
            }
        }
    }

    string LoadInput(bool debug = false)
    {
        var inputFilepath = $"./{Year}/Day{Day:D2}/{(debug ? "debug_" : "")}input.in";

        if (File.Exists(inputFilepath) && new FileInfo(inputFilepath).Length > 0)
        {
            return File.ReadAllText(inputFilepath);
        }

        return string.Empty;
    }

    public override string ToString() =>
        $"\n--- Day {Day}: {Title} --- {(Debug ? "!! Debug mode active, using DebugInput !!" : "")}\n"
        + $"{ResultToString(1, Part1)}\n"
        + $"{ResultToString(2, Part2)}";

    string ResultToString(int part, SolutionResult result) =>
        $"  - Part{part} => " + (string.IsNullOrEmpty(result.Answer)
            ? "Unsolved"
            : $"{result.Answer} ({result.Time.TotalMilliseconds}ms)");

    void PrintResult(string result)
    {
        if (result.Contains("Unsolved"))
            Console.ForegroundColor = ConsoleColor.Gray;
        else
            Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine(result);
    }

    public void Print()
    {
        var title = $"--- Day {Day}: {Title} --- ";
        var res1 = ResultToString(1, Part1);
        var res2 = ResultToString(2, Part2);

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write(title);
        if (Debug)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("!! Debug mode active, using DebugInput !!");
        }

        Console.WriteLine();

        PrintResult(res1);
        PrintResult(res2);

        Console.ResetColor();
    }

    protected abstract string SolvePartOne();
    protected abstract string SolvePartTwo();
}