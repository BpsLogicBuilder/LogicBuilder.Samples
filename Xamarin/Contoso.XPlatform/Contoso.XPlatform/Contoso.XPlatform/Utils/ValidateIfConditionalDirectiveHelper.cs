using AutoMapper;
using Contoso.Forms.Configuration.Directives;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels.Validatables;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Contoso.XPlatform.Utils
{
    public class ValidateIfConditionalDirectiveHelper<TModel>
    {
        private readonly IMapper mapper;
        private readonly string parentName;
        private readonly List<ValidateIf<TModel>> parentList;
        private readonly IFormGroupSettings formGroupSettings;
        private readonly IEnumerable<IValidatable> properties;
        const string PARAMETERS_KEY = "parameters";

        public ValidateIfConditionalDirectiveHelper(IFormGroupSettings formGroupSettings, IEnumerable<IValidatable> properties, IMapper mapper, List<ValidateIf<TModel>> parentList = null, string parentName = null)
        {
            this.mapper = mapper;
            this.parentList = parentList;
            this.parentName = parentName;
            this.formGroupSettings = formGroupSettings;
            this.properties = properties;
        }

        public List<ValidateIf<TModel>> GetConditions()
        {
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(p => p.Name);

            List<ValidateIf<TModel>> conditions = formGroupSettings.ConditionalDirectives?.Aggregate(parentList ?? new List<ValidateIf<TModel>>(), (list, kvp) =>
            {
                kvp.Value.ForEach
                (
                    descriptor =>
                    {
                        if (descriptor.Definition.ClassName != nameof(ValidateIf<TModel>))
                            return;

                        const string validationsToWatchKey = "validationsToWatch";
                        if (!descriptor.Definition.Arguments.TryGetValue(validationsToWatchKey, out DirectiveArgumentDescriptor validationsToWatchDescriptor))
                            throw new ArgumentException($"{validationsToWatchKey}: A5B39033-62C9-4B5D-A800-754054169502");
                        if (!typeof(IEnumerable<string>).IsAssignableFrom(validationsToWatchDescriptor.Value.GetType()))
                            throw new ArgumentException($"{validationsToWatchDescriptor}: 60E64249-A275-4992-8444-9DC85DF108B9");

                        HashSet<string> validationsToWatch = new HashSet<string>((IEnumerable<string>)validationsToWatchDescriptor.Value);

                        IValidatable validatable = propertiesDictionary[GetFieldName(kvp.Key)];

                        validatable.Validations.ForEach
                        (
                            validationRule =>
                            {
                                if (validationsToWatch.Contains(validationRule.ClassName) == false)
                                    return;

                                list.Add
                                (
                                    new ValidateIf<TModel>
                                    {
                                        Field = GetFieldName(kvp.Key),
                                        ParentField = this.parentName,
                                        Validator = validationRule,
                                        Evaluator = (Expression<Func<TModel, bool>>)mapper.Map<FilterLambdaOperator>
                                        (
                                            descriptor.Condition,
                                            opts => opts.Items[PARAMETERS_KEY] = new Dictionary<string, ParameterExpression>()
                                        ).Build(),
                                        DirectiveDefinition = descriptor.Definition
                                    }
                                );
                            }
                        );
                    }
                );

                return list;
            }) ?? new List<ValidateIf<TModel>>();

            formGroupSettings.FieldSettings.ForEach(AddConditions);

            return conditions;

            void AddConditions(FormItemSettingsDescriptor descriptor)
            {
                if (descriptor is FormGroupBoxSettingsDescriptor groupBox)
                {
                    groupBox.FieldSettings.ForEach(AddConditions);
                    return;
                }

                if (!(descriptor is FormGroupSettingsDescriptor childForm))
                    return;

                if ((childForm.FormGroupTemplate?.TemplateName) != FromGroupTemplateNames.InlineFormGroupTemplate)
                    return;

                conditions = new ValidateIfConditionalDirectiveHelper<TModel>
                (
                    childForm,
                    properties,
                    mapper,
                    conditions,
                    GetFieldName(childForm.Field)
                ).GetConditions();
            }
        }

        string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";
    }
}
