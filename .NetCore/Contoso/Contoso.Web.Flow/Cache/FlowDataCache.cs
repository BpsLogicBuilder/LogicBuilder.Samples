using Contoso.Domain;
using Contoso.Forms.Parameters;
using Contoso.Web.Flow.ScreenSettings.Navigation;
using Contoso.Web.Flow.ScreenSettings.Views;
using LogicBuilder.RulesDirector;
using System.Collections.Generic;

namespace Contoso.Web.Flow.Cache
{
    public class FlowDataCache
    {
        #region Properties
        public RequestedFlowStage RequestedFlowStage { get; set; } = new RequestedFlowStage();
        public NavigationBar NavigationBar { get; set; } = new NavigationBar();
        public ScreenSettingsBase ScreenSettings { get; set; }
        public ParametersDictionary ParametersItems { get; set; } = new ParametersDictionary();
        public ModelDictionary ModelItems { get; set; } = new ModelDictionary();
        public Variables Variables { get; set; } = new Variables();
        public Dictionary<int, object> InputQuestionsAnswers { get; } = new Dictionary<int, object>();
        #endregion Properties
    }
}
