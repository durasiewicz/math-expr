namespace MathExpr.Syntax;

public class Expression
{
    public ExpressionType Type { get; }

    protected Expression(ExpressionType type)
    {
        Type = type;
    }
}