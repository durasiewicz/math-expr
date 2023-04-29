namespace MathExpr.Syntax;

public class BinaryExpression : Expression
{
    public BinaryExpressionType Type { get; }
    public Expression? Left { get; }
    public Expression? Right { get; }
    
    public enum BinaryExpressionType
    {
        Add,
        Subtract,
        Divide,
        Multiply,
        Assign,
        Remainder
    }

    public BinaryExpression(BinaryExpressionType type, Expression? left, Expression? right)
    {
        Type = type;
        Left = left;
        Right = right;
    }
}