using Enrollment.XPlatform.Flow.Settings;
using System.Reactive.Subjects;

namespace Enrollment.XPlatform
{
    public interface IUiNotificationService
    {
        FlowSettings? FlowSettings { get; }
        Subject<FlowSettings> FlowSettingsSubject { get; }
        Subject<string> ValueChanged { get; }

        void NotifyFlowSettingsChanged(FlowSettings flowSettings);
        void NotifyPropertyChanged(string fieldName);
    }
}
