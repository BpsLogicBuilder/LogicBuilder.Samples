using AutoMapper;
using Contoso.Forms.Configuration.Directives;
using Contoso.XPlatform.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contoso.XPlatform.Validators
{
    internal class ReloadIfManager<TModel> : IDisposable
    {
        public ReloadIfManager(IEnumerable<IFormField> currentProperties, List<ReloadIf<TModel>> conditions, IMapper mapper, UiNotificationService uiNotificationService)
        {
            CurrentProperties = currentProperties;
            this.conditions = conditions;
            this.mapper = mapper;
            this.uiNotificationService = uiNotificationService;
            propertyChangedSubscription = this.uiNotificationService.ValueChanged.Subscribe(PropertyChanged);
        }

        private readonly IMapper mapper;
        private readonly List<ReloadIf<TModel>> conditions;
        private readonly UiNotificationService uiNotificationService;
        private readonly IDisposable propertyChangedSubscription;

        public IEnumerable<IFormField> CurrentProperties { get; }
        private IDictionary<string, IFormField> CurrentPropertiesDictionary
            => CurrentProperties.ToDictionary(p => p.Name);

        public void Check(ReloadIf<TModel> condition)
        {
            DoCheck(CurrentPropertiesDictionary[condition.Field]);

            void DoCheck(IFormField currentField)
            {
                if (currentField is IHasItemsSource hasItemsSource)
                {
                    TModel entity = mapper.Map<TModel>(CurrentProperties.ToDictionary(p => p.Name, p => p.Value));
                    if
                    (
                        ShouldReload
                        (
                            entity,
                            condition.Evaluator
                        )
                    )
                    {
                        hasItemsSource.Reload(entity, typeof(TModel));
                    }
                }
            }

            bool ShouldReload(TModel entity, Expression<Func<TModel, bool>> evaluator)
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
                    if (condition.DirectiveDefinition.FunctionName == nameof(ReloadIfManager<TModel>.Check))
                    {
                        if (condition.DirectiveDefinition.Arguments?.Any() != true)
                            throw new ArgumentException($"{condition.DirectiveDefinition.Arguments}: 5CC8C779-D9B5-4CEC-9B2B-4D2AC8558E21");

                        const string fieldsToWatch = "fieldsToWatch";
                        if (!condition.DirectiveDefinition.Arguments.TryGetValue(fieldsToWatch, out DirectiveArgumentDescriptor fieldsToWatchDescriptor))
                            throw new ArgumentException($"{fieldsToWatch}: 2202BFA2-C2B4-4A93-975B-BB66DC975173");
                        if (!typeof(IEnumerable<string>).IsAssignableFrom(fieldsToWatchDescriptor.Value.GetType()))
                            throw new ArgumentException($"{fieldsToWatchDescriptor}: 923F2F02-F65D-4F88-9612-DA5991FA0A24");

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
                        throw new ArgumentException($"{condition.DirectiveDefinition.FunctionName}: CF744C7B-399B-4332-8431-62758B473248");
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
