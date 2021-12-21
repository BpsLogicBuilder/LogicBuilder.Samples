using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Contoso.Domain.Attributes
{
    class DisplayFormatAttributeRewriter : AttributeRewriter
    {
        public DisplayFormatAttributeRewriter(DisplayFormatAttribute displayFormatAttribute)
        {
            this.attribute = displayFormatAttribute;
        }

        #region Variables
        private DisplayFormatAttribute attribute;
        #endregion Variables

        #region Properties
        public override string AttributeString
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture, "[DisplayFormat{0}]", NamedParameters);
            }
        }

        public string NamedParameters
        {
            get
            {
                List<string> list = new List<string>();
                if (!string.IsNullOrEmpty(attribute.DataFormatString))
                    list.Add(string.Format(CultureInfo.CurrentCulture, "DataFormatString  = \"{0}\"", attribute.DataFormatString));

                if (!string.IsNullOrEmpty(attribute.NullDisplayText))
                    list.Add(string.Format(CultureInfo.CurrentCulture, "NullDisplayText = \"{0}\"", attribute.NullDisplayText));


                if (attribute.ConvertEmptyStringToNull == false)
                    list.Add("ConvertEmptyStringToNull = false");

                if (attribute.ApplyFormatInEditMode == true)
                    list.Add("ApplyFormatInEditMode = true");

                if (attribute.HtmlEncode == false)
                    list.Add("HtmlEncode = false");

                return list.Count == 0 ? string.Empty : string.Concat("(", string.Join(", ", list), ")");
            }
        }
        #endregion Properties
    }
}
