using System;

namespace Contoso.XPlatform.ViewModels
{
    public interface IHasItemsSource
    {
        void Reload(object entity, Type entityType);
    }
}
