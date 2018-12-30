using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class ResidencyModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_UserId")]
		public int UserId { get; set; }

		[Display(Name = "Citizenship Status:")]
		[Required]
		[StringLength(6)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_CitizenshipStatus")]
		public string CitizenshipStatus { get; set; }

		[Display(Name = "Immigration Status:")]
		[StringLength(6)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_ImmigrationStatus")]
		public string ImmigrationStatus { get; set; }

		[Display(Name = "Country of Citizenship:")]
		[StringLength(55)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_CountryOfCitizenship")]
		public string CountryOfCitizenship { get; set; }

		[Display(Name = "Resident State:")]
		[Required]
		[StringLength(55)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_ResidentState")]
		public string ResidentState { get; set; }

		[Display(Name = "Do you have a valid driver’s license?")]
		[Required]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_HasValidDriversLicense")]
		public bool HasValidDriversLicense { get; set; }

		[Display(Name = "State Driver’s License Issued From:")]
		[StringLength(10)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_DriversLicenseState")]
		public string DriversLicenseState { get; set; }

		[Display(Name = "Driver's License Number:")]
		[StringLength(25)]
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_DriversLicenseNumber")]
		public string DriversLicenseNumber { get; set; }

		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("Residency_StatesLivedIn")]
		public ICollection<StateLivedInModel> StatesLivedIn { get; set; }
    }
}