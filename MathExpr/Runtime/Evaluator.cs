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
        }

        return result;
    }

    private object EvalExpression(Expression expression)
    {
        switch (expression)
        {
            case ConstantExpression constantExpression:
                return constantExpression.Value;

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

                var @params = new List<decimal>();

                foreach (var paramExpression in functionCallExpression.Params)
                {
                    var expressionResult = EvalExpression(paramExpression);

                    if (IsIdentifier(expressionResult))
                    {
                        var variableName = ReadString(expressionResult);

                        if (!_variables.ContainsKey(variableName))
                        {
                            throw new Exception($"Undefined variable '{variableName}'.");
                        }
                        
                        @params.Add(_variables[variableName]);
                    }
                    else
                    {
                        @params.Add(ReadDecimal(expressionResult));
                    }
                }
                
                return function.Call(@params.ToArray());
            }
            
            case BinaryExpression binaryExpression:
            {
                var left = EvalExpression(binaryExpression.Left);
                var right = EvalExpression(binaryExpression.Right);

                if (binaryExpression.Type == ExpressionType.Assign)
                {
                    var leftValue = ReadString(left);
                    var rightValue = ReadDecimal(right);
                    _variables[leftValue] = rightValue;
                    return rightValue;
                }
                else
                {
                    var leftValue = ReadDecimal(left);
                    var rightValue = ReadDecimal(right);
                    
                    return binaryExpression.Type switch
                    {
                        ExpressionType.Add => leftValue + rightValue,
                        ExpressionType.Subtract => leftValue - rightValue,
                        ExpressionType.Multiply => leftValue * rightValue,
                        ExpressionType.Divide => leftValue / rightValue,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
            }
        }

        return 0;
    }

    private bool IsIdentifier(object obj) =>
        obj is string str && str.All(char.IsLetter);
    
    private string ReadString(object obj)
    {
        if (obj is not string)
        {
            throw new Exception("String expected.");
        }

        return (string)obj;
    }
    
    private decimal ReadDecimal(object obj)
    {
        if (!decimal.TryParse(ReadString(obj), out var result))
        {
            throw new Exception("Decimal expteceted.");
        }

        return result;
    }
}