using System;
using System.Collections.Generic;

namespace Enrollment.Common.Configuration.ExpressionDescriptors
{
    public class FilterLambdaOperatorDescriptor : OperatorDescriptorBase
    {
        public OperatorDescriptorBase FilterBody { get; set; }
        public string SourceElementType { get; set; }
        public string ParameterName { get; set; }
    }
}