using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;


namespace Enrollment.Domain.Entities
{
    public class ResidencyModel : EntityModelBase
    {
		private int _userId;
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_UserId")]
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
		[AlsoKnownAs("Residency_User")]
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

		private string _citizenshipStatus;
		[Display(Name = "Citizenship Status:")]
		[Required]
		[StringLength(6)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_CitizenshipStatus")]
		public string CitizenshipStatus
		{
			get { return _citizenshipStatus; }
			set
			{
				if (_citizenshipStatus == value)
					return;

				_citizenshipStatus = value;
				OnPropertyChanged();
			}
		}

		private string _immigrationStatus;
		[Display(Name = "Immigration Status:")]
		[StringLength(6)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_ImmigrationStatus")]
		public string ImmigrationStatus
		{
			get { return _immigrationStatus; }
			set
			{
				if (_immigrationStatus == value)
					return;

				_immigrationStatus = value;
				OnPropertyChanged();
			}
		}

		private string _countryOfCitizenship;
		[Display(Name = "Country of Citizenship:")]
		[StringLength(55)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_CountryOfCitizenship")]
		public string CountryOfCitizenship
		{
			get { return _countryOfCitizenship; }
			set
			{
				if (_countryOfCitizenship == value)
					return;

				_countryOfCitizenship = value;
				OnPropertyChanged();
			}
		}

		private string _residentState;
		[Display(Name = "Resident State:")]
		[Required]
		[StringLength(55)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_ResidentState")]
		public string ResidentState
		{
			get { return _residentState; }
			set
			{
				if (_residentState == value)
					return;

				_residentState = value;
				OnPropertyChanged();
			}
		}

		private bool _hasValidDriversLicense;
		[Display(Name = "Do you have a valid driver’s license?")]
		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_HasValidDriversLicense")]
		public bool HasValidDriversLicense
		{
			get { return _hasValidDriversLicense; }
			set
			{
				if (_hasValidDriversLicense == value)
					return;

				_hasValidDriversLicense = value;
				OnPropertyChanged();
			}
		}

		private string _driversLicenseState;
		[Display(Name = "State Driver’s License Issued From:")]
		[StringLength(10)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_DriversLicenseState")]
		public string DriversLicenseState
		{
			get { return _driversLicenseState; }
			set
			{
				if (_driversLicenseState == value)
					return;

				_driversLicenseState = value;
				OnPropertyChanged();
			}
		}

		private string _driversLicenseNumber;
		[Display(Name = "Driver's License Number:")]
		[StringLength(25)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_DriversLicenseNumber")]
		public string DriversLicenseNumber
		{
			get { return _driversLicenseNumber; }
			set
			{
				if (_driversLicenseNumber == value)
					return;

				_driversLicenseNumber = value;
				OnPropertyChanged();
			}
		}

		private ICollection<StateLivedInModel> _statesLivedIn;
		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("Residency_StatesLivedIn")]
		public ICollection<StateLivedInModel> StatesLivedIn
		{
			get { return _statesLivedIn; }
			set
			{
				if (_statesLivedIn == value)
					return;

				_statesLivedIn = value;
				OnPropertyChanged();
			}
		}
    }
}