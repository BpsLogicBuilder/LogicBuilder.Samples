using Enrollment.Forms.View.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Enrollment.Forms.View.Input
{
    public class InputFormView : ViewBase, IValidatableObject
    {
        #region Constants
        #endregion Constants

        #region FormData
        public string Title { get; set; }
        public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
        public Dictionary<string, List<DirectiveView>> ConditionalDirectives { get; set; }
        #endregion FormData

        public ICollection<InputRowView> Rows { get; set; }

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
