using CheckMySymptoms.Forms.Parameters.Common;
using LogicBuilder.Attributes;
using LogicBuilder.Forms.Parameters;
using System.Collections.Generic;

namespace CheckMySymptoms.Flow
{
    public interface ICustomDialogs
    {
        [AlsoKnownAs("DisplayHtmlContent")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplayHtmlForm
        (
            [Comments("Configuration details for the form.")]
            HtmlPageSettingsParameters setting,

            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );

        [AlsoKnownAs("DisplaySelectForm")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplaySelectForm
        (
            [Comments("Configuration details for the form.")]
            MessageTemplateParameters setting,

            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );

        [AlsoKnownAs("DisplayFlowComplete")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplayFlowComplete
        (
            [Comments("Configuration details for the form.")]
            FlowCompleteParameters setting,

            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );
    }
}
