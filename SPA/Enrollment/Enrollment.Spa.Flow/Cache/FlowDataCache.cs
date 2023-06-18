using Enrollment.Domain;
using Enrollment.Forms.Parameters;
using Enrollment.Spa.Flow.ScreenSettings.Navigation;
using Enrollment.Spa.Flow.ScreenSettings.Views;

namespace Enrollment.Spa.Flow.Cache
{
    public class FlowDataCache
    {
        public RequestedFlowStage RequestedFlowStage { get; set; } = new RequestedFlowStage();
        public NavigationBar NavigationBar { get; set; } = new NavigationBar();
        public ScreenSettingsBase? ScreenSettings { get; set; }
        public ParametersDictionary ParametersItems { get; set; } = new ParametersDictionary();
        public ModelDictionary ModelItems { get; set; } = new ModelDictionary();
    }
}
