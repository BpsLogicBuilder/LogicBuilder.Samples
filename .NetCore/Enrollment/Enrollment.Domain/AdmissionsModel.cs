using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class AdmissionsModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Admissions_UserId")]
		public int UserId { get; set; }

		[Display(Name = "What is your entering status?")]
		[Required]
		[StringLength(1)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Admissions_EnteringStatus")]
		public string EnteringStatus { get; set; }

		[Display(Name = "Select Enrollment Term you wish to attend:")]
		[Required]
		[StringLength(6)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Admissions_EnrollmentTerm")]
		public string EnrollmentTerm { get; set; }

		[Display(Name = "Select Enrollment Year you wish to attend:")]
		[Required]
		[StringLength(4)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Admissions_EnrollmentYear")]
		public string EnrollmentYear { get; set; }

		[Display(Name = "Choose a program type:")]
		[Required]
		[StringLength(55)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Admissions_ProgramType")]
		public string ProgramType { get; set; }

		[Display(Name = "Choose a program:")]
		[Required]
		[StringLength(55)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Admissions_Program")]
		public string Program { get; set; }
    }
}