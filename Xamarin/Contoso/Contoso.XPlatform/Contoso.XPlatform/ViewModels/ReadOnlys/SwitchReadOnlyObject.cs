using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class SwitchReadOnlyObject : ReadOnlyObjectBase<bool>
    {
        public SwitchReadOnlyObject(string name, string templateName, string switchLabel, IContextProvider contextProvider) : base(name, templateName, contextProvider.UiNotificationService)
        {
            SwitchLabel = switchLabel;
        }

        private string _switchLabel;
        public string SwitchLabel
        {
            get => _switchLabel;
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
