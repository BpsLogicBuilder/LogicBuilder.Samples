using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters
{
    public class TextFieldTemplateParameters
    {
		public TextFieldTemplateParameters
		(
			[Comments("XAML template name for the field.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "TextTemplate")]
			[Domain("TextTemplate,CheckboxTemplate,DateTemplate,PasswordTemplate,HiddenTemplate,LabelTemplate,SwitchTemplate")]
			string templateName
		)
		{
			TemplateName = templateName;
		}

		public string TemplateName { get; set; }
    }
}