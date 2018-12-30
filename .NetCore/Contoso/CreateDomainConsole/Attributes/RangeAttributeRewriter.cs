using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Contoso.Domain.Attributes
{
    class RangeAttributeRewriter : ValidationAttributeRewriter
    {
        public RangeAttributeRewriter(RangeAttribute rangeAttribute)
        {
            this.attribute = rangeAttribute;
        }

        #region Variables
        private RangeAttribute attribute;
        #endregion Variables

        #region Properties
        protected override ValidationAttribute Attribute => this.attribute;

        public override string AttributeString
        {
            get
            {
                return attribute.OperandType == typeof(double) || attribute.OperandType == typeof(int)
                    ? string.Format
                                (
                                    CultureInfo.CurrentCulture,
                                    "[Range({0}, {1}{2})]",
                                    ((IConvertible)attribute.Minimum).ToString(CultureInfo.CurrentCulture),
                                    ((IConvertible)attribute.Maximum).ToString(CultureInfo.CurrentCulture),
                                    NamedParameters
                                )
                    : string.Format
                                (
                                    CultureInfo.CurrentCulture,
                                    "[Range(typeof({0}), \"{1}\", \"{2}\"{3})]",
                                    attribute.OperandType.FullName,
                                    attribute.Minimum.ToString(),
                                    attribute.Maximum.ToString(), 
                                    NamedParameters
                                );
            }
        }

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
