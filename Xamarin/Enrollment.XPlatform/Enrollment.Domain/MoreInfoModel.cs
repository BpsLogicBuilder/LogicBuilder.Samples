using LogicBuilder.Attributes;
using System.ComponentModel.DataAnnotations;


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
		[Display(Name = "What is your most important reason for attending?")]
		[Required]
		[StringLength(4)]
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
		[Display(Name = "What is your overall educational goal?")]
		[Required]
		[StringLength(4)]
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
		[Display(Name = "Military Information:")]
		[Required]
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
		[Display(Name = "Military Status:")]
		[StringLength(4)]
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
		[Display(Name = "Veteran Type:")]
		[StringLength(4)]
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
		[Display(Name = "Branch Served:")]
		[StringLength(4)]
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
		[Display(Name = "Applicable Government Benefits:")]
		[StringLength(10)]
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