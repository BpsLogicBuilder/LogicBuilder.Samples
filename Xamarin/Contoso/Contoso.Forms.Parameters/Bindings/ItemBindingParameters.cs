using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.Bindings
{
	abstract public class ItemBindingParameters
    {
		public ItemBindingParameters
		(
			[Comments("The section of the item template we're binding the property to.")]
			[Domain("Header,Text,Detail")]
			[NameValue(AttributeNames.USEFOREQUALITY, "true")]
			[NameValue(AttributeNames.USEFORHASHCODE, "true")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Header")]
			string name,

			[Comments("Update fieldTypeSource first. The property to bind to the name section.")]
			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			string property,

			[Comments("Label for the field.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			string title,

			[Comments("Specify a format for the binding e.g. 'Value: {0:F2}'")]
			[NameValue(AttributeNames.DEFAULTVALUE, "{0}")]
			string stringFormat,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[NameValue(AttributeNames.DEFAULTVALUE, "Contoso.Domain.Entities")]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = null
		)
		{
			Name = name;
			Property = property;
			Title = title;
			StringFormat = stringFormat;
		}

		public string Name { get; set; }
		public string Property { get; set; }
		public string Title { get; set; }
		public string StringFormat { get; set; }
    }
}