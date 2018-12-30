using Contoso.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Contoso.Domain.Attributes
{
    class VariableEditorControlAttributeRewriter : AttributeRewriter
    {
        public VariableEditorControlAttributeRewriter(PropertyInfo pInfo)
        {
            this.pInfo = pInfo;
        }

        #region Variables
        private PropertyInfo pInfo;
        #endregion Variables

        public override string AttributeString => "[VariableEditorControl(VariableControlType.SingleLineTextBox)]";
    }
}
