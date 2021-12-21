using Enrollment.Forms.Configuration.DataForm;
using Enrollment.XPlatform.Services;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public class HiddenReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public HiddenReadOnlyObject(string name, string templateName, IContextProvider contextProvider) : base(name, templateName, contextProvider.UiNotificationService)
        {
        }
    }
}
