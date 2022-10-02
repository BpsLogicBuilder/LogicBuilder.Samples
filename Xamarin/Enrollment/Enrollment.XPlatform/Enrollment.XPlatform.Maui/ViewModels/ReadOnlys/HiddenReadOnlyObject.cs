using Enrollment.XPlatform.Services;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class HiddenReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public HiddenReadOnlyObject(
            IUiNotificationService uiNotificationService,
            string name,
            string templateName) : base(name, templateName, uiNotificationService)
        {
        }
    }
}
