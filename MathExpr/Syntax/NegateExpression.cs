namespace MathExpr.Syntax;

public class NegateExpression : Expression
{
    public Expression? Expression { get; }

    public NegateExpression(Expression? expression) : base(ExpressionType.Negate)
    {
        Expression = expression;
    }
}