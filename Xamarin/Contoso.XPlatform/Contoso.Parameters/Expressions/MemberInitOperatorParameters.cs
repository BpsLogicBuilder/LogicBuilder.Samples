using System.Collections.Generic;
using System;
using System.Linq;
using LogicBuilder.Attributes;

namespace Contoso.Parameters.Expressions
{
    public class MemberInitOperatorParameters : IExpressionParameter
    {
		public MemberInitOperatorParameters()
		{
		}

		public MemberInitOperatorParameters
		(
			[Comments("List of member bindings")]
			IList<MemberBindingItem> memberBindings,

			[Comments("The Select New type leave as null (uncheck) for anonymous types. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type newType = null
		)
		{
			MemberBindings = memberBindings.ToDictionary(m => m.Property, m => m.Selector);
			NewType = newType;
		}

		public IDictionary<string, IExpressionParameter> MemberBindings { get; set; }
		public Type NewType { get; set; }
    }
}