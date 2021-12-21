using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


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
		[Display(Name = "Last High School Location:")]
		[Required]
		[StringLength(256)]
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
		[Display(Name = "NC High School Name:")]
		[StringLength(256)]
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
		[Display(Name = "Home School Type:")]
		[StringLength(256)]
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
		[Display(Name = "Home School Association:")]
		[StringLength(256)]
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
		[Required]
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
		[Display(Name = "To Date:")]
		[Required]
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
		[Display(Name = "Graduation Status:")]
		[Required]
		[StringLength(256)]
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
		[Display(Name = "Did you receive a GED?")]
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
		[Display(Name = "Where did you receive your GED?")]
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
		[Display(Name = "Date GED Received:")]
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
		[Display(Name = "Have you ever earned college credit hours at Charlotte School of Science")]
		[Required]
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
		[Display(Name = "Have you attended other colleges or universities?")]
		[Required]
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