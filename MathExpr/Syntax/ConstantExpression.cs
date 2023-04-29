namespace MathExpr.Syntax;

public class ConstantExpression : Expression
{
    public object? Value { get; }

    public ConstantExpression(object? value)
    {
        Value = value;
    }
}