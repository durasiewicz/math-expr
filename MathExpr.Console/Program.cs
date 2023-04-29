using MathExpr.Analyzers;
using MathExpr.Runtime;


var expr = """
    a = 2
    pow(a,3)
    """;

var eval = new Evaluator();
Console.WriteLine(eval.Eval(expr));
Console.ReadLine();