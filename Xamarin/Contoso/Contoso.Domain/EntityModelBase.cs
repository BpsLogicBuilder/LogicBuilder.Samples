using Contoso.Domain.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Contoso.Domain
{
    [JsonConverter(typeof(ModelConverter))]
    public abstract class EntityModelBase : BaseModelClass, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;

            if (eventHandler != null)
            {
                var eventArgs = new PropertyChangedEventArgs(propertyName);

                this.PropertyChanged(this, eventArgs);
            }
        }
    }
}
