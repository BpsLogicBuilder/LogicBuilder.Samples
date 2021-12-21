using Enrollment.Forms.Configuration.Directives;
using Enrollment.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Enrollment.Forms.Configuration.DataForm
{
    public interface IFormGroupSettings : IFormGroupBoxSettings
    {
        string ModelType { get; }
        string Title { get; }
        Dictionary<string, List<DirectiveDescriptor>> ConditionalDirectives { get; }
        Dictionary<string, List<ValidationRuleDescriptor>> ValidationMessages { get; }
    }
}
