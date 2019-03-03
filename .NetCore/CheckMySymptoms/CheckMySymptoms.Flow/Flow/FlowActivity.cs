using LogicBuilder.Forms.Parameters;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace CheckMySymptoms.Flow
{
    public class FlowActivity : IFlowActivity
    {
        public FlowActivity(IFlowManager flowManager)
        {
            this._flowManager = flowManager;
        }

        #region Variables
        private readonly IFlowManager _flowManager;
        #endregion Variables

        #region Properties
        public DirectorBase Director => this._flowManager.Director;
        #endregion Properties

        #region Methods
        public string FormatString(string format, Collection<object> list)
            => string.Format(CultureInfo.CurrentCulture, format, list.ToArray());

        public void FlowComplete() => this._flowManager.FlowComplete();

        public void Terminate() => this._flowManager.Terminate();

        public void Wait() => this._flowManager.Wait();

        public void DisplayQuestions(QuestionFormParameters form, ICollection<ConnectorParameters> connectors = null)
            => throw new NotImplementedException();

        public void DisplayInputQuestions(InputFormParameters form, ICollection<ConnectorParameters> connectors = null)
            => this._flowManager.DisplayInputQuestions(form, connectors);
        #endregion Methods
    }
}
