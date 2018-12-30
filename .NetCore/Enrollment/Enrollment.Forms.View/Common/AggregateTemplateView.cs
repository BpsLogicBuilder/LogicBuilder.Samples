using System.Collections.Generic;

namespace Enrollment.Forms.View.Common
{
    public class AggregateTemplateView
    {
		public string TemplateName { get; set; }
		public List<AggregateTemplateFieldsView> Aggregates { get; set; }
    }
}