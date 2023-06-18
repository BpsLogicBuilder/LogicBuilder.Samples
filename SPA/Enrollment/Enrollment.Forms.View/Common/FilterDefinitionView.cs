using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class FilterDefinitionView
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public object Value { get; set; }
        public bool? IgnoreCase { get; set; }
        public string ValueSourceMember { get; set; }
    }
}