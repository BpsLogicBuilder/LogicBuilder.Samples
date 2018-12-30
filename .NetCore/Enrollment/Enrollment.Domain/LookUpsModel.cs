using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class LookUpsModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_LookUpsID")]
		public int LookUpsID { get; set; }

		[Required(AllowEmptyStrings = true)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_Text")]
		public string Text { get; set; }

		[StringLength(100)]
		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_ListName")]
		public string ListName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_Value")]
		public string Value { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_NumericValue")]
		public double? NumericValue { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_BooleanValue")]
		public bool? BooleanValue { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_DateTimeValue")]
		public System.DateTime? DateTimeValue { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_CharValue")]
		public char? CharValue { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_GuidValue")]
		public System.Guid? GuidValue { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_TimeSpanValue")]
		public System.TimeSpan? TimeSpanValue { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("LookUps_Order")]
		public int Order { get; set; }
    }
}