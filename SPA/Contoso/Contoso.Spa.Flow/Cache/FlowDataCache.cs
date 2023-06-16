using Contoso.Domain;
using Contoso.Forms.Parameters;
using Contoso.Spa.Flow.ScreenSettings.Navigation;
using Contoso.Spa.Flow.ScreenSettings.Views;

namespace Contoso.Spa.Flow.Cache
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
