using AutoMapper;
using Contoso.Forms.Configuration.Directives;
using Contoso.XPlatform.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Contoso.XPlatform.Validators
{
    internal class HideIfManager<TModel> : IDisposable
    {
        public HideIfManager(IEnumerable<IFormField> currentProperties, List<HideIf<TModel>> conditions, IMapper mapper, UiNotificationService uiNotificationService)
        {
            CurrentProperties = currentProperties;
            this.conditions = conditions;
            this.mapper = mapper;
            this.uiNotificationService = uiNotificationService;
            propertyChangedSubscription = this.uiNotificationService.ValueChanged.Subscribe(PropertyChanged);
        }

        private readonly IMapper mapper;
        private readonly List<HideIf<TModel>> conditions;
        private readonly UiNotificationService uiNotificationService;
        private readonly IDisposable propertyChangedSubscription;

        public IEnumerable<IFormField> CurrentProperties { get; }
        private IDictionary<string, IFormField> CurrentPropertiesDictionary
            => CurrentProperties.ToDictionary(p => p.Name);

        public void Check(HideIf<TModel> condition)
        {
            DoCheck(CurrentPropertiesDictionary[condition.Field]);

            void DoCheck(IFormField currentField)
            {
                currentField.IsVisible = ShouldHide
                (
                    mapper.Map<TModel>(CurrentProperties.ToDictionary(p => p.Name, p => p.Value)),
                    condition.Evaluator
                ) == false;
            }

            bool ShouldHide(TModel entity, Expression<Func<TModel, bool>> evaluator)
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
                    if (condition.DirectiveDefinition.FunctionName == nameof(HideIfManager<TModel>.Check))
                    {
                        if (condition.DirectiveDefinition.Arguments?.Any() != true)
                            throw new ArgumentException($"{condition.DirectiveDefinition.Arguments}: 44DCE54F-354A-4A4C-B0C3-33996894C523");

                        const string fieldsToWatch = "fieldsToWatch";
                        if (!condition.DirectiveDefinition.Arguments.TryGetValue(fieldsToWatch, out DirectiveArgumentDescriptor fieldsToWatchDescriptor))
                            throw new ArgumentException($"{fieldsToWatch}: 8B4587F9-2E28-4BEC-895E-B76452D96A8C");
                        if (!typeof(IEnumerable<string>).IsAssignableFrom(fieldsToWatchDescriptor.Value.GetType()))
                            throw new ArgumentException($"{fieldsToWatchDescriptor}: CF80A321-A3FE-4C3A-91E9-83EE1ADB051D");

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
                        throw new ArgumentException($"{condition.DirectiveDefinition.FunctionName}: 670983D8-C169-431A-8ABC-7EE721BC4133");
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
