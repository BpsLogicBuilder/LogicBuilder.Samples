using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.Validation;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.DataForm
{
    public interface IFormGroupSettings : IFormGroupBoxSettings
    {
        string ModelType { get; }
        string Title { get; }
        Dictionary<string, List<DirectiveDescriptor>> ConditionalDirectives { get; }
        Dictionary<string, List<ValidationRuleDescriptor>> ValidationMessages { get; }
    }
}
