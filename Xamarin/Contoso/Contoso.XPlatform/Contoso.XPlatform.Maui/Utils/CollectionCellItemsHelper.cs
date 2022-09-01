﻿using Contoso.Forms.Configuration.Bindings;
using Contoso.XPlatform.Services;
using Contoso.XPlatform.ViewModels.ReadOnlys;
using LogicBuilder.Expressions.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Contoso.XPlatform.Utils
{
    internal class CollectionCellItemsHelper
    {
        private List<ItemBindingDescriptor> itemBindings;
        private readonly IContextProvider contextProvider;
        private readonly Type modelType;
        public ICollection<IReadOnly> Properties { get; }

        public CollectionCellItemsHelper(List<ItemBindingDescriptor> itemBindings, IContextProvider contextProvider, Type modelType)
        {
            this.itemBindings = itemBindings;
            this.contextProvider = contextProvider;
            this.modelType = modelType;
            Properties = new List<IReadOnly>();
        }

        public ICollection<IReadOnly> CreateFields()
        {
            this.CreateFieldsCollection();
            return this.Properties;
        }

        private void CreateFieldsCollection()
        {
            itemBindings.ForEach
            (
                binding =>
                {
                    switch (binding)
                    {
                        case DropDownItemBindingDescriptor dropDownItemBindingDescriptor:
                            AddDropDowCell(dropDownItemBindingDescriptor);
                            break;
                        case MultiSelectItemBindingDescriptor multiSelectItemBindingDescriptor:
                            AddMultiSelectCell(multiSelectItemBindingDescriptor);
                            break;
                        case TextItemBindingDescriptor textItemBindingDescriptor:
                            AddTextCell(textItemBindingDescriptor);
                            break;
                        default:
                            throw new ArgumentException($"{nameof(binding)}: 4BF30766-5D6D-4ACE-B205-12A59A98BEAD");
                    }
                }
            );
        }

        private void AddTextCell(TextItemBindingDescriptor binding)
        {
            if (binding.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.TextTemplate)
                || binding.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.PasswordTemplate)
                || binding.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.DateTemplate))
            {
                Properties.Add(CreateTextFieldReadOnlyObject(binding));
            }
            else if (binding.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.HiddenTemplate))
            {
                Properties.Add(CreateHiddenReadOnlyObject(binding));
            }
            else if (binding.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.CheckboxTemplate))
            {
                Properties.Add(CreateCheckboxReadOnlyObject(binding));
            }
            else if (binding.TextTemplate.TemplateName == nameof(ReadOnlyControlTemplateSelector.SwitchTemplate))
            {
                Properties.Add(CreateSwitchReadOnlyObject(binding));
            }
            else
            {
                throw new ArgumentException($"{nameof(binding.TextTemplate.TemplateName)}: C858796C-6625-43A4-AA56-DC131036FDFD");
            }
        }

        private IReadOnly CreateHiddenReadOnlyObject(TextItemBindingDescriptor binding)
            =>(IReadOnly)(
                Activator.CreateInstance
                (
                    typeof(HiddenReadOnlyObject<>).MakeGenericType(GetModelFieldType(binding.Property)),
                    binding.Property,
                    binding.TextTemplate.TemplateName,
                    this.contextProvider
                ) ?? throw new ArgumentException($"{binding.Property}: {{BFEB64A2-F3A8-4508-94A4-03F57703BFF5}}")
            );

        private IReadOnly CreateCheckboxReadOnlyObject(TextItemBindingDescriptor binding)
            => (IReadOnly)(Activator.CreateInstance
                (
                    typeof(CheckboxReadOnlyObject),
                    binding.Property,
                    binding.TextTemplate.TemplateName,
                    binding.Title,
                    this.contextProvider
                ) ?? throw new ArgumentException($"{typeof(CheckboxReadOnlyObject)}: {{368F5D2C-B20F-41F6-89F1-CE073DF4B04E}}")
            );

        private IReadOnly CreateSwitchReadOnlyObject(TextItemBindingDescriptor binding)
            => (IReadOnly)(
                Activator.CreateInstance
                (
                    typeof(SwitchReadOnlyObject),
                    binding.Property,
                    binding.TextTemplate.TemplateName,
                    binding.Title,
                    this.contextProvider
                ) ?? throw new ArgumentException($"{typeof(SwitchReadOnlyObject)}: {{0CDD3611-8C67-47A7-8D6E-40D061D2E87F}}")
            );

        private IReadOnly CreateTextFieldReadOnlyObject(TextItemBindingDescriptor binding)
            => (IReadOnly)(
                Activator.CreateInstance
                (
                    typeof(TextFieldReadOnlyObject<>).MakeGenericType(GetModelFieldType(binding.Property)),
                    binding.Property,
                    binding.TextTemplate.TemplateName,
                    binding.Title,
                    binding.StringFormat,
                    this.contextProvider
                ) ?? throw new ArgumentException($"{binding.Property}: {{9EF8E935-40F9-4904-8625-500B74481F03}}")
            );

        private void AddMultiSelectCell(MultiSelectItemBindingDescriptor binding)
            => Properties.Add(CreateMultiSelectReadOnlyObject(binding));

        private void AddDropDowCell(DropDownItemBindingDescriptor binding)
            => Properties.Add(CreatePickerReadOnlyObject(binding));

        private IReadOnly CreatePickerReadOnlyObject(DropDownItemBindingDescriptor binding)
            => (IReadOnly)(
                Activator.CreateInstance
                (
                    typeof(PickerReadOnlyObject<>).MakeGenericType(GetModelFieldType(binding.Property)),
                    binding.Property,
                    binding.Title,
                    binding.StringFormat,
                    binding.DropDownTemplate,
                    this.contextProvider
                ) ?? throw new ArgumentException($"{binding.Property}: {{15DACB56-2914-4A2A-8089-48FBC5BBBA49}}")
            );

        private IReadOnly CreateMultiSelectReadOnlyObject(MultiSelectItemBindingDescriptor binding)
        {
            Type type = GetModelFieldType(binding.Property);
            if (!type.IsList())
                throw new ArgumentException($"{nameof(type)}: 4F67CC2A-21B8-49B4-BFC2-4D949518AB34");

            return GetValidatable(type.GetUnderlyingElementType());

            IReadOnly GetValidatable(Type elementType)
                => (IReadOnly)(
                    Activator.CreateInstance
                    (
                        typeof(MultiSelectReadOnlyObject<,>).MakeGenericType
                        (
                            typeof(ObservableCollection<>).MakeGenericType(elementType),
                            elementType
                        ),
                        binding.Property,
                        binding.KeyFields,
                        binding.Title,
                        binding.StringFormat,
                        binding.MultiSelectTemplate,
                        this.contextProvider
                    ) ?? throw new ArgumentException($"{elementType}: {{D7A4FD1B-6F25-41EF-BDE1-1840702483D7}}")
                );
        }

        Type GetModelFieldType(string fullPropertyName)
                => this.modelType.GetMemberInfoFromFullName(fullPropertyName).GetMemberType();
    }
}