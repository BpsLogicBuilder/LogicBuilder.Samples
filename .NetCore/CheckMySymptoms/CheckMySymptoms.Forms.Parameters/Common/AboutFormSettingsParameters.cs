using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Forms.Parameters.Common
{
    public class AboutFormSettingsParameters
    {
        public AboutFormSettingsParameters()
        {
        }

        public AboutFormSettingsParameters
        (
            [NameValue(AttributeNames.DEFAULTVALUE, "Title")]
            [Comments("Header field on the form")]
            string title,

            [Comments("Includes the URL to retrieve the data.")]
            RequestDetailsParameters requestDetails,

            [Comments("Defines the state of the request including the sort, filter, page and page size.")]
            DataRequestStateParameters state,

            [Comments("List of fields and form groups for display.")]
            List<DetailItemParameters> fieldSettings
        )
        {
            Title = title;
            RequestDetails = requestDetails;
            State = state;
            FieldSettings = fieldSettings;
        }

        public string Title { get; set; }
        public RequestDetailsParameters RequestDetails { get; set; }
        public DataRequestStateParameters State { get; set; }
        public List<DetailItemParameters> FieldSettings { get; set; }
    }
}
