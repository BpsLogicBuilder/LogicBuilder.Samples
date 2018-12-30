using Enrollment.Web.Flow.ScreenSettings;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enrollment.Web.Flow
{
    public class Director : AppDirectorBase
    {
        public Director(IFlowManager flowManager, IRulesCache rulesCache)
        {
            this._flowManager = flowManager;
            this._rulesCache = rulesCache;
        }

        #region Constants
        #endregion Constants

        #region Variables
        private readonly IFlowManager _flowManager;
        private readonly IRulesCache _rulesCache;
        #endregion Variables

        #region Properties
        protected override Dictionary<int, int> QuestionListAnswers => throw new NotImplementedException();
        protected override Dictionary<int, object> InputQuestionsAnswers => this._flowManager.InputQuestionsAnswers;
        protected override Variables Variables => this._flowManager.Variables;

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
                    ModuleEndName = this._moduleEndName,
                    DialogClosed = this._dialogClosed
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
                this._dialogClosed = value.DialogClosed;
            }
        }
        #endregion Properties

        #region Methods
        public override void SetCurrentBusinessBackupData() => this._flowManager.SetCurrentBusinessBackupData();
        #endregion Methods
    }
}
