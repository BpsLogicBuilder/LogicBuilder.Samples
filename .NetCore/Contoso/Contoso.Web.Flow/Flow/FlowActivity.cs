using LogicBuilder.RulesDirector;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace Contoso.Web.Flow
{
    public class FlowActivity : IFlowActivity
    {
        public FlowActivity(IFlowManager flowManager)
        {
            this._flowManager = flowManager;
        }

        #region Variables
        private IFlowManager _flowManager;
        #endregion Variables

        #region Properties
        public DirectorBase Director => this._flowManager.Director;
        #endregion Properties

        #region Methods
        public string FormatString(string format, Collection<object> list) 
            => FormatString(format, list.ToArray());

        public string FormatString(string format, object[] list)
            => string.Format(CultureInfo.CurrentCulture, format, list);

        public void FlowComplete() => this._flowManager.FlowComplete();

        public void Terminate() => this._flowManager.Terminate();
        #endregion Methods
    }
}
