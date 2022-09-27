using System;

namespace Enrollment.XPlatform.ViewModels
{
    public interface IHasItemsSource
    {
        void Reload(object entity, Type entityType);
    }
}
