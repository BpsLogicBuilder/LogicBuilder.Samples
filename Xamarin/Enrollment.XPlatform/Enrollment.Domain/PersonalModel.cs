using LogicBuilder.Attributes;
using System.ComponentModel.DataAnnotations;


namespace Enrollment.Domain.Entities
{
    public class PersonalModel : EntityModelBase
    {
		private int _userId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_UserId")]
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
		[AlsoKnownAs("Personal_User")]
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

		private string _userIdString;
		public string UserIdString
		{
			get { return _userIdString; }
			set
			{
				if (_userIdString == value)
					return;

				_userIdString = value;
				OnPropertyChanged();
			}
		}

		private string _firstName;
		[Display(Name = "First Name:")]
		[Required]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_FirstName")]
		public string FirstName
		{
			get { return _firstName; }
			set
			{
				if (_firstName == value)
					return;

				_firstName = value;
				OnPropertyChanged();
			}
		}

		private string _middleName;
		[Display(Name = "Middle Name:")]
		[StringLength(20)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_MiddleName")]
		public string MiddleName
		{
			get { return _middleName; }
			set
			{
				if (_middleName == value)
					return;

				_middleName = value;
				OnPropertyChanged();
			}
		}

		private string _fullName;
		public string FullName
		{
			get { return _fullName; }
			set
			{
				if (_fullName == value)
					return;

				_fullName = value;
				OnPropertyChanged();
			}
		}

		private string _lastName;
		[Display(Name = "Last Name:")]
		[Required]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_LastName")]
		public string LastName
		{
			get { return _lastName; }
			set
			{
				if (_lastName == value)
					return;

				_lastName = value;
				OnPropertyChanged();
			}
		}

		private string _suffix;
		[Display(Name = "Suffix:")]
		[StringLength(6)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_Suffix")]
		public string Suffix
		{
			get { return _suffix; }
			set
			{
				if (_suffix == value)
					return;

				_suffix = value;
				OnPropertyChanged();
			}
		}

		private string _address1;
		[Display(Name = "Mailing Address1:")]
		[Required]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_Address1")]
		public string Address1
		{
			get { return _address1; }
			set
			{
				if (_address1 == value)
					return;

				_address1 = value;
				OnPropertyChanged();
			}
		}

		private string _address2;
		[Display(Name = "Mailing Address2:")]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_Address2")]
		public string Address2
		{
			get { return _address2; }
			set
			{
				if (_address2 == value)
					return;

				_address2 = value;
				OnPropertyChanged();
			}
		}

		private string _city;
		[Display(Name = "City:")]
		[Required]
		[StringLength(30)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_City")]
		public string City
		{
			get { return _city; }
			set
			{
				if (_city == value)
					return;

				_city = value;
				OnPropertyChanged();
			}
		}

		private string _state;
		[Required]
		[StringLength(25)]
		[Display(Name = "State:")]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_State")]
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

		private string _county;
		[Display(Name = "County:")]
		[StringLength(25)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_County")]
		public string County
		{
			get { return _county; }
			set
			{
				if (_county == value)
					return;

				_county = value;
				OnPropertyChanged();
			}
		}

		private string _zipCode;
		[Display(Name = "Zip Code (00000):")]
		[Required]
		[StringLength(5)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_ZipCode")]
		public string ZipCode
		{
			get { return _zipCode; }
			set
			{
				if (_zipCode == value)
					return;

				_zipCode = value;
				OnPropertyChanged();
			}
		}

		private string _cellPhone;
		[Display(Name = "Cell Phone:")]
		[StringLength(15)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_CellPhone")]
		public string CellPhone
		{
			get { return _cellPhone; }
			set
			{
				if (_cellPhone == value)
					return;

				_cellPhone = value;
				OnPropertyChanged();
			}
		}

		private string _otherPhone;
		[Display(Name = "Other Phone:")]
		[StringLength(15)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_OtherPhone")]
		public string OtherPhone
		{
			get { return _otherPhone; }
			set
			{
				if (_otherPhone == value)
					return;

				_otherPhone = value;
				OnPropertyChanged();
			}
		}

		private string _primaryEmail;
		[Display(Name = "Primary Email):")]
		[StringLength(40)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Personal_PrimaryEmail")]
		public string PrimaryEmail
		{
			get { return _primaryEmail; }
			set
			{
				if (_primaryEmail == value)
					return;

				_primaryEmail = value;
				OnPropertyChanged();
			}
		}
    }
}