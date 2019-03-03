using CheckMySymptoms.Forms.View.Input;
using CheckMySymptoms.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Forms.View.Json
{
    public class InputViewConverter : JsonTypeConverter<BaseInputView>
    {
        public override string TypePropertyName => "TypeString";

        protected override Type GetDerivedType(string typeName)
            => Type.GetType(typeName, false);
    }
}
