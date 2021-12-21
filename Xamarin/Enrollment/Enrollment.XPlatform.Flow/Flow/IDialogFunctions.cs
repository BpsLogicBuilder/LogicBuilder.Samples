using Enrollment.Forms.Parameters.DataForm;
using Enrollment.Forms.Parameters.ListForm;
using Enrollment.Forms.Parameters.SearchForm;
using Enrollment.Forms.Parameters.TextForm;
using LogicBuilder.Attributes;
using LogicBuilder.Forms.Parameters;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Flow
{
    public interface IDialogFunctions
    {
        [AlsoKnownAs("DisplayEditCollection")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplayEditCollection
        (
            [Comments("Configuration details for the form.")]
            SearchFormSettingsParameters setting,
            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );

        [AlsoKnownAs("DisplayEditForm")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplayEditForm
        (
            [Comments("Configuration details for the form.")]
            DataFormSettingsParameters setting,
            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );

        [AlsoKnownAs("DisplayDetailForm")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplayDetailForm
        (
            [Comments("Configuration details for the form.")]
            DataFormSettingsParameters setting,
            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );

        [AlsoKnownAs("DisplayReadOnlyCollection")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplayReadOnlyCollection
        (
            [Comments("Configuration details for the form.")]
            ListFormSettingsParameters setting,
            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );

        [AlsoKnownAs("DisplayTextForm")]
        [FunctionGroup(FunctionGroup.DialogForm)]
        void DisplayTextForm
        (
            [Comments("Configuration details for the form.")]
            TextFormSettingsParameters setting,
            [ListEditorControl(ListControlType.Connectors)]
            ICollection<ConnectorParameters> buttons
        );
    }
}
