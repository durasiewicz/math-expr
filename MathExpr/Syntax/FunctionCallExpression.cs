namespace MathExpr.Syntax;

public class FunctionCallExpression : Expression
{
    private readonly string _name;
    private readonly Expression[] _params;

    public FunctionCallExpression(string name, params Expression[] @params) : base(ExpressionType.FunctionCall)
    {
        _params = @params;
        _name = name;
    }
}