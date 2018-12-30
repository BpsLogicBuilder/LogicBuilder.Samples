using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LogicBuilder.Attributes;

namespace Enrollment.Domain.Entities
{
    public class VariableMetaDataModel : BaseModelClass
    {
		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("VariableMetaData_VariableMetaDataId")]
		public int VariableMetaDataId { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("VariableMetaData_Data")]
		public string Data { get; set; }

		[VariableEditorControl(VariableControlType.SingleLineTextBox)]
		[AlsoKnownAs("VariableMetaData_LastUpdated")]
		public System.DateTime LastUpdated { get; set; }
    }
}