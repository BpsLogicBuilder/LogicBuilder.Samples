using LogicBuilder.Attributes;
using System;

namespace Contoso.Parameters.ItemFilter
{
    public class ValueSourceFilterParameters : ItemFilterParametersBase
    {
		public ValueSourceFilterParameters
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

			[Comments("The value (RHS) being compared with the LHS of the operation.")]
			object value,

			[Comments("The element type for the value field. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.  Use the full name (e.g. System.Int32) for literals or core platform types.")]
			Type type,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Contoso.Domain.Entities"
		)
		{
			Field = field;
			Operator = oper;
			Value = value;
			Type = type;
		}

		public string Field { get; set; }
		public string Operator { get; set; }
		public object Value { get; set; }
		public Type Type { get; set; }
    }
}