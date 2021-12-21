using System.Collections.Generic;
using System;

namespace Contoso.Common.Configuration.ExpressionDescriptors
{
    public class MemberInitOperatorDescriptor : OperatorDescriptorBase
    {
		public IDictionary<string, OperatorDescriptorBase> MemberBindings { get; set; }
		public string NewType { get; set; }
    }
}