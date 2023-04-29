namespace MathExpr.Syntax;

public class NegateExpression : Expression
{
    public Expression? Expression { get; }

    public NegateExpression(Expression? expression)
    {
        Expression = expression;
    }
}