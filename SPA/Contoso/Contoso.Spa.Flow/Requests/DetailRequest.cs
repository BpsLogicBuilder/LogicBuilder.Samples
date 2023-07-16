using Contoso.Domain;
using Contoso.Spa.Flow.ScreenSettings.Views;

namespace Contoso.Spa.Flow.Requests
{
    public class DetailRequest : RequestBase
    {
        public EntityModelBase? Entity { get; set; }
        public override ViewType ViewType { get; set; }
    }
}
