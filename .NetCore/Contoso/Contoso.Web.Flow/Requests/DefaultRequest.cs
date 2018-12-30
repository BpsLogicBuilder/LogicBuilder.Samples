using System;
using System.Collections.Generic;
using System.Text;
using Contoso.Web.Flow.ScreenSettings.Views;

namespace Contoso.Web.Flow.Requests
{
    public class DefaultRequest : RequestBase
    {
        public override ViewType ViewType { get; set; }
    }
}
