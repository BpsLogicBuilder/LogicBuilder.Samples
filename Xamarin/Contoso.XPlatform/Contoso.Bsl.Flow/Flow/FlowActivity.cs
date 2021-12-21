using LogicBuilder.Expressions.Utils.ExpressionDescriptors;
using LogicBuilder.Forms.Parameters;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Contoso.Bsl.Flow
{
    public class FlowActivity : IFlowActivity
    {
        public FlowActivity(IFlowManager flowManager)
        {
            this.flowManager = flowManager;
        }

        #region Fields
        private readonly IFlowManager flowManager;
        #endregion Fields

        #region Properties
        public DirectorBase Director => this.flowManager.Director;
        #endregion Properties

        #region Methods
        public string FormatString(string format, Collection<object> list)
            => FormatString(format, list.ToArray());

        public string FormatString(string format, object[] list) 
            => string.Format(CultureInfo.CurrentCulture, format, list);

        public void FlowComplete() => this.flowManager.FlowComplete();

        public void Terminate() => this.flowManager.Terminate();
        #endregion Methods
    }
}
