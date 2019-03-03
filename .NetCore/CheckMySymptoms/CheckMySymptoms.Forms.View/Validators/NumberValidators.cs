using System;
using System.Linq;
using System.Reflection;

namespace CheckMySymptoms.Forms.View.Validators
{
    public static class NumberValidators
    {
        public static bool range(object inputValue, object minValue, object maxValue)
        {
            if (inputValue == null)
                return true;

            return DoRangeCheck(inputValue.GetType());
            bool DoRangeCheck(Type objectType)
            {
                if (objectType == typeof(string))
                    return IsInRange((string)inputValue, (string)minValue, (string)maxValue);

                if (objectType.IsValueType)
                {
                    MethodInfo method = typeof(NumberValidators)
                        .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                        .Single(m => m.Name == "IsInRange" && m.GetGenericArguments().Count() == 1);

                    //GetType always returns the underlying type not the Nullable type i.e. save to call the generic IsInRange<T> with a nullable input value
                    return (bool)method.MakeGenericMethod(objectType).Invoke(null, new object[] { inputValue, minValue, maxValue });
                }

                return false;
            }
        }

        private static bool IsInRange<T>(T inputValue, T minValue, T maxValue) where T : struct, IComparable
            => !(inputValue.CompareTo(minValue) < 0 || inputValue.CompareTo(maxValue) > 0);

        private static bool IsInRange(string inputValue, string minValue, string maxValue)
            => !(inputValue.CompareTo(minValue) < 0 || inputValue.CompareTo(maxValue) > 0);

        public static bool mustBeANumber(object inputValue)
            => inputValue == null
                ? true
                : decimal.TryParse(inputValue.ToString(), out decimal dec)
                    || double.TryParse(inputValue.ToString(), out double doub);
    }
}
