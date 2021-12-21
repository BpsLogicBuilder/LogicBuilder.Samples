using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contoso.Domain.Attributes
{
    abstract class ValidationAttributeRewriter : AttributeRewriter
    {
        #region Properties
        abstract protected ValidationAttribute Attribute { get; }
        #endregion Properties

        #region Methods
        protected string GetErrorMessage()
        {
            if (this.Attribute == null)
                return string.Empty;

            string message;
            if (!string.IsNullOrEmpty(this.Attribute.ErrorMessageResourceName) && this.Attribute.ErrorMessageResourceType != null)
            {
                System.Resources.ResourceManager temp = new System.Resources.ResourceManager(this.Attribute.ErrorMessageResourceType);
                message = temp.GetString(this.Attribute.ErrorMessageResourceName);
            }
            else
            {
                message = this.Attribute.ErrorMessage;
            }

            return message;
        }
        #endregion Methods
    }
}
