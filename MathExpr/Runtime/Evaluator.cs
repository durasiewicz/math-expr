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

                switch (expression.Type)
                {
                    case ExpressionType.Add:
                        return left + right;
                    
                    case ExpressionType.Multiply:
                        return left * right;
                }

                break;
            }
        }

        return 0;
    }
}