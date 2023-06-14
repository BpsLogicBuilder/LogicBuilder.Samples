using LogicBuilder.RulesDirector;

namespace Contoso.Bsl.Flow
{
    public class DirectorFactory
    {
        public DirectorFactory(IRulesCache rulesCache)
        {
            this._rulesCache = rulesCache;
        }

        #region Variables
        private IRulesCache _rulesCache;
        #endregion Variables

        public DirectorBase Create(IFlowManager flowManager)
            => new Director(flowManager, _rulesCache);
    }
}
