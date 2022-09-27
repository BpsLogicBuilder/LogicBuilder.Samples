using Enrollment.Forms.Configuration.DataForm;
using Enrollment.Forms.Configuration.Validation;
using Enrollment.XPlatform.ViewModels.Validatables;
using LogicBuilder.RulesDirector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Enrollment.XPlatform.Validators.Rules
{
    internal class ValidatorRuleFactory
    {
        public ValidatorRuleFactory(string? parentName)
        {
            this.parentName = parentName;
        }

        private readonly string? parentName;

        public IValidationRule GetValidatorRule(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, ObservableCollection<IValidatable> fields)
            => (IValidationRule)(
                (
                    typeof(ValidatorRuleFactory).GetMethod
                    (
                        "_GetValidatorRule",
                        1,
                        BindingFlags.NonPublic | BindingFlags.Instance,
                        null,
                        new Type[]
                        {
                            typeof(ValidatorDefinitionDescriptor),
                            typeof(FormControlSettingsDescriptor),
                            typeof(Dictionary<string, List<ValidationRuleDescriptor>>),
                            typeof(ObservableCollection<IValidatable>)
                        },
                        null
                    ) ?? throw new ArgumentException($"{nameof(_GetValidatorRule)}: {{9F4F97F7-E1A6-4EB0-BFFF-4A628D9646D7}}")
                )
                .MakeGenericMethod
                (
                    Type.GetType(setting.Type) ?? throw new ArgumentException($"{nameof(setting.Type)}: {{D21C8078-2BF6-4151-AECF-032E4FD4CBD2}}")
                ).Invoke
                (
                    this,
                    new object[]
                    {
                        validator,
                        setting,
                        validationMessages,
                        fields
                    }
                ) ?? throw new ArgumentException($"{nameof(setting.Type)}: {{87CAF85A-2665-41B1-BF0B-5D33A7909F62}}")
            );

        private IValidationRule _GetValidatorRule<T>(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting, Dictionary<string, List<ValidationRuleDescriptor>> validationMessages, ObservableCollection<IValidatable> fields)
        {
            if (validationMessages == null)
                throw new ArgumentException($"{nameof(validationMessages)}: C1BDA4F7-B684-438F-B5BB-B61F01B625CE");

            if (!validationMessages.TryGetValue(setting.Field, out List<ValidationRuleDescriptor>? methodList))
                throw new ArgumentException($"{nameof(setting.Field)}: 4FF12AAC-DF7F-4346-8747-52413FCA808F");

            Dictionary<string, string> methodDictionary = methodList.ToDictionary(vr => vr.ClassName, vr => vr.Message);

            if (!methodDictionary.TryGetValue(validator.ClassName, out string? validationMessage))
                throw new ArgumentException($"{nameof(validator.ClassName)}: 8A45F637-347D-4578-9F9C-72E9026FBCEB");

            if (validator.ClassName == nameof(RequiredRule<T>))
                return GetRequiredRule();
            else if (validator.ClassName == nameof(IsMatchRule<T>))
                return GetIsMatchRule();
            else if (validator.ClassName == nameof(RangeRule<int>))
                return GetRangeRule();
            else if (validator.ClassName == nameof(MustBeNumberRule<T>))
                return GetMustBeNumberRule();
            else if (validator.ClassName == nameof(MustBePositiveNumberRule<T>))
                return GetMustBePositiveNumberRule();
            else if (validator.ClassName == nameof(MustBeIntegerRule<T>))
                return GetMustBeIntegerRule();
            else if (validator.ClassName == nameof(IsLengthValidRule))
                return GetIsLengthValidRule();
            else if (validator.ClassName == nameof(IsValidEmailRule))
                return GetIsValidEmailRule();
            else if (validator.ClassName == nameof(IsValidPasswordRule))
                return GetIsValidPasswordRule();
            else if (validator.ClassName == nameof(IsPatternMatchRule))
                return GetIsPatternMatchRule();
            else if (validator.ClassName == nameof(IsValueTrueRule))
                return GetIsValueTrueRule();
            else if (validator.ClassName == nameof(AtLeastOneRequiredRule<List<string>>))
                return GetAtLeastOneRequiredRule();
            else
                throw new ArgumentException($"{nameof(validator.ClassName)}: CF4FDB4D-F135-40E0-BB31-14DBA624FC25");

            IValidationRule GetIsValueTrueRule()
                => new IsValueTrueRule
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                );

            IValidationRule GetIsValidPasswordRule()
                => new IsValidEmailRule
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                );

            IValidationRule GetIsValidEmailRule()
                => new IsValidEmailRule
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                );

            IValidationRule GetIsPatternMatchRule()
            {
                const string pattern = "pattern";
                if (validator.Arguments?.TryGetValue(pattern, out ValidatorArgumentDescriptor? patternDescriptor) != true)
                    throw new ArgumentException($"{pattern}: 086E280E-03C7-4900-A8DB-2C570CEEC91A");

                return new IsPatternMatchRule
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields,
                    (string)patternDescriptor!.Value/*patternDescriptor is not null here*/
                );
            }

            IValidationRule GetIsLengthValidRule()
            {
                const string argumentMin = "minimunLength";
                const string argumentMax = "maximunLength";
                if (!validator.Arguments.TryGetValue(argumentMin, out ValidatorArgumentDescriptor? minDescriptor))
                    throw new ArgumentException($"{argumentMin}: 521CBE54-0677-4633-AB4F-35A355490D89");
                if (!validator.Arguments.TryGetValue(argumentMax, out ValidatorArgumentDescriptor? maxDescriptor))
                    throw new ArgumentException($"{argumentMax}: EEB2EC10-42B9-49EC-A7A3-86530D11C679");

                return new IsLengthValidRule
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields,
                    (int)minDescriptor.Value,
                    (int)maxDescriptor.Value
                );
            }

            IValidationRule GetMustBePositiveNumberRule()
                => new MustBePositiveNumberRule<T>
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                );

            IValidationRule GetMustBeNumberRule()
                => new MustBeNumberRule<T>
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                );

            IValidationRule GetMustBeIntegerRule()
                => new MustBeIntegerRule<T>
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields
                );

            IValidationRule GetRequiredRule()
            {
                if (setting.ValidationSetting?.DefaultValue != null
                    && !typeof(T).AssignableFrom(setting.ValidationSetting.DefaultValue.GetType()))
                    throw new ArgumentException($"{nameof(setting.ValidationSetting.DefaultValue)}: C96394B8-B26B-45B2-8C34-B9BA3FF95088");

                return new RequiredRule<T>
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields,
                    setting.ValidationSetting?.DefaultValue == null ? default : (T)setting.ValidationSetting.DefaultValue
                );
            }

            IValidationRule GetIsMatchRule()
            {
                const string argumentName = "otherFieldName";
                if (!validator.Arguments.TryGetValue(argumentName, out ValidatorArgumentDescriptor? validatorArgumentDescriptor))
                    throw new ArgumentException($"{argumentName}: ADB88D64-F9DA-4FC0-B9C0-CB910F86B735");

                return new IsMatchRule<T>
                (
                    GetFieldName(setting.Field),
                    validationMessage,
                    fields,
                    GetFieldName((string)validatorArgumentDescriptor.Value)
                );
            }

            IValidationRule GetRangeRule()
            {
                return (IValidationRule)(
                    (
                        typeof(ValidatorRuleFactory).GetMethod
                        (
                            "GetRangeRule",
                            1,
                            BindingFlags.NonPublic | BindingFlags.Instance,
                            null,
                            new Type[]
                            {
                                typeof(ValidatorDefinitionDescriptor),
                                typeof(FormControlSettingsDescriptor),
                                typeof(string),
                                typeof(ObservableCollection<IValidatable>)
                            },
                            null
                        ) ?? throw new ArgumentException($"{setting.Type}: {{7A3707F7-CCBE-4B62-B52B-E976835ED0A3}}")
                    )
                    .MakeGenericMethod
                    (
                        Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{56E15D04-39D3-4709-A5EC-0C15C8E94AF9}}")
                    ).Invoke
                    (
                        this,
                        new object[]
                        {
                            validator,
                            setting,
                            validationMessage,
                            fields
                        }
                    ) ?? throw new ArgumentException($"{setting.Type}: {{15AB0AAF-ADBA-4EAE-B2BA-BAB385B89D58}}")
                );
            }

            IValidationRule GetAtLeastOneRequiredRule()
            {
                return (IValidationRule)(
                    (
                        typeof(ValidatorRuleFactory).GetMethod
                        (
                            "GetAtLeastOneRequiredRule",
                            1,
                            BindingFlags.NonPublic | BindingFlags.Instance,
                            null,
                            new Type[]
                            {
                                typeof(FormControlSettingsDescriptor),
                                typeof(string),
                                typeof(ObservableCollection<IValidatable>)
                            },
                            null
                        ) ?? throw new ArgumentException($"{setting.Type}: {{A52B7E1D-FAF4-48FA-A9B4-6B7043DE987D}}")
                    )
                    .MakeGenericMethod
                    (
                        Type.GetType(setting.Type) ?? throw new ArgumentException($"{setting.Type}: {{F81C42F5-B702-448C-8A9D-03C0926D7C8A}}")
                    ).Invoke
                    (
                        this,
                        new object[]
                        {
                            setting,
                            validationMessage,
                            fields
                        }
                    ) ?? throw new ArgumentException($"{setting.Type}: {{83616D86-2DF4-4E6A-8ED7-B3DE7B52077C}}")
                );
            }
        }

        private IValidationRule GetRangeRule<T>(ValidatorDefinitionDescriptor validator, FormControlSettingsDescriptor setting, string validationMessage, ObservableCollection<IValidatable> fields) where T : IComparable<T>
        {
            const string argumentMin = "min";
            const string argumentMax = "max";
            if (!validator.Arguments.TryGetValue(argumentMin, out ValidatorArgumentDescriptor? minDescriptor))
                throw new ArgumentException($"{argumentMin}: 34965468-76E0-4FA0-A3EC-16F2BCCB2CE0");
            if (!validator.Arguments.TryGetValue(argumentMax, out ValidatorArgumentDescriptor? maxDescriptor))
                throw new ArgumentException($"{argumentMax}: 6AA3A056-3ECA-4F48-B79A-A326B2188D14");

            return new RangeRule<T>
            (
                GetFieldName(setting.Field),
                validationMessage,
                fields,
                (T)minDescriptor.Value,
                (T)maxDescriptor.Value
            );
        }

        private IValidationRule GetAtLeastOneRequiredRule<T>(FormControlSettingsDescriptor setting, string validationMessage, ObservableCollection<IValidatable> fields) where T : IEnumerable<object>
        {
            return (IValidationRule)(
                Activator.CreateInstance
                (
                    typeof(AtLeastOneRequiredRule<>).MakeGenericType
                    (
                        typeof(ObservableCollection<>).MakeGenericType
                        (
                            typeof(T).GetGenericArguments()[0]
                        )
                    ),
                    new object[]
                    {
                        GetFieldName(setting.Field),
                        validationMessage,
                        fields
                    }
                ) ?? throw new ArgumentException($"{nameof(AtLeastOneRequiredRule<IEnumerable<object>>)}: {{824A0081-0E2F-4929-8BF2-C1F65D54AC6D}}")
            );
        }

        string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";
    }
}
