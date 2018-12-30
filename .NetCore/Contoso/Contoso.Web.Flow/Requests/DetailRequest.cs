using Contoso.Forms.Parameters.Common;
using Contoso.Web.Flow.ScreenSettings.Views;

namespace Contoso.Web.Flow.Requests
{
    public class DetailRequest : RequestBase
    {
        public FilterGroupParameters FilterGroup { get; set; }
        public override ViewType ViewType { get; set; }
    }
}
