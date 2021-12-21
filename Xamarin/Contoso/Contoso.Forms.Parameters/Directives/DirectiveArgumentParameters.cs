using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.Directives
{
    public class DirectiveArgumentParameters
    {
		public DirectiveArgumentParameters
		(
			[Comments("Name of the argument for the directive method.")]
			string name,

			[Comments("Value of the argument for the directive method.")]
			object value,

			[Comments("The argument type. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument (For literals, the full name (e.g. System.Int32) is sufficient.)")]
			Type type
		)
		{
			Name = name;
			Value = value;
			Type = type;
		}

		public string Name { get; set; }
		public object Value { get; set; }
		public Type Type { get; set; }
    }
}