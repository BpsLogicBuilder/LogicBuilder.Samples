using System;

namespace Enrollment.Parameters.Expressions
{
    public class CastOperatorParameters : IExpressionParameter
    {
        public CastOperatorParameters()
        {
        }

        public CastOperatorParameters(IExpressionParameter operand, Type type)
        {
            Operand = operand;
            Type = type;
        }

        public IExpressionParameter Operand { get; set; }
        public Type Type { get; set; }
    }
}