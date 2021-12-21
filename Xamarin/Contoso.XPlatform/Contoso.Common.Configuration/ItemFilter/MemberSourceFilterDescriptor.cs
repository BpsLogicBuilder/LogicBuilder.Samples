namespace Contoso.Common.Configuration.ItemFilter
{
    public class MemberSourceFilterDescriptor : ItemFilterDescriptorBase
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string MemberSource { get; set; }
        public string Type { get; set; }
    }
}
