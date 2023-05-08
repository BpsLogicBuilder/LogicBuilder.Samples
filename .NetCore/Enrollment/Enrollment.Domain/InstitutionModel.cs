using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class InstitutionModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_InstitutionId")]
		public int InstitutionId { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_InstitutionState")]
		public string InstitutionState { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_InstitutionName")]
		public string InstitutionName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_StartYear")]
		public string StartYear { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_EndYear")]
		public string EndYear { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_HighestDegreeEarned")]
		public string HighestDegreeEarned { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_MonthYearGraduated")]
		public System.DateTime? MonthYearGraduated { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_UserId")]
		public int UserId { get; set; }
    }
}