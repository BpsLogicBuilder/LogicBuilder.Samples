using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
{
    public class MessageTemplateParameters
    {
        public MessageTemplateParameters()
        {
        }

        public MessageTemplateParameters
        (
            [NameValue(AttributeNames.DEFAULTVALUE, "Caption")]
            [Comments("Caption for the message.")]
            string caption,

            [NameValue(AttributeNames.DEFAULTVALUE, "Message Text")]
            [Comments("The message text.")]
            string message,

            [NameValue(AttributeNames.DEFAULTVALUE, "messageTemplate")]
            [Comments("HTML template for the control's layout.")]
            string templateName
        )
        {
            Caption = caption;
            Message = message;
            TemplateName = templateName;
        }

        public string Caption { get; set; }
        public string Message { get; set; }
        public string TemplateName { get; set; }
    }
}
