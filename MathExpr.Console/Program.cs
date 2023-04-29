using MathExpr.Runtime;
using static System.Console;

const string exitCommand = "/q"; 

WriteLine($"To exit type: {exitCommand}");

var evaluator = new Evaluator();

while (true)
{
    var line = ReadLine();

    if (string.IsNullOrEmpty(line))
    {
        continue;
    }
    
    if (line?.Trim()?.ToLower() == exitCommand)
    {
        break;
    }

    try
    {
        WriteLine(evaluator.Eval(line));
    }
    catch (Exception e)
    {
        WriteLine(e.Message);
    }
}