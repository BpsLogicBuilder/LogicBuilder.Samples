using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Contoso.Domain.Attributes
{
    class AlsoKnownAsAttributeRewriter : AttributeRewriter
    {
        public AlsoKnownAsAttributeRewriter(PropertyInfo pInfo)
        {
            this.pInfo = pInfo;
        }
        #region Variables
        private PropertyInfo pInfo;
        #endregion Variables

        public override string AttributeString => string.Format(CultureInfo.CurrentCulture, "[AlsoKnownAs(\"{0}_{1}\")]", pInfo.DeclaringType.Name, pInfo.Name);
    }
}
