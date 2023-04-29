using MathExpr.Analyzers;
using MathExpr.Syntax;

namespace MathExpr.Runtime;

public class Evaluator
{
    private readonly Lexer _lexer = new();
    private readonly Parser _parser = new();

    private readonly Dictionary<string, Function> _functions = new()
    {
        ["pow"] = new Pow()
    };

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
            case ConstantExpression constantExpression:
                return decimal.Parse((string)constantExpression.Value);

            case FunctionCallExpression functionCallExpression:
            {
                var name = functionCallExpression.Name?.Trim()?.ToLower();

                if (!_functions.TryGetValue(name, out var function))
                {
                    throw new Exception($"Unknown function '{name}'.");
                }

                if (function.ParametersCount != functionCallExpression.Params.Length)
                {
                    throw new Exception("Invalid parameters count.");
                }
                
                return function.Call(functionCallExpression.Params.Select(EvalExpression).ToArray());
            }
            
            case BinaryExpression binaryExpression:
            {
                var left = EvalExpression(binaryExpression.Left);
                var right = EvalExpression(binaryExpression.Right);

                return binaryExpression.Type switch
                {
                    ExpressionType.Add => left + right,
                    ExpressionType.Subtract => left - right,
                    ExpressionType.Multiply => left * right,
                    ExpressionType.Divide => left / right,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        return 0;
    }
}