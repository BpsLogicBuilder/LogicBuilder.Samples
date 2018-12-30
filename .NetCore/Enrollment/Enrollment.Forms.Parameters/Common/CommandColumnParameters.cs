using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.Parameters.Common
{
    public class CommandColumnParameters
    {
        public CommandColumnParameters()
        {
        }

        public CommandColumnParameters
            (
                [Comments("Column title.")]
                string title,

                [Comments("Column width")]
                int? width
            )
        {
            Title = title;
            Width = width;
        }

        public string Title { get; set; }
        public int? Width { get; set; }
    }
}
