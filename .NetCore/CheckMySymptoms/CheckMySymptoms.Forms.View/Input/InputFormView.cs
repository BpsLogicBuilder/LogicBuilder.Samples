using CheckMySymptoms.Forms.View.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace CheckMySymptoms.Forms.View.Input
{
    public class InputFormView : ViewBase, IValidatableObject
    {
        #region Constants
        #endregion Constants

        #region FormData
        public string Title { get; set; }
        public Dictionary<string, Dictionary<string, string>> ValidationMessages { get; set; }
        public Dictionary<string, List<DirectiveView>> ConditionalDirectives { get; set; }
        public string Icon { get; set; }
        #endregion FormData

        public ICollection<InputRowView> Rows { get; set; }

        public override void UpdateFields(object fields)
        {
            if (!(fields is object[] list))
                return;

            this.UpdateAllQuestions(list.Aggregate(new Dictionary<int, object>(), (dic, next) =>
            {
                if (next is KeyValuePair<int, object> kvp)
                    dic.Add(kvp.Key, kvp.Value);

                return dic;
            }));
        }

        public KeyValuePair<int, object>[] GetFields()
        {
            return this.GetAllQuestions()
                .Select(q => new KeyValuePair<int, object>(q.Id, q.GetInputResponse()?.Answer))
                .ToArray();
        }

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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(InputFormView))
                return false;

            InputFormView other = (InputFormView)obj;

            return other.Title == this.Title
                && other.Icon == this.Icon;
        }

        public override int GetHashCode()
            => (this.Title ?? string.Empty).GetHashCode();
    }
}
