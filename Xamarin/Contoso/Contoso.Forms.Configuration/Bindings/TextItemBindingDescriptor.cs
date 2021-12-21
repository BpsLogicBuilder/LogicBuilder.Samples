namespace Contoso.Forms.Configuration.Bindings
{
    public class TextItemBindingDescriptor : ItemBindingDescriptor
    {
        public TextFieldTemplateDescriptor TextTemplate { get; set; }

        public override string TemplateName => TextTemplate.TemplateName;
    }
}
