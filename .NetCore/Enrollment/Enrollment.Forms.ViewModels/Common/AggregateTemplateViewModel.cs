using System.Collections.Generic;

namespace Enrollment.Forms.ViewModels.Common
{
    public class AggregateTemplateViewModel
    {
		public string TemplateName { get; set; }
		public List<AggregateTemplateFieldsViewModel> Aggregates { get; set; }
    }
}