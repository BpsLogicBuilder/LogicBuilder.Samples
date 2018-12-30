using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Contoso.Domain.Attributes
{
    class DataTypeAttributeRewriter : ValidationAttributeRewriter
    {
        public DataTypeAttributeRewriter(DataTypeAttribute dataTypeAttribute)
        {
            this.attribute = dataTypeAttribute;
        }

        #region Variables
        private DataTypeAttribute attribute;
        #endregion Variables

        #region Properties
        protected override ValidationAttribute Attribute => this.attribute;

        public override string AttributeString => string.Format(CultureInfo.CurrentCulture, "[DataType(DataType.{0}){1}]", Enum.GetName(typeof(DataType), attribute.DataType), NamedParameters);

        public string NamedParameters
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                string errorMessage = GetErrorMessage();
                if (!string.IsNullOrEmpty(errorMessage))
                    sb.Append(string.Format(CultureInfo.CurrentCulture, ", ErrorMessage = \"{0}\"", errorMessage));

                return sb.ToString();
            }
        }
        #endregion Properties
    }
}
