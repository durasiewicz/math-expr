using System.Runtime.CompilerServices;
using MathExpr.Analyzers;
using MathExpr.Runtime;


var expr = """
    a = -(2 + 2)
    """;

var eval = new Evaluator();
Console.WriteLine(eval.Eval(expr));
Console.ReadLine();

int a = 0;
var b = -(1 + 2);