using System.Collections.Generic;
using System;
using System.Linq;
using LogicBuilder.Attributes;

namespace Contoso.Parameters.Expressions
{
    public class MemberInitOperatorParameters : IExpressionParameter
    {
		public MemberInitOperatorParameters()
		{
		}

		public MemberInitOperatorParameters
		(
			[Comments("List of member bindings")]
			IList<MemberBindingItem> memberBindings,

			[Comments("The Select New type leave as null (uncheck) for anonymous types. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type newType = null
		)
		{
			MemberBindings = memberBindings.ToDictionary(m => m.Property, m => m.Selector);
			NewType = newType;
		}

        /*public IDictionary<string, IExpressionParameter> MemberBindings { get; set; }
         * After calling IMapper.Map() IDictionary fails on iOS MAUI only with the following exception
         * 
         * ---> System.InvalidOperationException: Collection was modified; enumeration operation may not execute.
   at System.Collections.Generic.Dictionary`2.Enumerator[[System.String, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[Contoso.Parameters.Expressions.IExpressionParameter, Contoso.Parameters, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]].MoveNext()
   at System.Linq.Expressions.Interpreter.FuncCallInstruction`2[[System.Collections.IEnumerator, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[System.Boolean, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]].Run(InterpretedFrame frame)
   at System.Linq.Expressions.Interpreter.EnterTryCatchFinallyInstruction.Run(InterpretedFrame frame)
   at System.Linq.Expressions.Interpreter.EnterTryCatchFinallyInstruction.Run(InterpretedFrame frame)
   --- End of inner exception stack trace ---
         */
        public Dictionary<string, IExpressionParameter> MemberBindings { get; set; }
        public Type NewType { get; set; }
    }
}