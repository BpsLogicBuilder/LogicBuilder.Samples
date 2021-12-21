using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Contoso.Domain.Attributes
{
    class DisplayAttributeRewriter : AttributeRewriter
    {
        public DisplayAttributeRewriter(DisplayAttribute rangeAttribute)
        {
            this.attribute = rangeAttribute;
        }

        #region Variables
        private DisplayAttribute attribute;
        #endregion Variables

        #region Properties
        public override string AttributeString
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture, "[Display{0}]", NamedParameters);
            }
        }

        public string NamedParameters
        {
            get
            {
                List<string> list = new List<string>();
                if (!string.IsNullOrEmpty(attribute.GetPrompt()))
                    list.Add(string.Format(CultureInfo.CurrentCulture, "Prompt = \"{0}\"", attribute.GetPrompt()));

                if (attribute.GetOrder().HasValue)
                    list.Add(string.Format(CultureInfo.CurrentCulture, "Order = {0}", attribute.GetOrder()));

                if (!string.IsNullOrEmpty(attribute.GetName()))
                    list.Add(string.Format(CultureInfo.CurrentCulture, "Name = \"{0}\"", attribute.GetName()));

                if (!string.IsNullOrEmpty(attribute.GetGroupName()))
                    list.Add(string.Format(CultureInfo.CurrentCulture, "GroupName = \"{0}\"", attribute.GetGroupName()));

                if (!string.IsNullOrEmpty(attribute.GetDescription()))
                    list.Add(string.Format(CultureInfo.CurrentCulture, "Description = \"{0}\"", attribute.GetDescription()));

                if (attribute.GetAutoGenerateFilter().HasValue)
                    list.Add(string.Format(CultureInfo.CurrentCulture, "AutoGenerateFilter = {0}", attribute.GetAutoGenerateFilter()));

                if (attribute.GetAutoGenerateField().HasValue)
                    list.Add(string.Format(CultureInfo.CurrentCulture, "AutoGenerateField = {0}", attribute.GetAutoGenerateField()));

                if (!string.IsNullOrEmpty(attribute.GetShortName()) && attribute.GetShortName() != attribute.GetName())
                    list.Add(string.Format(CultureInfo.CurrentCulture, "ShortName = \"{0}\"", attribute.GetShortName()));

                return list.Count == 0 ? string.Empty : string.Concat("(", string.Join(", ", list), ")");
            }
        }
        #endregion Properties
    }
}
