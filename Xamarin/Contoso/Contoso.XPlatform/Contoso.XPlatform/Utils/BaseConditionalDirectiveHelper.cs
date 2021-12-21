using AutoMapper;
using Contoso.Forms.Configuration.DataForm;
using Contoso.XPlatform.Validators;
using Contoso.XPlatform.ViewModels;
using LogicBuilder.Expressions.Utils.ExpressionBuilder.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contoso.XPlatform.Utils
{
    abstract public class BaseConditionalDirectiveHelper<TConditionBase, TModel> where TConditionBase : ConditionBase<TModel>, new()
    {
        private readonly IFormGroupSettings formGroupSettings;
        private readonly IEnumerable<IFormField> properties;
        private readonly IMapper mapper;
        private readonly List<TConditionBase> parentList;
        private readonly string parentName;
        const string PARAMETERS_KEY = "parameters";

        public BaseConditionalDirectiveHelper(IFormGroupSettings formGroupSettings, IEnumerable<IFormField> properties, IMapper mapper, List<TConditionBase> parentList = null, string parentName = null)
        {
            this.formGroupSettings = formGroupSettings;
            this.properties = properties;
            this.mapper = mapper;
            this.parentList = parentList;
            this.parentName = parentName;
        }

        public List<TConditionBase> GetConditions()
        {
            List<TConditionBase> conditions = formGroupSettings.ConditionalDirectives?.Aggregate(parentList ?? new List<TConditionBase>(), (list, kvp) =>
            {
                kvp.Value.ForEach
                (
                    descriptor =>
                    {
                        if ($"{descriptor.Definition.ClassName}`1" != typeof(TConditionBase).Name)
                            return;

                        list.Add
                        (
                            new TConditionBase
                            {
                                Field = GetFieldName(kvp.Key),
                                ParentField = this.parentName,
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

                return list;
            }) ?? new List<TConditionBase>();

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

                var helper = (BaseConditionalDirectiveHelper<TConditionBase, TModel>)Activator.CreateInstance
                (
                    this.GetType(),
                    new object[]
                    {
                        childForm,
                        properties,
                        mapper,
                        conditions,
                        GetFieldName(childForm.Field)
                    }
                );

                conditions = helper.GetConditions();
            }
        }

        string GetFieldName(string field)
                => parentName == null ? field : $"{parentName}.{field}";
    }
}
