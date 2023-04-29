namespace MathExpr.Runtime;

public interface IFunction
{
    string Name { get; }
    int ParametersCount { get; }
    decimal Call(params decimal[] @params);
}