using Contoso.XPlatform.Flow.Settings;
using System.Reactive.Subjects;

namespace Contoso.XPlatform
{
    public class UiNotificationService : IUiNotificationService
    {
        #region Fields
        #endregion Fields

        #region Properties
        public Subject<FlowSettings> FlowSettingsSubject { get; private set; } = new Subject<FlowSettings>();
        public Subject<string> ValueChanged { get; private set; } = new Subject<string>();
        public FlowSettings? FlowSettings { get; private set; }
        #endregion Properties

        public void NotifyPropertyChanged(string fieldName)
        {
            this.ValueChanged.OnNext(fieldName);
        }

        public void NotifyFlowSettingsChanged(FlowSettings flowSettings)
        {
            this.FlowSettings = flowSettings;
            this.FlowSettingsSubject.OnNext(flowSettings);
        }
    }
}
