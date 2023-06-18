using LogicBuilder.Attributes;
using System.Collections.Generic;


namespace Enrollment.Domain.Entities
{
    public class AcademicModel : EntityModelBase
    {
		private int _userId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_UserId")]
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
		[AlsoKnownAs("Academic_User")]
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

		private string _lastHighSchoolLocation;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_LastHighSchoolLocation")]
		public string LastHighSchoolLocation
		{
			get { return _lastHighSchoolLocation; }
			set
			{
				if (_lastHighSchoolLocation == value)
					return;

				_lastHighSchoolLocation = value;
				OnPropertyChanged();
			}
		}

		private string _ncHighSchoolName;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_NcHighSchoolName")]
		public string NcHighSchoolName
		{
			get { return _ncHighSchoolName; }
			set
			{
				if (_ncHighSchoolName == value)
					return;

				_ncHighSchoolName = value;
				OnPropertyChanged();
			}
		}

		private string _homeSchoolType;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_HomeSchoolType")]
		public string HomeSchoolType
		{
			get { return _homeSchoolType; }
			set
			{
				if (_homeSchoolType == value)
					return;

				_homeSchoolType = value;
				OnPropertyChanged();
			}
		}

		private string _homeSchoolAssociation;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_HomeSchoolAssociation")]
		public string HomeSchoolAssociation
		{
			get { return _homeSchoolAssociation; }
			set
			{
				if (_homeSchoolAssociation == value)
					return;

				_homeSchoolAssociation = value;
				OnPropertyChanged();
			}
		}

		private System.DateTime _fromDate;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_FromDate")]
		public System.DateTime FromDate
		{
			get { return _fromDate; }
			set
			{
				if (_fromDate == value)
					return;

				_fromDate = value;
				OnPropertyChanged();
			}
		}

		private System.DateTime _toDate;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_ToDate")]
		public System.DateTime ToDate
		{
			get { return _toDate; }
			set
			{
				if (_toDate == value)
					return;

				_toDate = value;
				OnPropertyChanged();
			}
		}

		private string _graduationStatus;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_GraduationStatus")]
		public string GraduationStatus
		{
			get { return _graduationStatus; }
			set
			{
				if (_graduationStatus == value)
					return;

				_graduationStatus = value;
				OnPropertyChanged();
			}
		}

		private bool? _receivedGed;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_ReceivedGed")]
		public bool? ReceivedGed
		{
			get { return _receivedGed; }
			set
			{
				if (_receivedGed == value)
					return;

				_receivedGed = value;
				OnPropertyChanged();
			}
		}

		private string _gedLocation;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_GedLocation")]
		public string GedLocation
		{
			get { return _gedLocation; }
			set
			{
				if (_gedLocation == value)
					return;

				_gedLocation = value;
				OnPropertyChanged();
			}
		}

		private System.DateTime? _gedDate;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_GedDate")]
		public System.DateTime? GedDate
		{
			get { return _gedDate; }
			set
			{
				if (_gedDate == value)
					return;

				_gedDate = value;
				OnPropertyChanged();
			}
		}

		private bool _earnedCreditAtCmc;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_EarnedCreditAtCmc")]
		public bool EarnedCreditAtCmc
		{
			get { return _earnedCreditAtCmc; }
			set
			{
				if (_earnedCreditAtCmc == value)
					return;

				_earnedCreditAtCmc = value;
				OnPropertyChanged();
			}
		}

		private bool _attendedPriorColleges;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Academic_AttendedPriorColleges")]
		public bool AttendedPriorColleges
		{
			get { return _attendedPriorColleges; }
			set
			{
				if (_attendedPriorColleges == value)
					return;

				_attendedPriorColleges = value;
				OnPropertyChanged();
			}
		}

		private ICollection<InstitutionModel> _institutions;
		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("Academic_Institutions")]
		public ICollection<InstitutionModel> Institutions
		{
			get { return _institutions; }
			set
			{
				if (_institutions == value)
					return;

				_institutions = value;
				OnPropertyChanged();
			}
		}
    }
}