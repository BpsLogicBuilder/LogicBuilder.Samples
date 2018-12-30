using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enrollment.Forms.ViewModels.Input
{
    public static class InputQuestionHelpers
    {
        public static List<BaseInputViewModel> GetAllQuestions(this InputFormViewModel form)
        {
            return form.Rows.Aggregate(new List<BaseInputViewModel>(), (list, next) =>
            {
                return next.GetAllQuestions(list);
            });
        }

        private static List<BaseInputViewModel> GetAllQuestions(this InputRowViewModel row, List<BaseInputViewModel> allQuestions)
        {
            return row.Columns.Aggregate(allQuestions, (list, next) =>
            {
                return next.GetAllQuestions(list);
            });
        }

        private static List<BaseInputViewModel> GetAllQuestions(this InputColumnViewModel column, List<BaseInputViewModel> allQuestions)
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
