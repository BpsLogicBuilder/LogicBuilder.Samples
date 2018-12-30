using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Enrollment.Forms.Parameters.Common
{
    public class FilterGroupParameters
    {
        public FilterGroupParameters()
        {
        }

        public FilterGroupParameters
        (
            [Domain("and,or")]
            [ParameterEditorControl(ParameterControlType.DropDown)]
            [Comments("and,or")]
            string logic, 
            List<FilterDefinitionParameters> filters = null, 
            List<FilterGroupParameters> filterGroups = null
        )
        {
            Logic = logic;
            Filters = filters;
            FilterGroups = filterGroups;
        }

        public string Logic { get; set; }
        public List<FilterDefinitionParameters> Filters { get; set; }
        public List<FilterGroupParameters> FilterGroups { get; set; }
    }
}