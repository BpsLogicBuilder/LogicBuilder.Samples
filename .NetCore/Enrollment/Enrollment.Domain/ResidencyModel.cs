using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Enrollment.Domain.Entities
{
    public class ResidencyModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_UserId")]
		public int UserId { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_CitizenshipStatus")]
		public string CitizenshipStatus { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_ImmigrationStatus")]
		public string ImmigrationStatus { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_CountryOfCitizenship")]
		public string CountryOfCitizenship { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_ResidentState")]
		public string ResidentState { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_HasValidDriversLicense")]
		public bool HasValidDriversLicense { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_DriversLicenseState")]
		public string DriversLicenseState { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("Residency_DriversLicenseNumber")]
		public string DriversLicenseNumber { get; set; }

		[ListEditorControl(ListControlType.HashSetForm)]
		[AlsoKnownAs("Residency_StatesLivedIn")]
		public ICollection<StateLivedInModel> StatesLivedIn { get; set; }
    }
}