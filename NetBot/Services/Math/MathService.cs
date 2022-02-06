using System.Collections.Generic;
using System.Data;

namespace NetBot.Services.Math;

public class MathService : IMathService
{
    public ulong Sum(IEnumerable<ulong> source)
    {
        var sumResult = 0UL;
        foreach (var i in source)
        {
            sumResult += i;
        }

        return sumResult;
    }

    public ulong Multiply(IEnumerable<ulong> source)
    {
        var multiplyResult = 1UL;
        foreach (var i in source)
        {
            multiplyResult *= i;
        }

        return multiplyResult;
    }

    public double Eval(IEnumerable<string> expression)
    {
        var result = new DataTable().Compute(string.Concat(expression), null);
        return (double)result;
    }

    public string Convert(string number, int fromBase, int toBase)
    {
        return System.Convert.ToString(System.Convert.ToInt32(number, fromBase), toBase);
    }
}