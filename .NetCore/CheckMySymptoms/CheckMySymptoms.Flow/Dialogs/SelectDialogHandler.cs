using System;
using System.Collections.Generic;
using System.Text;
using CheckMySymptoms.Flow.Requests;
using LogicBuilder.RulesDirector;

namespace CheckMySymptoms.Flow.Dialogs
{
    public class SelectDialogHandler : BaseDialogHandler, IDialogHandler
    {
        public SelectDialogHandler(RequestBase request) : base(request)
        {
        }

        public override void Complete(IFlowManager flowManager)
        {
            if (!this.Request.CommandButtonRequest.Cancel)
            {
                if (((SelectRequest)this.Request).AddToSymptoms)
                {                  
                    flowManager.CustomActions.AddSymptom
                    (
                        ((SelectRequest)this.Request).CommandButtonRequest.SymtomText
                    );
                }
            }

            base.Complete(flowManager);
        }

        object IDialogHandler.FieldValues => ((SelectRequest)this.Request).MessageTemplateView.Selection;
    }
}
