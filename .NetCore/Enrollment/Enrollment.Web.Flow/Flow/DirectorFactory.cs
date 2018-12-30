using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Web.Flow
{
    public class DirectorFactory
    {
        public DirectorFactory(IRulesCache rulesCache)
        {
            this._rulesCache = rulesCache;
        }

        #region Variables
        private readonly IRulesCache _rulesCache;
        #endregion Variables

        public DirectorBase Create(IFlowManager flowManager)
            => new Director(flowManager, _rulesCache);
    }
}
