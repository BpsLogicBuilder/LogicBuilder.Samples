using AutoMapper;
using Contoso.Forms.Configuration.Directives;
using Contoso.XPlatform.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contoso.XPlatform.Validators
{
    internal class ClearIfManager<TModel> : IDisposable
    {
        public ClearIfManager(IEnumerable<IFormField> currentProperties, List<ClearIf<TModel>> conditions, IMapper mapper, UiNotificationService uiNotificationService)
        {
            CurrentProperties = currentProperties;
            this.conditions = conditions;
            this.mapper = mapper;
            this.uiNotificationService = uiNotificationService;
            propertyChangedSubscription = this.uiNotificationService.ValueChanged.Subscribe(PropertyChanged);
        }

        private readonly IMapper mapper;
        private readonly List<ClearIf<TModel>> conditions;
        private readonly UiNotificationService uiNotificationService;
        private readonly IDisposable propertyChangedSubscription;

        public IEnumerable<IFormField> CurrentProperties { get; }
        private IDictionary<string, IFormField> CurrentPropertiesDictionary
            => CurrentProperties.ToDictionary(p => p.Name);

        public void Check(ClearIf<TModel> condition)
        {
            DoCheck(CurrentPropertiesDictionary[condition.Field]);

            void DoCheck(IFormField currentField)
            {
                if
                (
                    ShouldClear
                    (
                        mapper.Map<TModel>(CurrentProperties.ToDictionary(p => p.Name, p => p.Value)),
                        condition.Evaluator
                    )
                )
                {
                    currentField.Clear();
                }
            }

            bool ShouldClear(TModel entity, Expression<Func<TModel, bool>> evaluator)
                => new List<TModel> { entity }.AsQueryable().All(evaluator);
        }

        public void Dispose()
        {
            DisposeSubscription(propertyChangedSubscription);
        }

        private void PropertyChanged(string fieldName)
        {
            conditions.ForEach
            (
                condition =>
                {
                    if (condition.DirectiveDefinition.FunctionName == nameof(ClearIfManager<TModel>.Check))
                    {
                        if (condition.DirectiveDefinition.Arguments?.Any() != true)
                            throw new ArgumentException($"{condition.DirectiveDefinition.Arguments}: 9D4514A4-AE19-432B-B419-7BB43111EC41");

                        const string fieldsToWatch = "fieldsToWatch";
                        if (!condition.DirectiveDefinition.Arguments.TryGetValue(fieldsToWatch, out DirectiveArgumentDescriptor fieldsToWatchDescriptor))
                            throw new ArgumentException($"{fieldsToWatch}: 6534ABBA-9599-43F8-BA64-2FDD1205C855");
                        if (!typeof(IEnumerable<string>).IsAssignableFrom(fieldsToWatchDescriptor.Value.GetType()))
                            throw new ArgumentException($"{fieldsToWatchDescriptor}: CB7F6C35-B3FE-4BF3-9C8E-BD84C2A72001");

                        if (
                                new HashSet<string>
                                (
                                    ((IEnumerable<string>)fieldsToWatchDescriptor.Value).Select(f => GetFieldName(f, condition.ParentField))
                                ).Contains(fieldName)
                          )
                        {
                            Check(condition);
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"{condition.DirectiveDefinition.FunctionName}: 0579B28B-0A54-4ED1-A4FC-7FB99CD2BE77");
                    }
                }
            );
        }

        private void DisposeSubscription(IDisposable subscription)
        {
            if (subscription != null)
            {
                subscription.Dispose();
            }
        }

        private string GetFieldName(string field, string parentName)
                => parentName == null ? field : $"{parentName}.{field}";
    }
}
