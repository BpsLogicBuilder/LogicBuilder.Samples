using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Enrollment.Forms.View.Validators
{
    public static class Validators
    {
        public static bool min(object inputValue, object minValue)
        {
            if (inputValue == null)
                return true;

            return DoMinCheck(inputValue.GetType());
            bool DoMinCheck(Type objectType)
            {
                if (objectType == typeof(string))
                    return Min((string)inputValue, (string)minValue);

                if (objectType.IsValueType)
                {
                    MethodInfo method = typeof(Validators)
                        .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                        .Single(m => m.Name == "Min" && m.GetGenericArguments().Count() == 1);

                    //GetType always returns the underlying type not the Nullable type i.e. save to call the generic Min<T> with a nullable input value
                    return (bool)method.MakeGenericMethod(objectType).Invoke(null, new object[] { inputValue, minValue });
                }

                return false;
            }
        }

        private static bool Min<T>(T inputValue, T minValue) where T : struct, IComparable
            => !(inputValue.CompareTo(minValue) < 0);

        private static bool Min(string inputValue, string minValue)
            => !(inputValue.CompareTo(minValue) < 0);

        public static bool max(object inputValue, object maxValue)
        {
            if (inputValue == null)
                return true;

            return DoMaxCheck(inputValue.GetType());
            bool DoMaxCheck(Type objectType)
            {
                if (objectType == typeof(string))
                    return Max((string)inputValue, (string)maxValue);

                if (objectType.IsValueType)
                {
                    MethodInfo method = typeof(Validators)
                        .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                        .Single(m => m.Name == "Max" && m.GetGenericArguments().Count() == 1);

                    //GetType always returns the underlying type not the Nullable type i.e. safe to call the generic Max<T> with a nullable input value
                    return (bool)method.MakeGenericMethod(objectType).Invoke(null, new object[] { inputValue, maxValue });
                }

                return false;
            }
        }

        private static bool Max<T>(T inputValue, T maxValue) where T : struct, IComparable
            => !(inputValue.CompareTo(maxValue) > 0);

        private static bool Max(string inputValue, string maxValue)
            => !(inputValue.CompareTo(maxValue) > 0);

        public static bool email(object inputValue)
            => inputValue == null ? true : new RegexUtilities().IsValidEmail(inputValue.ToString());

        public static bool required(object inputValue)
            => inputValue != null;

        public static bool requiredTrue(object inputValue)
            => inputValue != null && inputValue is bool && (bool)inputValue;

        public static bool pattern(object inputValue, string pattern)
            => inputValue == null ? true : Regex.IsMatch(inputValue.ToString(), pattern);
    }
}
