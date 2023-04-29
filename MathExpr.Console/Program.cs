using MathExpr.Analyzers;
using MathExpr.Runtime;


var expr = """
    2 + 2 * 2
    """;

var eval = new Evaluator();
Console.WriteLine(eval.Eval(expr));
Console.ReadLine();