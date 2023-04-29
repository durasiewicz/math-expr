namespace MathExpr.Syntax;

public class BinaryExpression : Expression
{
    public Expression? Left { get; }
    public Expression? Right { get; }
    private readonly ExpressionType _type;

    public BinaryExpression(ExpressionType type, Expression? left, Expression? right) : base(type)
    {
        Left = left;
        Right = right;
        _type = type;
    }
}