using System.Collections.Generic;

namespace NetBot.Services.Math
{
    public interface IMathService
    {
        public ulong Sum(IEnumerable<ulong> source);
        public ulong Multiply(IEnumerable<ulong> source);
        public double Eval(IEnumerable<string> expression);
    }
}