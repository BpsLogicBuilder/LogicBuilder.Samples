using System;

namespace Enrollment.Data.Entities
{
    public class LookUps : BaseDataClass
    {
        public int LookUpsID { get; set; }

        public string Text { get; set; }

        public string ListName { get; set; }

        public string Value { get; set; }

        public double? NumericValue { get; set; }

        public bool? BooleanValue { get; set; }

        public DateTime? DateTimeValue { get; set; }

        public char? CharValue { get; set; }

        public Guid? GuidValue { get; set; }

        public TimeSpan? TimeSpanValue { get; set; }

        public int Order { get; set; }
    }
}
