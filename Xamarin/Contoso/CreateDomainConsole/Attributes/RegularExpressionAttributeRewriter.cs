using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Contoso.Domain.Attributes
{
    class RegularExpressionAttributeRewriter : ValidationAttributeRewriter
    {
        public RegularExpressionAttributeRewriter(RegularExpressionAttribute regularExpressionAttribute)
        {
            this.attribute = regularExpressionAttribute;
        }

        #region Variables
        private RegularExpressionAttribute attribute;
        #endregion Variables

        #region Properties
        protected override ValidationAttribute Attribute => this.attribute;

        public override string AttributeString => string.Format(CultureInfo.CurrentCulture, "[RegularExpression(@\"{0}\"{1})]", attribute.Pattern, NamedParameters);

        public string NamedParameters
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                string errorMessage = GetErrorMessage();
                if (!string.IsNullOrEmpty(errorMessage))
                    sb.Append(string.Format(CultureInfo.CurrentCulture, ", ErrorMessage = \"{0}\"", errorMessage));

                if (attribute.MatchTimeoutInMilliseconds != default(int))
                    sb.Append(string.Concat(", MatchTimeoutInMilliseconds = ", attribute.MatchTimeoutInMilliseconds.ToString(CultureInfo.CurrentCulture)));

                return sb.ToString();
            }
        }
        #endregion Properties
    }
}
