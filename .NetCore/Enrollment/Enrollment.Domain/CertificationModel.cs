using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class CertificationModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Certification_UserId")]
		public int UserId { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Certification_CertificateStatementChecked")]
		public bool CertificateStatementChecked { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Certification_DeclarationStatementChecked")]
		public bool DeclarationStatementChecked { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Certification_PolicyStatementsChecked")]
		public bool PolicyStatementsChecked { get; set; }
    }
}