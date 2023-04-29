using MathExpr.Analyzers;
using MathExpr.Syntax;

namespace MathExpr.Runtime;

public class Evaluator
{
    private readonly Lexer _lexer = new();
    private readonly Parser _parser = new();
    private readonly Dictionary<string, decimal> _variables = new();
    private readonly Dictionary<string, Function> _functions = new()
    {
        ["pow"] = new Pow()
    };

    public object Eval(string expression)
    {
        var tokenCollections = _lexer.Lex(expression);

        object result = null;
        
        foreach (var tokenCollection in tokenCollections)
        {
            var ast = _parser.Parse(tokenCollection);
            result = EvalExpression(ast);

            if (result is string str && str.All(char.IsLetter))
            {
                if (!_variables.ContainsKey(str))
                {
                    throw new Exception($"Undefined variable '{str}'.");
                }
                
                result = _variables[str];
            }
        }

        return result;
    }

    private object EvalExpression(Expression expression)
    {
        switch (expression)
        {
            case ConstantExpression constantExpression:
                return constantExpression.Value;
            
            case NegateExpression negateExpression:
                return -ReadDecimal(EvalExpression(negateExpression.Expression));

            case FunctionCallExpression functionCallExpression:
                return EvalFunctionCallExpression(functionCallExpression);
            
            case BinaryExpression binaryExpression:
                return EvalBinaryExpression(binaryExpression);
        }

        return 0;
    }

    private object EvalFunctionCallExpression(FunctionCallExpression functionCallExpression)
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

        var @params = functionCallExpression.Params
            .Select(EvalExpression)
            .Select(ReadValue)
            .ToArray();

        return function.Call(@params);
    }

    private object EvalBinaryExpression(BinaryExpression binaryExpression)
    {
        var left = EvalExpression(binaryExpression.Left);
        var right = EvalExpression(binaryExpression.Right);

        if (binaryExpression.Type == ExpressionType.Assign)
        {
            var variableName = ReadString(left);
            var value = ReadValue(right);
            _variables[variableName] = value;
            return value;
        }

        var leftValue = ReadValue(left);
        var rightValue = ReadValue(right);

        return binaryExpression.Type switch
        {
            ExpressionType.Add => leftValue + rightValue,
            ExpressionType.Subtract => leftValue - rightValue,
            ExpressionType.Multiply => leftValue * rightValue,
            ExpressionType.Divide => leftValue / rightValue,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private decimal ReadValue(object? obj)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        if (!IsIdentifier(obj))
        {
            return ReadDecimal(obj);
        }
        
        var variableName = ReadString(obj);

        if (!_variables.ContainsKey(variableName))
        {
            throw new Exception($"Undefined variable '{variableName}'.");
        }

        return _variables[variableName];
    }
    
    private bool IsIdentifier(object obj) =>
        obj is string str && str.All(char.IsLetter);
    
    private string ReadString(object obj)
    {
        if (obj is not string str)
        {
            throw new Exception("String expected.");
        }

        return str;
    }
    
    private decimal ReadDecimal(object obj)
    {
        if (obj is decimal var)
        {
            return var;
        }
        
        if (!decimal.TryParse(ReadString(obj), out var result))
        {
            throw new Exception("Decimal expected.");
        }

        return result;
    }
}