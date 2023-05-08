using LogicBuilder.Attributes;


namespace Enrollment.Domain.Entities
{
    public class MoreInfoModel : EntityModelBase
    {
		private int _userId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_UserId")]
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
		[AlsoKnownAs("MoreInfo_User")]
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

		private string _reasonForAttending;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_ReasonForAttending")]
		public string ReasonForAttending
		{
			get { return _reasonForAttending; }
			set
			{
				if (_reasonForAttending == value)
					return;

				_reasonForAttending = value;
				OnPropertyChanged();
			}
		}

		private string _overallEducationalGoal;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_OverallEducationalGoal")]
		public string OverallEducationalGoal
		{
			get { return _overallEducationalGoal; }
			set
			{
				if (_overallEducationalGoal == value)
					return;

				_overallEducationalGoal = value;
				OnPropertyChanged();
			}
		}

		private bool _isVeteran;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_IsVeteran")]
		public bool IsVeteran
		{
			get { return _isVeteran; }
			set
			{
				if (_isVeteran == value)
					return;

				_isVeteran = value;
				OnPropertyChanged();
			}
		}

		private string _militaryStatus;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_MilitaryStatus")]
		public string MilitaryStatus
		{
			get { return _militaryStatus; }
			set
			{
				if (_militaryStatus == value)
					return;

				_militaryStatus = value;
				OnPropertyChanged();
			}
		}

		private string _militaryBranch;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_MilitaryBranch")]
		public string MilitaryBranch
		{
			get { return _militaryBranch; }
			set
			{
				if (_militaryBranch == value)
					return;

				_militaryBranch = value;
				OnPropertyChanged();
			}
		}

		private string _veteranType;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_VeteranType")]
		public string VeteranType
		{
			get { return _veteranType; }
			set
			{
				if (_veteranType == value)
					return;

				_veteranType = value;
				OnPropertyChanged();
			}
		}

		private string _governmentBenefits;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("MoreInfo_GovernmentBenefits")]
		public string GovernmentBenefits
		{
			get { return _governmentBenefits; }
			set
			{
				if (_governmentBenefits == value)
					return;

				_governmentBenefits = value;
				OnPropertyChanged();
			}
		}
    }
}