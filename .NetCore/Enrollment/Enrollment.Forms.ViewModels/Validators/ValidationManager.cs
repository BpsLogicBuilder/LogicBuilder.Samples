using Enrollment.Forms.ViewModels.Common;
using Enrollment.Forms.ViewModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Enrollment.Forms.ViewModels.Validators
{
    public static class ValidationManager
    {
        private const string VALIDATORS = "Validators";
        private const string NUMBERVALIDATORS = "NumberValidators";
        private const string CUSTOMVALIDATORS = "CustomValidators";
        private const string REQUIRED = "required";
        private const string REQUIREDTRUE = "requiredTrue";

        public static Type GetValidationType(string className)
        {
            switch (className)
            {
                case VALIDATORS:
                    return typeof(Validators);
                case NUMBERVALIDATORS:
                    return typeof(NumberValidators);
                case CUSTOMVALIDATORS:
                    return typeof(CustomValidators);
                default:
                    throw new ArgumentException(Resources.unknownValidationClass);
            }
        }

        private static string GetMessageIndex(this string functionName)
            => functionName == REQUIREDTRUE ? REQUIRED : functionName;

        public static List<string> DoValidation(object inputValue, string variableName, FormValidationSettingViewModel validationSetting, Dictionary<string, string> messages)
        {
            List<string> errors = new List<string>();

            validationSetting.Validators.ForEach(validator =>
            {
                if (!messages.TryGetValue(validator.FunctionName.GetMessageIndex(), out string message))
                    throw new InvalidOperationException(Resources.validationFunctionMessageRequiredFormat.FormatString(validator.FunctionName, variableName));

                MethodInfo methodInfo = GetValidationType(validator.ClassName).GetMethod(validator.FunctionName);
                if (methodInfo == null)
                    throw new InvalidOperationException(Resources.validationFunctionNotFoundFormat.FormatString(validator.FunctionName, validator.ClassName));

                object[] args = GetArguments(validator);
                if (!(bool)methodInfo.Invoke(null, args))
                    errors.Add(message);
            });

            object[] GetArguments(ValidatorDescriptionViewModel validator)
            {
                return validator.Arguments == null ? new object[] { inputValue } :validator.Arguments.Aggregate(new List<object> { inputValue }, (list, next) =>
                {
                    list.Add(next.Value);
                    return list;
                }).ToArray();
            }

            return errors;
        }
    }
}
