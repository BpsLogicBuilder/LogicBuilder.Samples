using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Contoso.Domain.Attributes
{
    class RequiredAttributeRewriter : ValidationAttributeRewriter
    {
        public RequiredAttributeRewriter(RequiredAttribute requiredAttribute)
        {
            this.attribute = requiredAttribute;
        }

        #region Variables
        private RequiredAttribute attribute;
        #endregion Variables

        #region Properties
        protected override ValidationAttribute Attribute => this.attribute;

        public override string AttributeString
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture,"[Required{0}]", NamedParameters);
            }
        }

        public string NamedParameters
        {
            get
            {
                List<string> list = new List<string>();
                string errorMessage = GetErrorMessage();
                if (!string.IsNullOrEmpty(errorMessage))
                    list.Add(string.Format(CultureInfo.CurrentCulture, "ErrorMessage = \"{0}\"", errorMessage));

                if (attribute.AllowEmptyStrings == true)
                    list.Add("AllowEmptyStrings = true");

                return list.Count == 0 ? string.Empty : string.Concat("(", string.Join(", ", list), ")");
            }
        }
        #endregion Properties
    }
}
