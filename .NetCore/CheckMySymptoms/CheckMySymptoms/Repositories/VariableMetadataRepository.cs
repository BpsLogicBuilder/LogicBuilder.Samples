using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckMySymptoms.Forms.View;
using CheckMySymptoms.ViewModels;

namespace CheckMySymptoms.Repositories
{
    public class VariableMetadataRepository : IVariableMetadataRepository
    {
        public ICollection<VariableMetadata> GetMetadata(string tableName)
        {
            return new List<VariableMetadata>
            {
                new VariableMetadata("Patient.Age", typeof(int).FullName, string.Empty, tableName),
                new VariableMetadata("Patient.Sex", typeof(char).FullName, string.Empty, tableName)
            };
        }
    }
}
