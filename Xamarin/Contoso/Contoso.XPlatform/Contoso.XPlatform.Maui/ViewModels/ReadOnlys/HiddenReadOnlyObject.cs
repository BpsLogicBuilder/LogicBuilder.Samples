using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Services;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class HiddenReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public HiddenReadOnlyObject(string name, string templateName, IContextProvider contextProvider) : base(name, templateName, contextProvider.UiNotificationService)
        {
        }
    }
}
