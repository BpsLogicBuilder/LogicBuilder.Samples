using Enrollment.Forms.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Enrollment.Forms.ViewModels.Input
{
    public class InputFormViewModel : ViewModelBase, IValidatableObject
    {
        #region Constants
        #endregion Constants

        #region FormData
        public string Title { get; set; }
        public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
        public Dictionary<string, List<DirectiveViewModel>> ConditionalDirectives { get; set; }
        #endregion FormData

        public ICollection<InputRowViewModel> Rows { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return this.GetAllQuestions().Aggregate(new List<ValidationResult>(), (resultList, next) =>
            {
                next.Validate(this.ValidationMessages);
                if (next.HasErrors)
                {
                    resultList.AddRange
                    (
                        next
                        .Errors
                        .Select(err => new ValidationResult(err, new string[] { next.VariableId }))
                    );
                }

                return resultList;
            });
        }
    }
}
