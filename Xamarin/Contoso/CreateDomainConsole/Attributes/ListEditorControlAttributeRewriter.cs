using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Contoso.Domain.Attributes
{
    class ListEditorControlAttributeRewriter : AttributeRewriter
    {
        public ListEditorControlAttributeRewriter(PropertyInfo pInfo)
        {
            this.pInfo = pInfo;
        }

        #region Variables
        private PropertyInfo pInfo;
        #endregion Variables

        public override string AttributeString => "[ListEditorControl(ListControlType.HashSetForm)]";
    }
}
