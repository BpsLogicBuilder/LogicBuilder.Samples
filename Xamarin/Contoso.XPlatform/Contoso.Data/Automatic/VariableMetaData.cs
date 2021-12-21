using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contoso.Data.Automatic
{
    [Table("VariableMetaData", Schema = "Automatic")]
    public class VariableMetaData : BaseDataClass
    {
        public int VariableMetaDataId { get; set; }

        public string Data { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
