using Contoso.XPlatform.Services;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class HiddenReadOnlyObject<T> : ReadOnlyObjectBase<T>
    {
        public HiddenReadOnlyObject(IContextProvider contextProvider, string name, string templateName) : base(name, templateName, contextProvider.UiNotificationService)
        {
        }
    }
}
