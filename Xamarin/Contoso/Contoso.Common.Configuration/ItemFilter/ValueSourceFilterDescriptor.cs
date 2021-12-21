namespace Contoso.Common.Configuration.ItemFilter
{
    public class ValueSourceFilterDescriptor : ItemFilterDescriptorBase
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
        public string Type { get; set; }
    }
}
