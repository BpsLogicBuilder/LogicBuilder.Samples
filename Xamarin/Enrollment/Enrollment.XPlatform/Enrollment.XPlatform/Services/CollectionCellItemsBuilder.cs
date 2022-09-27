using Enrollment.Forms.Configuration.Bindings;
using Enrollment.XPlatform.Utils;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using Enrollment.XPlatform.ViewModels.ReadOnlys.Factories;
using LogicBuilder.Expressions.Utils;
using System;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public class CollectionCellItemsBuilder : ICollectionCellItemsBuilder
    {
        private readonly IReadOnlyFactory readOnlyFactory;
        private readonly List<ItemBindingDescriptor> itemBindings;
        private readonly Type modelType;
        public ICollection<IReadOnly> Properties { get; }

        public CollectionCellItemsBuilder(
            IReadOnlyFactory readOnlyFactory,
            List<ItemBindingDescriptor> itemBindings,
            Type modelType)
        {
            this.itemBindings = itemBindings;
            this.readOnlyFactory = readOnlyFactory;
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

        private void AddMultiSelectCell(MultiSelectItemBindingDescriptor binding)
            => Properties.Add(CreateMultiSelectReadOnlyObject(binding));

        private void AddDropDowCell(DropDownItemBindingDescriptor binding)
            => Properties.Add(CreatePickerReadOnlyObject(binding));

        private IReadOnly CreateHiddenReadOnlyObject(TextItemBindingDescriptor binding) 
            => readOnlyFactory.CreateHiddenReadOnlyObject
            (
                GetModelFieldType(binding.Property),
                binding.Property,
                binding.TextTemplate.TemplateName
            );

        private IReadOnly CreateCheckboxReadOnlyObject(TextItemBindingDescriptor binding) 
            => readOnlyFactory.CreateCheckboxReadOnlyObject
            (
                binding.Property,
                binding.TextTemplate.TemplateName,
                binding.Title
            );

        private IReadOnly CreateSwitchReadOnlyObject(TextItemBindingDescriptor binding) 
            => readOnlyFactory.CreateSwitchReadOnlyObject
            (
                binding.Property,
                binding.TextTemplate.TemplateName,
                binding.Title
            );

        private IReadOnly CreateTextFieldReadOnlyObject(TextItemBindingDescriptor binding) 
            => readOnlyFactory.CreateTextFieldReadOnlyObject
            (
                GetModelFieldType(binding.Property),
                binding.Property,
                binding.TextTemplate.TemplateName,
                binding.Title,
                binding.StringFormat
            );

        private IReadOnly CreatePickerReadOnlyObject(DropDownItemBindingDescriptor binding) 
            => readOnlyFactory.CreatePickerReadOnlyObject
            (
                GetModelFieldType(binding.Property),
                binding.Property,
                binding.Title,
                binding.StringFormat,
                binding.DropDownTemplate
            );

        private IReadOnly CreateMultiSelectReadOnlyObject(MultiSelectItemBindingDescriptor binding)
        {
            Type type = GetModelFieldType(binding.Property);
            if (!type.IsList())
                throw new ArgumentException($"{nameof(type)}: {{FC9E3842-11E3-420C-8A34-ED94DA56126E}}");

            return readOnlyFactory.CreateMultiSelectReadOnlyObject
            (
                type.GetUnderlyingElementType(),
                binding.Property,
                binding.KeyFields,
                binding.Title,
                binding.StringFormat,
                binding.MultiSelectTemplate
            );
        }

        Type GetModelFieldType(string fullPropertyName)
                => this.modelType.GetMemberInfoFromFullName(fullPropertyName).GetMemberType();
    }
}
