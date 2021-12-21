using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Directives
{
    public class DirectiveParameters
    {
		public DirectiveParameters
		(
			[Comments("Details about the directive's function and its arguments.")]
			DirectiveDefinitionParameters definition,

			[Comments("Lambda expression which dynamically activates and desctivates the dependent function.")]
			FilterLambdaOperatorParameters condition
		)
		{
			Definition = definition;
			Condition = condition;
		}

		public DirectiveDefinitionParameters Definition { get; set; }
		public FilterLambdaOperatorParameters Condition { get; set; }
    }
}