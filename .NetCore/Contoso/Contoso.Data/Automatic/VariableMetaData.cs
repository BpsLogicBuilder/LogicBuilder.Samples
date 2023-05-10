using System;

namespace Contoso.Data.Automatic
{
    public class VariableMetaData : BaseDataClass
    {
        public int VariableMetaDataId { get; set; }

        public string Data { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
