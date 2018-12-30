using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class AcademicModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_UserId")]
		public int UserId { get; set; }

		[Display(Name = "Last High School Location:")]
		[Required]
		[StringLength(256)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_LastHighSchoolLocation")]
		public string LastHighSchoolLocation { get; set; }

		[Display(Name = "NC High School Name:")]
		[StringLength(256)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_NcHighSchoolName")]
		public string NcHighSchoolName { get; set; }

		[Display(Name = "Home School Type:")]
		[StringLength(256)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_HomeSchoolType")]
		public string HomeSchoolType { get; set; }

		[Display(Name = "Home School Association:")]
		[StringLength(256)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_HomeSchoolAssociation")]
		public string HomeSchoolAssociation { get; set; }

		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_FromDate")]
		public System.DateTime FromDate { get; set; }

		[Display(Name = "To Date:")]
		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_ToDate")]
		public System.DateTime ToDate { get; set; }

		[Display(Name = "Graduation Status:")]
		[Required]
		[StringLength(256)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_GraduationStatus")]
		public string GraduationStatus { get; set; }

		[Display(Name = "Did you receive a GED?")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_ReceivedGed")]
		public bool? ReceivedGed { get; set; }

		[Display(Name = "Where did you receive your GED?")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_GedLocation")]
		public string GedLocation { get; set; }

		[Display(Name = "Date GED Received:")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_GedDate")]
		public System.DateTime? GedDate { get; set; }

		[Display(Name = "Have you ever earned college credit hours at Charlotte School of Science")]
		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_EarnedCreditAtCmc")]
		public bool EarnedCreditAtCmc { get; set; }

		[Display(Name = "Have you attended other colleges or universities?")]
		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_AttendedPriorColleges")]
		public bool AttendedPriorColleges { get; set; }

		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("Academic_Institutions")]
		public ICollection<InstitutionModel> Institutions { get; set; }
    }
}