using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.Validatables;

namespace Contoso.XPlatform.Views.Factories
{
    public interface IPopupFormFactory
    {
        ChildFormArrayPageCS CreateChildFormArrayPage(IValidatable formArrayValidatable);
        ChildFormPageCS CreateChildFormPage(IValidatable formValidatable);
        MultiSelectPageCS CreateMultiSelectPage(IValidatable multiSelectValidatable);
        ReadOnlyChildFormArrayPageCS CreateReadOnlyChildFormArrayPage(IReadOnly formArrayReadOnly);
        ReadOnlyChildFormPageCS CreateReadOnlyChildFormPage(IReadOnly formReadOnly);
        ReadOnlyMultiSelectPageCS CreateReadOnlyMultiSelectPage(IReadOnly multiSelectReadOnly);
    }
}
