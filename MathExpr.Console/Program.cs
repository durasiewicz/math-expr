using MathExpr.Analyzers;
using MathExpr.Runtime;


var expr = """
    a = (1 + 1)
    b = 3
    pow(a,b)
    """;

var eval = new Evaluator();
Console.WriteLine(eval.Eval(expr));
Console.ReadLine();