using LogicBuilder.Attributes;

namespace CheckMySymptoms.Forms.Parameters.Common
{
    public class DirectiveParameters
    {
        public DirectiveParameters()
        {
        }

        public DirectiveParameters
        (
            [Comments("Details about the directive's function and its arguments.")]
            DirectiveDescriptionParameters directiveDescription,

            [Comments("Condition group to trigger the directive.")]
            ConditionGroupParameters conditionGroup
        )
        {
            DirectiveDescription = directiveDescription;
            ConditionGroup = conditionGroup;
        }

        public DirectiveDescriptionParameters DirectiveDescription { get; set; }
        public ConditionGroupParameters ConditionGroup { get; set; }
    }
}