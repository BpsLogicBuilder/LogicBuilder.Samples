using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Services;
using System.Diagnostics.CodeAnalysis;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class CheckboxReadOnlyObject : ReadOnlyObjectBase<bool>
    {
        public CheckboxReadOnlyObject(
            UiNotificationService uiNotificationService,
            string name,
            string templateName,
            string checkboxLabel) : base(name, templateName, uiNotificationService)
        {
            /*MemberNotNull unvailable in 2.1*/
            _checkboxLabel = null!;
            /*MemberNotNull unvailable in 2.1*/
            CheckboxLabel = checkboxLabel;
        }

        private string _checkboxLabel;
        public string CheckboxLabel
        {
            get => _checkboxLabel;
            //[MemberNotNull(nameof(_checkboxLabel))]
            set
            {
                if (_checkboxLabel == value)
                    return;

                _checkboxLabel = value;
                OnPropertyChanged();
            }
        }
    }
}
