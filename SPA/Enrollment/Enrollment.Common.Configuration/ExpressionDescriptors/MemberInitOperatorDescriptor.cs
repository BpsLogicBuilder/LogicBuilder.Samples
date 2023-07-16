using System;
using System.Collections.Generic;

namespace Enrollment.Common.Configuration.ExpressionDescriptors
{
    public class MemberInitOperatorDescriptor : OperatorDescriptorBase
    {
        /*public IDictionary<string, OperatorDescriptorBase> MemberBindings { get; set; }*
         * After calling IMapper.Map() IDictionary fails on iOS Maui only with the following exception
         * 
         * ---> System.InvalidOperationException: Collection was modified; enumeration operation may not execute.
    at System.Collections.Generic.Dictionary`2.Enumerator[[System.String, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[Enrollment.Parameters.Expressions.IExpressionParameter, Contoso.Parameters, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]].MoveNext()
    at System.Linq.Expressions.Interpreter.FuncCallInstruction`2[[System.Collections.IEnumerator, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Boolean, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].Run(InterpretedFrame frame)
    at System.Linq.Expressions.Interpreter.EnterTryCatchFinallyInstruction.Run(InterpretedFrame frame)
    at System.Linq.Expressions.Interpreter.EnterTryCatchFinallyInstruction.Run(InterpretedFrame frame)
    --- End of inner exception stack trace ---
         */
        public Dictionary<string, OperatorDescriptorBase> MemberBindings { get; set; }
        public string NewType { get; set; }
    }
}