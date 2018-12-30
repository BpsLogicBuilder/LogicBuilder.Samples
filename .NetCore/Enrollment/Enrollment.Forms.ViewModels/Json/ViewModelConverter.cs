using Enrollment.Forms.ViewModels.Input;
using Enrollment.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.ViewModels.Json
{
    public class ViewModelConverter : JsonTypeConverter<BaseInputViewModel>
    {
        public override string TypePropertyName => "TypeString";

        protected override Type GetDerivedType(string typeName)
            => Type.GetType(typeName, false);
    }
}
