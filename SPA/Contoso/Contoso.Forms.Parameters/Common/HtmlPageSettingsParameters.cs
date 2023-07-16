using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Forms.Parameters.Common
{
    public class HtmlPageSettingsParameters
    {
        public HtmlPageSettingsParameters()
        {
        }

        public HtmlPageSettingsParameters
        (
            [Comments("Template when the page will be used for markup.")]
            ContentTemplateParameters contentTemplate = null,

            [Comments("Template when the page will be used to display a message or ask a question.")]
            MessageTemplateParameters messageTemplate = null
        )
        {
            ContentTemplate = contentTemplate;
            MessageTemplate = messageTemplate;
        }

        public ContentTemplateParameters ContentTemplate { get; set; }
        public MessageTemplateParameters MessageTemplate { get; set; }
    }
}
