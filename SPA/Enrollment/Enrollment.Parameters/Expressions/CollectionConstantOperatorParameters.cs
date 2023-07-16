using System;
using System.Collections.Generic;

namespace Enrollment.Parameters.Expressions
{
    public class CollectionConstantOperatorParameters : IExpressionParameter
    {
        public CollectionConstantOperatorParameters()
        {
        }

        public CollectionConstantOperatorParameters(ICollection<object> constantValues, Type elementType)
        {
            ConstantValues = constantValues;
            ElementType = elementType;
        }

        public Type ElementType { get; set; }
        public ICollection<object> ConstantValues { get; set; }
    }
}