using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CheckMySymptoms.Domain
{
    public class RulesModuleModel : BaseModelClass
    {
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("RulesModule_RulesModuleId")]
        public int RulesModuleId { get; set; }

        [Required]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("RulesModule_Name")]
        public string Name { get; set; }

        [Required]
        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("RulesModule_Application")]
        public string Application { get; set; }

        [Required]
        [ListEditorControl(ListControlType.HashSetForm)]
        [AlsoKnownAs("RulesModule_RuleSetFile")]
        public byte[] RuleSetFile { get; set; }

        [Required]
        [ListEditorControl(ListControlType.HashSetForm)]
        [AlsoKnownAs("RulesModule_ResourceSetFile")]
        public byte[] ResourceSetFile { get; set; }

        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("RulesModule_LoggedInUserId")]
        public string LoggedInUserId { get; set; }

        [VariableEditorControl(VariableControlType.SingleLineTextBox)]
        [AlsoKnownAs("RulesModule_LastUpdated")]
        public System.DateTime LastUpdated { get; set; }
    }
}
