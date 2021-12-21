using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Contoso.Domain.Attributes
{
    class StringLengthAttributeRewriter : ValidationAttributeRewriter
    {
        public StringLengthAttributeRewriter(StringLengthAttribute stringAttribute)
        {
            this.attribute = stringAttribute;
        }

        #region Variables
        private StringLengthAttribute attribute;
        #endregion Variables

        #region Properties
        protected override ValidationAttribute Attribute => this.attribute;

        public override string AttributeString => string.Format(CultureInfo.CurrentCulture, "[StringLength({0})]", string.Concat(attribute.MaximumLength.ToString(CultureInfo.CurrentCulture), NamedParameters));

        public string NamedParameters
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (attribute.MinimumLength != default(int))
                    sb.Append(string.Concat(", MinimumLength = ", attribute.MinimumLength.ToString(CultureInfo.CurrentCulture)));

                string errorMessage = GetErrorMessage();
                if (!string.IsNullOrEmpty(errorMessage))
                    sb.Append(string.Format(CultureInfo.CurrentCulture, ", ErrorMessage = \"{0}\"", errorMessage));

                return sb.ToString();
            }
        } 
        #endregion Properties
    }
}
