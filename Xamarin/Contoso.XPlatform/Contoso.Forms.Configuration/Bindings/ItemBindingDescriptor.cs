namespace Contoso.Forms.Configuration.Bindings
{
    abstract public class ItemBindingDescriptor
    {
        public string Name { get; set; }
        public string Property { get; set; }
        public string Title { get; set; }
        public string StringFormat { get; set; }
        abstract public string TemplateName { get; }
    }
}
