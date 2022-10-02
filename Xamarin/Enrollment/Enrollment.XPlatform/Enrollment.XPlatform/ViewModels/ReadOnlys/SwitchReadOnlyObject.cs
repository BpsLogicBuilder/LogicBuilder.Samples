using System.Diagnostics.CodeAnalysis;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class SwitchReadOnlyObject : ReadOnlyObjectBase<bool>
    {
        public SwitchReadOnlyObject(
            IUiNotificationService uiNotificationService,
            string name,
            string templateName,
            string switchLabel) : base(name, templateName, uiNotificationService)
        {
            /*MemberNotNull unvailable in 2.1*/
            _switchLabel = null!;
            /*MemberNotNull unvailable in 2.1*/
            SwitchLabel = switchLabel;
        }

        private string _switchLabel;
        public string SwitchLabel
        {
            get => _switchLabel;
            //[MemberNotNull(nameof(_switchLabel))]
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
