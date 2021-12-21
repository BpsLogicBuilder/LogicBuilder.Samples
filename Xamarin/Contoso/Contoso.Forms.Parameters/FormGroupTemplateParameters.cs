using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters
{
    public class FormGroupTemplateParameters
    {
		public FormGroupTemplateParameters
		(
			[Comments("XAML template name for the form field.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "PopupFormGroupTemplate")]
			[Domain("InlineFormGroupTemplate,PopupFormGroupTemplate")]
			string templateName
		)
		{
			TemplateName = templateName;
		}

		public string TemplateName { get; set; }
    }
}