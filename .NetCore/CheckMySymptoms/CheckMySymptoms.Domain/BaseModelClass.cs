using CheckMySymptoms.Domain.Json;
using CheckMySymptoms.Utils;
using LogicBuilder.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Domain
{
    [JsonConverter(typeof(ModelConverter))]
    abstract public class BaseModelClass : BaseModel
    {
        public string TypeFullName => this.GetType().ToTypeString();
    }
}
