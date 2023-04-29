using MathExpr.Analyzers;
using MathExpr.Runtime;


var expr = """
    pow(6,2)
    """;

var eval = new Evaluator();
Console.WriteLine(eval.Eval(expr));
Console.ReadLine();