using Contoso.Forms.Parameters.Common;
using LogicBuilder.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Contoso.Forms.Parameters
{
    public class FormDataParameters
    {
        public FormDataParameters
        (
            [NameValue(AttributeNames.DEFAULTVALUE, "Form Title")]
            string title,

            [Comments("Input validation messages for each field.")]
            List<ValidationMessageParameters> validationMessages = null,

            [Comments("Input validation messages for each field.")]
            List<VariableDirectivesParameters> conditionalDirectives = null
        )
        {
            this.Title = title;
            this.ValidationMessages = validationMessages == null
                                        ? new Dictionary<string, Dictionary<string, string>>()
                                        : validationMessages
                                            .Select(vm => new ValidationMessageParameters
                                            {
                                                Field = vm.Field.Replace('.', '_'),
                                                Methods = vm.Methods
                                            })
                                            .ToDictionary(kvp => kvp.Field, kvp => kvp.Methods);
            this.ConditionalDirectives = conditionalDirectives == null
                                            ? new Dictionary<string, List<DirectiveParameters>>()
                                            : conditionalDirectives
                                                .Select(cd => new VariableDirectivesParameters
                                                {
                                                    Field = cd.Field.Replace('.', '_'),
                                                    ConditionalDirectives = cd.ConditionalDirectives
                                                })
                                                .ToDictionary(kvp => kvp.Field, kvp => kvp.ConditionalDirectives);
        }

        public FormDataParameters()
        {
        }

        public string Title { get; set; }
        public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
        public Dictionary<string, List<DirectiveParameters>> ConditionalDirectives { get; set; }
    }

    public class FormDataDummyConstructor
    {
        public FormDataDummyConstructor
        (
            FormDataParameters form,
            RowDataParameters row,
            ColumnDataParameters column,
            Input.InputDataParameters input
        )
        {

        }
    }
}
