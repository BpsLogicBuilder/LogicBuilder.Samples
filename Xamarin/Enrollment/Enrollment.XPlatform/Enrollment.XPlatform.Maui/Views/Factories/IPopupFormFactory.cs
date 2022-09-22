using Enrollment.XPlatform.ViewModels.ReadOnlys;
using Enrollment.XPlatform.ViewModels.Validatables;

namespace Enrollment.XPlatform.Views.Factories
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
