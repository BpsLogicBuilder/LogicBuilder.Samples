using LogicBuilder.Attributes;

namespace Contoso.Parameters.Expressions
{
    public class MemberSelectorOperatorParameters : IExpressionParameter
    {
		public MemberSelectorOperatorParameters()
		{
		}

		public MemberSelectorOperatorParameters
		(
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. Full or parial member name from the source operand parent.")]
			string memberFullName,

			[Comments("Source Operand.")]
			IExpressionParameter sourceOperand,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[NameValue(AttributeNames.DEFAULTVALUE, "Enrollment.Domain.Entities")]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = null
		)
		{
			MemberFullName = memberFullName;
			SourceOperand = sourceOperand;
		}

		public string MemberFullName { get; set; }
		public IExpressionParameter SourceOperand { get; set; }
    }
}