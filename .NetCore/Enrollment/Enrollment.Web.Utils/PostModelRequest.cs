using Enrollment.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Utils
{
    public class PostModelRequest
    {
        public DataSourceRequestOptions Options { get; set; }
        public string ModelType { get; set; }
        public string DataType { get; set; }
        public BaseModelClass Model { get; set; }
    }
}
