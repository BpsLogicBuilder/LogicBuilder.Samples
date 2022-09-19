using Contoso.Forms.Configuration.DataForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.XPlatform.ViewModels.ReadOnlys.Factories
{
    public interface IReadOnlyFactory
    {
        IReadOnly CreateFormReadOnlyObject(Type fieldType, string name, IChildFormGroupSettings setting);
    }
}
