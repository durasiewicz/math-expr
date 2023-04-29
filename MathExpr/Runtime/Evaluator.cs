using System.Diagnostics;
using MathExpr.Analyzers;
using MathExpr.Syntax;

namespace MathExpr.Runtime;

public class Evaluator
{
    private readonly Lexer _lexer = new();
    private readonly Parser _parser = new();

    public object Eval(string expression)
    {
        var tokens = _lexer.Lex(expression);
        var ast = _parser.Parse(tokens);

        return EvalExpression(ast);
    }

    private decimal EvalExpression(Expression expression)
    {
        switch (expression)
        {
            case ConstantExpression c:
                return decimal.Parse((string)c.Value);

            case BinaryExpression b:
            {
                var left = EvalExpression(b.Left);
                var right = EvalExpression(b.Right);

                return b.Type switch
                {
                    ExpressionType.Add => left + right,
                    ExpressionType.Subtract => left - right,
                    ExpressionType.Multiply => left * right,
                    ExpressionType.Divide => left / right,
                    _ => throw new ArgumentOutOfRangeException()
                };

                break;
            }
        }

        return 0;
    }
}