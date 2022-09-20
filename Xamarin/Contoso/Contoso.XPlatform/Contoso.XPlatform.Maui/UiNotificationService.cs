﻿using Contoso.XPlatform.Flow.Settings;
using System.Reactive.Subjects;

namespace Contoso.XPlatform
{
    public class UiNotificationService
    {
        #region Fields
        #endregion Fields

        #region Properties
        public Subject<FlowSettings> FlowSettingsSubject { get; set; } = new Subject<FlowSettings>();
        public Subject<string> ValueChanged { get; set; } = new Subject<string>();
        public FlowSettings? FlowSettings { get; set; }
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
