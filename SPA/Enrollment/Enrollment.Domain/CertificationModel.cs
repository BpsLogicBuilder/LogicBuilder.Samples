using LogicBuilder.Attributes;


namespace Enrollment.Domain.Entities
{
    public class CertificationModel : EntityModelBase
    {
		private int _userId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Certification_UserId")]
		public int UserId
		{
			get { return _userId; }
			set
			{
				if (_userId == value)
					return;

				_userId = value;
				OnPropertyChanged();
			}
		}

		private UserModel _user;
		[AlsoKnownAs("Certification_User")]
		public UserModel User
		{
			get { return _user; }
			set
			{
				if (_user == value)
					return;

				_user = value;
				OnPropertyChanged();
			}
		}

		private bool _certificateStatementChecked;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Certification_CertificateStatementChecked")]
		public bool CertificateStatementChecked
		{
			get { return _certificateStatementChecked; }
			set
			{
				if (_certificateStatementChecked == value)
					return;

				_certificateStatementChecked = value;
				OnPropertyChanged();
			}
		}

		private bool _declarationStatementChecked;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Certification_DeclarationStatementChecked")]
		public bool DeclarationStatementChecked
		{
			get { return _declarationStatementChecked; }
			set
			{
				if (_declarationStatementChecked == value)
					return;

				_declarationStatementChecked = value;
				OnPropertyChanged();
			}
		}

		private bool _policyStatementsChecked;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Certification_PolicyStatementsChecked")]
		public bool PolicyStatementsChecked
		{
			get { return _policyStatementsChecked; }
			set
			{
				if (_policyStatementsChecked == value)
					return;

				_policyStatementsChecked = value;
				OnPropertyChanged();
			}
		}
    }
}