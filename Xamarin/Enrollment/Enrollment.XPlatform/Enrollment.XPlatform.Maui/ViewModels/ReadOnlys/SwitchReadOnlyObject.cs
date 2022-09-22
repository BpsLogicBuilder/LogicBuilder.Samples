using System.Diagnostics.CodeAnalysis;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class SwitchReadOnlyObject : ReadOnlyObjectBase<bool>
    {
        public SwitchReadOnlyObject(
            UiNotificationService uiNotificationService,
            string name,
            string templateName,
            string switchLabel) : base(name, templateName, uiNotificationService)
        {
            SwitchLabel = switchLabel;
        }

        private string _switchLabel;
        public string SwitchLabel
        {
            get => _switchLabel;
            [MemberNotNull(nameof(_switchLabel))]
            set
            {
                if (_switchLabel == value)
                    return;

                _switchLabel = value;
                OnPropertyChanged();
            }
        }
    }
}
