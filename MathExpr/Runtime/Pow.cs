using raminrahimzada;

namespace MathExpr.Runtime;

public class Pow : Function
{
    public Pow() : base(2)
    {
    }

    public override decimal Call(params decimal[] @params) => DecimalMath.PowerN(@params[0], (int)@params[1]);
}