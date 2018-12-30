using System.Collections.Generic;
using System.Linq;

namespace Contoso.Forms.View.Input
{
    public static class InputQuestionHelpers
    {
        public static List<BaseInputView> GetAllQuestions(this InputFormView form)
        {
            return form.Rows.Aggregate(new List<BaseInputView>(), (list, next) =>
            {
                return next.GetAllQuestions(list);
            });
        }

        private static List<BaseInputView> GetAllQuestions(this InputRowView row, List<BaseInputView> allQuestions)
        {
            return row.Columns.Aggregate(allQuestions, (list, next) =>
            {
                return next.GetAllQuestions(list);
            });
        }

        private static List<BaseInputView> GetAllQuestions(this InputColumnView column, List<BaseInputView> allQuestions)
        {
            allQuestions = column.Rows.Aggregate(allQuestions, (list, next) =>
            {
                return next.GetAllQuestions(list);
            });

            return column.Questions.Aggregate(allQuestions, (list, next) =>
            {
                list.Add(next);
                return list;
            });
        }
    }
}
