using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckMySymptoms.Forms.View.Input
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

        public static void UpdateAllQuestions(this InputFormView form, Dictionary<int, object> values)
        {
            foreach (InputRowView row in form.Rows)
                row.UpdateAllQuestions(values);
        }

        private static void UpdateAllQuestions(this InputRowView row, Dictionary<int, object> values)
        {
            foreach (InputColumnView column in row.Columns)
                column.UpdateAllQuestions(values);
        }

        private static void UpdateAllQuestions(this InputColumnView column, Dictionary<int, object> values)
        {
            foreach (InputRowView row in column.Rows)
                row.UpdateAllQuestions(values);

            foreach (BaseInputView question in column.Questions)
            {
                if (values.TryGetValue(question.Id, out object @value))
                    question.UpdateValue(@value);
            }
        }
    }
}
