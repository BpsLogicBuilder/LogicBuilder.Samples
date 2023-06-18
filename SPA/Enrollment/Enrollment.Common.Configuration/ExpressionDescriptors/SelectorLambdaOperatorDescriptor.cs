using System;
using System.Collections.Generic;

namespace Enrollment.Common.Configuration.ExpressionDescriptors
{
    public class SelectorLambdaOperatorDescriptor : OperatorDescriptorBase
    {
        public OperatorDescriptorBase Selector { get; set; }
        public string SourceElementType { get; set; }
        public string BodyType { get; set; }
        public string ParameterName { get; set; }
    }
}