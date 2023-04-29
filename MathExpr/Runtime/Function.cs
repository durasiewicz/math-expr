namespace MathExpr.Runtime;

public abstract class Function
{
    public int ParametersCount { get; }

    protected Function(int parametersCount)
    {
        ParametersCount = parametersCount;
    }

    public abstract decimal Call(params decimal[] @params);
}