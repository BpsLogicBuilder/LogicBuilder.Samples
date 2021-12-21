using System.ComponentModel;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public interface IReadOnly : INotifyPropertyChanged, IFormField
    {
        string TemplateName { get; set; }
    }
}
