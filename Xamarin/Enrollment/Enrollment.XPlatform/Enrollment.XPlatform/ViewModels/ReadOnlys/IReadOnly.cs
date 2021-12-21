using System.ComponentModel;

namespace Enrollment.XPlatform.ViewModels.ReadOnlys
{
    public interface IReadOnly : INotifyPropertyChanged, IFormField
    {
        string TemplateName { get; set; }
    }
}
