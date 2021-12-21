using Contoso.Forms.Parameters.DataForm;
using Contoso.Forms.Parameters.ListForm;
using Contoso.Forms.Parameters.SearchForm;
using Contoso.Forms.Parameters.TextForm;
using LogicBuilder.Attributes;
using LogicBuilder.Forms.Parameters;
using System.Collections.Generic;

namespace Contoso.XPlatform.Flow
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
