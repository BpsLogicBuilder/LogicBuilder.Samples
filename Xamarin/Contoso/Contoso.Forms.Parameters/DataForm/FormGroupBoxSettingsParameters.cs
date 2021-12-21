using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Forms.Parameters.DataForm
{
    public class FormGroupBoxSettingsParameters : FormItemSettingsParameters
	{
		public FormGroupBoxSettingsParameters
		(
			[NameValue(AttributeNames.DEFAULTVALUE, "Header")]
			[Comments("Title for the group box.")]
			string groupHeader,

			[Comments("Configuration for each field in the group box.")]
			List<FormItemSettingsParameters> fieldSettings,

			[Comments("Multibindings list for the group header field - typically used in edit mode.")]
			MultiBindingParameters headerBindings = null,

			[Comments("Hide this group box.")]
			bool isHidden = false
		)
		{
			if (fieldSettings.Any(s => s is FormGroupBoxSettingsParameters))
				throw new ArgumentException($"{nameof(fieldSettings)}: D8590E1F-D029-405F-8E6C-EA98803004B8");

			GroupHeader = groupHeader;
			FieldSettings = fieldSettings;
			HeaderBindings = headerBindings;
			IsHidden = isHidden;
		}

		public string GroupHeader { get; set; }
		public List<FormItemSettingsParameters> FieldSettings { get; set; }
		public MultiBindingParameters HeaderBindings { get; set; }
		public bool IsHidden { get; set; }
	}
}
