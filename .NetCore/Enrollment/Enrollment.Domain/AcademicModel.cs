using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Enrollment.Domain.Entities
{
    public class AcademicModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_UserId")]
		public int UserId { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_LastHighSchoolLocation")]
		public string LastHighSchoolLocation { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_NcHighSchoolName")]
		public string NcHighSchoolName { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_HomeSchoolType")]
		public string HomeSchoolType { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_HomeSchoolAssociation")]
		public string HomeSchoolAssociation { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_FromDate")]
		public System.DateTime FromDate { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_ToDate")]
		public System.DateTime ToDate { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_GraduationStatus")]
		public string GraduationStatus { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_ReceivedGed")]
		public bool? ReceivedGed { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_GedLocation")]
		public string GedLocation { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_GedDate")]
		public System.DateTime? GedDate { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_EarnedCreditAtCmc")]
		public bool EarnedCreditAtCmc { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_AttendedPriorColleges")]
		public bool AttendedPriorColleges { get; set; }

		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("Academic_Institutions")]
		public ICollection<InstitutionModel> Institutions { get; set; }
    }
}