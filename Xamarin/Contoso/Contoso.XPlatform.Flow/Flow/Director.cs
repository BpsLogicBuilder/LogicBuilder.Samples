using Contoso.XPlatform.Flow.Settings;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.XPlatform.Flow
{
    public class Director : AppDirectorBase
    {
        public Director(IFlowManager flowManager, IRulesCache rulesCache)
        {
            this._flowManager = flowManager;
            this._rulesCache = rulesCache;
        }

        #region Fields
        private readonly IFlowManager _flowManager;
        private readonly IRulesCache _rulesCache;
        #endregion Fields

        #region Properties
        protected override Progress Progress => this._flowManager.Progress;
        protected override IRulesCache RulesCache => this._rulesCache;
        protected override IFlowActivity FlowActivity => this._flowManager.FlowActivity;

        public FlowState FlowState
        {
            get
            {
                return new FlowState
                {
                    Driver = this._driver,
                    Selection = this._selection,
                    CallingModuleDriverStack = new List<string>(this._callingModuleDriverStack.OfType<string>()),
                    CallingModuleStack = new List<string>(this._callingModuleStack.OfType<string>()),
                    ModuleBeginName = this._moduleBeginName,
                    ModuleEndName = this._moduleEndName
                };
            }
            set
            {
                this._driver = value.Driver;
                this._selection = value.Selection;
                this._callingModuleDriverStack = new System.Collections.Stack(value.CallingModuleDriverStack.Reverse<string>().ToList());
                this._callingModuleStack = new System.Collections.Stack(value.CallingModuleStack.Reverse<string>().ToList());
                this._moduleBeginName = value.ModuleBeginName;
                this._moduleEndName = value.ModuleEndName;
            }
        }
        #endregion Properties

        #region Methods
        public override void SetCurrentBusinessBackupData() => this._flowManager.SetCurrentBusinessBackupData();
        #endregion Methods
    }
}
