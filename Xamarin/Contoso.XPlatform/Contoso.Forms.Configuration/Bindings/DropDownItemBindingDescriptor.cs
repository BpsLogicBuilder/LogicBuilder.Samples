namespace Contoso.Forms.Configuration.Bindings
{
    public class DropDownItemBindingDescriptor : ItemBindingDescriptor
    {
        public DropDownTemplateDescriptor DropDownTemplate { get; set; }
        public bool RequiresReload { get; set; }

        public override string TemplateName => DropDownTemplate.TemplateName;
    }
}
