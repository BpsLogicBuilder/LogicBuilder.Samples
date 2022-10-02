using Contoso.XPlatform.Flow.Settings;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace Contoso.XPlatform
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
