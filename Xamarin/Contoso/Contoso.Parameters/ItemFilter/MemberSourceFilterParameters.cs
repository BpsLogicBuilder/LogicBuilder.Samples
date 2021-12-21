using LogicBuilder.Attributes;
using System;

namespace Contoso.Parameters.ItemFilter
{
    public class MemberSourceFilterParameters : ItemFilterParametersBase
    {
		public MemberSourceFilterParameters
		(
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. The property being compared and LHS of the operation.")]
			string field,

			[Comments("The filter operator (comparison).")]
			[Domain("eq, neq")]
			[ParameterEditorControl(ParameterControlType.DropDown)]
			[NameValue(AttributeNames.DEFAULTVALUE, "eq")]
			string oper,

			[Comments("The source field to get the RHS from an existing object which will then be compared with the LHS of the operation.")]
			string memberSource,

			[Comments("The element type for the memberSource field. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.  Use the full name (e.g. System.Int32) for literals or core platform types.")]
			Type type,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Contoso.Domain.Entities"
		)
		{
			Field = field;
			Operator = oper;
			MemberSource = memberSource;
			Type = type;
		}

		public string Field { get; set; }
		public string Operator { get; set; }
		public string MemberSource { get; set; }
		public Type Type { get; set; }
    }
}