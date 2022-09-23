using Contoso.XPlatform.ViewModels.ReadOnlys;
using Contoso.XPlatform.ViewModels.Validatables;
using System;

namespace Contoso.XPlatform.Views.Factories
{
    internal class PopupFormFactory : IPopupFormFactory
    {
        private readonly Func<IValidatable, ChildFormArrayPageCS> getChildFormArrayPage;
        private readonly Func<IValidatable, ChildFormPageCS> getChildFormPage;
        private readonly Func<IValidatable, MultiSelectPageCS> getMultiSelectPage;
        private readonly Func<IReadOnly, ReadOnlyChildFormArrayPageCS> getReadOnlyChildFormArrayPage;
        private readonly Func<IReadOnly, ReadOnlyChildFormPageCS> getReadOnlyChildFormPage;
        private readonly Func<IReadOnly, ReadOnlyMultiSelectPageCS> getReadOnlyMultiSelectPage;

        public PopupFormFactory(
            Func<IValidatable, ChildFormArrayPageCS> getChildFormArrayPage,
            Func<IValidatable, ChildFormPageCS> getChildFormPage,
            Func<IValidatable, MultiSelectPageCS> getMultiSelectPage,
            Func<IReadOnly, ReadOnlyChildFormArrayPageCS> getReadOnlyChildFormArrayPage,
            Func<IReadOnly, ReadOnlyChildFormPageCS> getReadOnlyChildFormPage,
            Func<IReadOnly, ReadOnlyMultiSelectPageCS> getReadOnlyMultiSelectPage)
        {
            this.getChildFormArrayPage = getChildFormArrayPage;
            this.getChildFormPage = getChildFormPage;
            this.getMultiSelectPage = getMultiSelectPage;
            this.getReadOnlyChildFormArrayPage = getReadOnlyChildFormArrayPage;
            this.getReadOnlyChildFormPage = getReadOnlyChildFormPage;
            this.getReadOnlyMultiSelectPage = getReadOnlyMultiSelectPage;
        }

        public ChildFormArrayPageCS CreateChildFormArrayPage(IValidatable formArrayValidatable) 
            => getChildFormArrayPage(formArrayValidatable);

        public ChildFormPageCS CreateChildFormPage(IValidatable formValidatable)
            => getChildFormPage(formValidatable);

        public MultiSelectPageCS CreateMultiSelectPage(IValidatable multiSelectValidatable)
            => getMultiSelectPage(multiSelectValidatable);

        public ReadOnlyChildFormArrayPageCS CreateReadOnlyChildFormArrayPage(IReadOnly formArrayReadOnly)
            => getReadOnlyChildFormArrayPage(formArrayReadOnly);

        public ReadOnlyChildFormPageCS CreateReadOnlyChildFormPage(IReadOnly formReadOnly)
            => getReadOnlyChildFormPage(formReadOnly);

        public ReadOnlyMultiSelectPageCS CreateReadOnlyMultiSelectPage(IReadOnly multiSelectReadOnly)
            => getReadOnlyMultiSelectPage(multiSelectReadOnly);
    }
}
