using MathExpr.Analyzers;
using MathExpr.Syntax;

namespace MathExpr.Runtime;

public class Evaluator
{
    private readonly Lexer _lexer = new();
    private readonly Parser _parser = new();
    private readonly Dictionary<string, decimal> _variables = new();
    private readonly Dictionary<string, IFunction> _functions;

    public Evaluator()
    {
        var functions = typeof(Evaluator).Assembly.GetTypes()
            .Where(q => !q.IsInterface)
            .Where(q => q.IsClass)
            .Where(q => q.IsAssignableTo(typeof(IFunction)))
            .Select(Activator.CreateInstance)
            .Cast<IFunction>()
            .ToList();

        var duplicates = functions
            .GroupBy(q => q.Name.Trim().ToLower())
            .Where(q => q.Count() > 1)
            .ToList();

        if (duplicates.Any())
        {
            throw new InvalidOperationException(
                $"Duplicated build-in functions: {string.Join(", ", duplicates.SelectMany(q => q).Select(q => q.Name).Distinct())}");
        }

        _functions = functions.ToDictionary(q => q.Name.Trim().ToLower(), q => q);
    }

    public object? Eval(string? expression)
    {
        if (string.IsNullOrEmpty(expression))
        {
            return null;
        }
        
        var tokenCollections = _lexer.Lex(expression);

        object? result = null;
        
        foreach (var tokenCollection in tokenCollections)
        {
            var ast = _parser.Parse(tokenCollection);
            result = EvalExpression(ast ?? throw new InvalidOperationException());

            if (result is string str && str.All(char.IsLetter))
            {
                result = ReadValue(result);
            }
        }

        return result;
    }

    private object? EvalExpression(Expression expression)
    {
        switch (expression)
        {
            case ConstantExpression constantExpression:
                return constantExpression.Value;
            
            case NegateExpression negateExpression:
                return -ReadValue(EvalExpression(negateExpression.Expression));

            case FunctionCallExpression functionCallExpression:
                return EvalFunctionCallExpression(functionCallExpression);
            
            case BinaryExpression binaryExpression:
                return EvalBinaryExpression(binaryExpression);
        }

        throw new InvalidOperationException($"Expression of type '{expression.GetType()}' is not supported.");
    }

    private object EvalFunctionCallExpression(FunctionCallExpression functionCallExpression)
    {
        var name = functionCallExpression.Name.Trim().ToLower();

        if (string.IsNullOrEmpty(name))
        {
            throw new InvalidOperationException("Function name is required.");
        }
        
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
        var left = EvalExpression(binaryExpression.Left ?? throw new InvalidOperationException("left == null"));
        var right = EvalExpression(binaryExpression.Right ?? throw new InvalidOperationException("right == null"));

        if (binaryExpression.Type == BinaryExpression.BinaryExpressionType.Assign)
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
            BinaryExpression.BinaryExpressionType.Add => leftValue + rightValue,
            BinaryExpression.BinaryExpressionType.Subtract => leftValue - rightValue,
            BinaryExpression.BinaryExpressionType.Multiply => leftValue * rightValue,
            BinaryExpression.BinaryExpressionType.Divide => leftValue / rightValue,
            BinaryExpression.BinaryExpressionType.Remainder => leftValue % rightValue,
            BinaryExpression.BinaryExpressionType.And => (int)leftValue & (int)rightValue,
            BinaryExpression.BinaryExpressionType.Or => (int)leftValue | (int)rightValue,
            BinaryExpression.BinaryExpressionType.Xor => (int)leftValue ^ (int)rightValue,
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