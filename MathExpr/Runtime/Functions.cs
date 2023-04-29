using raminrahimzada;

namespace MathExpr.Runtime;

public class Pow : IFunction
{
    public string Name => nameof(Pow);
    public int ParametersCount => 2;
    public decimal Call(params decimal[] @params) => DecimalMath.Power(@params[0], @params[1]);
}

public class Sqrt : IFunction
{
    public string Name => nameof(Sqrt);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Sqrt(@params[0]);
}

public class Exp : IFunction
{
    public string Name => nameof(Exp);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Exp(@params[0]);
}

public class Log10 : IFunction
{
    public string Name => nameof(Log10);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Log10(@params[0]);
}

public class Log : IFunction
{
    public string Name => nameof(Log);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Log(@params[0]);
}

public class Cos : IFunction
{
    public string Name => nameof(Cos);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Cos(@params[0]);
}

public class Tan : IFunction
{
    public string Name => nameof(Tan);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Tan(@params[0]);
}

public class Sin : IFunction
{
    public string Name => nameof(Sin);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Sin(@params[0]);
}

public class Sinh : IFunction
{
    public string Name => nameof(Sinh);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Sinh(@params[0]);
}

public class Cosh : IFunction
{
    public string Name => nameof(Cosh);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Cosh(@params[0]);
}

public class Sign : IFunction
{
    public string Name => nameof(Sign);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Sign(@params[0]);
}

public class Tanh : IFunction
{
    public string Name => nameof(Tanh);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Tanh(@params[0]);
}

public class Abs : IFunction
{
    public string Name => nameof(Abs);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Abs(@params[0]);
}

public class Asin : IFunction
{
    public string Name => nameof(Asin);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Asin(@params[0]);
}

public class Atan : IFunction
{
    public string Name => nameof(Atan);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.ATan(@params[0]);
}

public class Atan2 : IFunction
{
    public string Name => nameof(Atan2);
    public int ParametersCount => 2;
    public decimal Call(params decimal[] @params) => DecimalMath.Atan2(@params[0], @params[1]);
}

public class Acos : IFunction
{
    public string Name => nameof(Acos);
    public int ParametersCount => 1;
    public decimal Call(params decimal[] @params) => DecimalMath.Acos(@params[0]);
}