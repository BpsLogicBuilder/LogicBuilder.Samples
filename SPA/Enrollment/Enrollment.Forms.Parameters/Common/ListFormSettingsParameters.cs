using Enrollment.Parameters.Expressions;
using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Enrollment.Forms.Parameters.Common
{
    public class ListFormSettingsParameters
    {
        public ListFormSettingsParameters()
        {
        }

        public ListFormSettingsParameters
        (
            [NameValue(AttributeNames.DEFAULTVALUE, "Title")]
            [Comments("Header field on the form")]
            string title,

            [Comments("Defines the LINQ query for retrieving the list.")]
            SelectorLambdaOperatorParameters fieldsSelector,

            [Comments("Includes the URL to retrieve the data.")]
            RequestDetailsParameters requestDetails,

            [Comments("List of fields and form groups for display.")]
            List<DetailItemParameters> fieldSettings
        )
        {
            Title = title;
            FieldsSelector = fieldsSelector;
            RequestDetails = requestDetails;
            FieldSettings = fieldSettings;
        }

        public string Title { get; set; }
        public RequestDetailsParameters RequestDetails { get; set; }
        public SelectorLambdaOperatorParameters FieldsSelector { get; set; }
        public List<DetailItemParameters> FieldSettings { get; set; }
    }
}
