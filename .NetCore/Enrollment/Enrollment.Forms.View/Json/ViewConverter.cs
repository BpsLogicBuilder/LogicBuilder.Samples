using Enrollment.Forms.View.Input;
using Enrollment.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.View.Json
{
    public class ViewConverter : JsonTypeConverter<BaseInputView>
    {
        public override string TypePropertyName => "TypeString";

        protected override Type GetDerivedType(string typeName)
            => Type.GetType(typeName, false);
    }
}
