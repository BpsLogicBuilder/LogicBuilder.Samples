using LogicBuilder.Attributes;


namespace Enrollment.Domain.Entities
{
    public class AdmissionsModel : EntityModelBase
    {
		private int _userId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Admissions_UserId")]
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
		[AlsoKnownAs("Admissions_User")]
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

		private string _enteringStatus;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Admissions_EnteringStatus")]
		public string EnteringStatus
		{
			get { return _enteringStatus; }
			set
			{
				if (_enteringStatus == value)
					return;

				_enteringStatus = value;
				OnPropertyChanged();
			}
		}

		private string _enrollmentTerm;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Admissions_EnrollmentTerm")]
		public string EnrollmentTerm
		{
			get { return _enrollmentTerm; }
			set
			{
				if (_enrollmentTerm == value)
					return;

				_enrollmentTerm = value;
				OnPropertyChanged();
			}
		}

		private string _enrollmentYear;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Admissions_EnrollmentYear")]
		public string EnrollmentYear
		{
			get { return _enrollmentYear; }
			set
			{
				if (_enrollmentYear == value)
					return;

				_enrollmentYear = value;
				OnPropertyChanged();
			}
		}

		private string _programType;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Admissions_ProgramType")]
		public string ProgramType
		{
			get { return _programType; }
			set
			{
				if (_programType == value)
					return;

				_programType = value;
				OnPropertyChanged();
			}
		}

		private string _program;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Admissions_Program")]
		public string Program
		{
			get { return _program; }
			set
			{
				if (_program == value)
					return;

				_program = value;
				OnPropertyChanged();
			}
		}
    }
}