using LogicBuilder.Attributes;


namespace Enrollment.Domain.Entities
{
    public class InstitutionModel : EntityModelBase
    {
		private int _institutionId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_InstitutionId")]
		public int InstitutionId
		{
			get { return _institutionId; }
			set
			{
				if (_institutionId == value)
					return;

				_institutionId = value;
				OnPropertyChanged();
			}
		}

		private AcademicModel _academic;
		[AlsoKnownAs("Institution_Academic")]
		public AcademicModel Academic
		{
			get { return _academic; }
			set
			{
				if (_academic == value)
					return;

				_academic = value;
				OnPropertyChanged();
			}
		}

		private string _institutionState;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_InstitutionState")]
		public string InstitutionState
		{
			get { return _institutionState; }
			set
			{
				if (_institutionState == value)
					return;

				_institutionState = value;
				OnPropertyChanged();
			}
		}

		private string _institutionName;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_InstitutionName")]
		public string InstitutionName
		{
			get { return _institutionName; }
			set
			{
				if (_institutionName == value)
					return;

				_institutionName = value;
				OnPropertyChanged();
			}
		}

		private string _startYear;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_StartYear")]
		public string StartYear
		{
			get { return _startYear; }
			set
			{
				if (_startYear == value)
					return;

				_startYear = value;
				OnPropertyChanged();
			}
		}

		private string _endYear;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_EndYear")]
		public string EndYear
		{
			get { return _endYear; }
			set
			{
				if (_endYear == value)
					return;

				_endYear = value;
				OnPropertyChanged();
			}
		}

		private string _highestDegreeEarned;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_HighestDegreeEarned")]
		public string HighestDegreeEarned
		{
			get { return _highestDegreeEarned; }
			set
			{
				if (_highestDegreeEarned == value)
					return;

				_highestDegreeEarned = value;
				OnPropertyChanged();
			}
		}

		private System.DateTime? _monthYearGraduated;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_MonthYearGraduated")]
		public System.DateTime? MonthYearGraduated
		{
			get { return _monthYearGraduated; }
			set
			{
				if (_monthYearGraduated == value)
					return;

				_monthYearGraduated = value;
				OnPropertyChanged();
			}
		}

		private int _userId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Institution_UserId")]
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
    }
}