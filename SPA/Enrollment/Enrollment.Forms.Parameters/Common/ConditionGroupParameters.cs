using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Enrollment.Forms.Parameters.Common
{
    public class ConditionGroupParameters
    {
        public ConditionGroupParameters()
        {
        }

        public ConditionGroupParameters
        (
            [Domain("and,or")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            string logic,

            [Comments("List of conditions.")]
            List<ConditionParameters> conditions = null,

            [Comments("Child condition groups.")]
            List<ConditionGroupParameters> conditionGroups = null
        )
        {
            Logic = logic;
            Conditions = conditions;
            ConditionGroups = conditionGroups;
        }

        public string Logic { get; set; }
        public List<ConditionParameters> Conditions { get; set; }
        public List<ConditionGroupParameters> ConditionGroups { get; set; }
    }
}