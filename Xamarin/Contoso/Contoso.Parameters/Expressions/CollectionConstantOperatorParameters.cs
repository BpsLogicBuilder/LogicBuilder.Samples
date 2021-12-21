using System.Collections.Generic;
using System;

namespace Contoso.Parameters.Expressions
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