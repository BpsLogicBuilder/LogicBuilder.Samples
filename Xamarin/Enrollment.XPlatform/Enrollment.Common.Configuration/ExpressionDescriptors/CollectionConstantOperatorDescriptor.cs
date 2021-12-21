using System.Collections.Generic;
using System;

namespace Enrollment.Common.Configuration.ExpressionDescriptors
{
    public class CollectionConstantOperatorDescriptor : OperatorDescriptorBase
    {
		public string ElementType { get; set; }
		public ICollection<object> ConstantValues { get; set; }
    }
}