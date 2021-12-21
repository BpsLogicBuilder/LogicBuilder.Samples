using LogicBuilder.Attributes;
using System.ComponentModel.DataAnnotations;


namespace Enrollment.Domain.Entities
{
    public class StateLivedInModel : EntityModelBase
    {
		private int _stateLivedInId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("StateLivedIn_StateLivedInId")]
		public int StateLivedInId
		{
			get { return _stateLivedInId; }
			set
			{
				if (_stateLivedInId == value)
					return;

				_stateLivedInId = value;
				OnPropertyChanged();
			}
		}

		private string _state;
		[Required]
		[StringLength(25)]
		[Display(Name = "State:")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("StateLivedIn_State")]
		public string State
		{
			get { return _state; }
			set
			{
				if (_state == value)
					return;

				_state = value;
				OnPropertyChanged();
			}
		}

		private int _userId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("StateLivedIn_UserId")]
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

		private ResidencyModel _residency;
		[AlsoKnownAs("StateLivedIn_Residency")]
		public ResidencyModel Residency
		{
			get { return _residency; }
			set
			{
				if (_residency == value)
					return;

				_residency = value;
				OnPropertyChanged();
			}
		}
    }
}