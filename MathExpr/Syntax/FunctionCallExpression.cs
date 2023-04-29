namespace MathExpr.Syntax;

public class FunctionCallExpression : Expression
{
    public string Name { get; }
    public Expression[] Params { get; }

    public FunctionCallExpression(string name, params Expression[] @params) : base(ExpressionType.FunctionCall)
    {
        Name = name;
        Params = @params;
    }
}