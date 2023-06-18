using Enrollment.Domain;
using Enrollment.Spa.Flow.ScreenSettings.Views;

namespace Enrollment.Spa.Flow.Requests
{
    public class GridRequest : RequestBase
    {
        public EntityModelBase? Entity { get; set; }
        public override ViewType ViewType { get; set; }
    }
}
