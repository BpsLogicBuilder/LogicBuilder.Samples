namespace Contoso.Domain.Entities
{
    public class VariableMetaDataModel : BaseModelClass
    {
        public int VariableMetaDataId { get; set; }

        public string Data { get; set; }

        public System.DateTime LastUpdated { get; set; }
    }
}
