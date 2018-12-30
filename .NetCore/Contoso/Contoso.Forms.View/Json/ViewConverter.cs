using Contoso.Forms.View.Input;
using Contoso.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Forms.View.Json
{
    public class ViewConverter : JsonTypeConverter<BaseInputView>
    {
        public override string TypePropertyName => "TypeString";

        protected override Type GetDerivedType(string typeName)
            => Type.GetType(typeName, false);
    }
}
