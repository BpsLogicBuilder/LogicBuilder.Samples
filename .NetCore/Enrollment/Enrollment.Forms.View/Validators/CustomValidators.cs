using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.Forms.View.Validators
{
    public static class CustomValidators
    {
        public static bool fieldMatcher(object inputValue, string otherField)
        {
            //maybe pass the model object to all validator functions and use reflection to get the value of other field
            return true;
        }
    }
}
