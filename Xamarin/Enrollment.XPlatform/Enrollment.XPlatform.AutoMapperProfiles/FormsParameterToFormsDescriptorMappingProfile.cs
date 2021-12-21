using AutoMapper;
using Enrollment.Forms.Configuration;
using Enrollment.Forms.Configuration.Bindings;
using Enrollment.Forms.Configuration.Directives;
using Enrollment.Forms.Configuration.DataForm;
using Enrollment.Forms.Configuration.ListForm;
using Enrollment.Forms.Configuration.Navigation;
using Enrollment.Forms.Configuration.SearchForm;
using Enrollment.Forms.Configuration.TextForm;
using Enrollment.Forms.Configuration.Validation;
using Enrollment.Forms.Parameters;
using Enrollment.Forms.Parameters.Bindings;
using Enrollment.Forms.Parameters.Directives;
using Enrollment.Forms.Parameters.DataForm;
using Enrollment.Forms.Parameters.ListForm;
using Enrollment.Forms.Parameters.Navigation;
using Enrollment.Forms.Parameters.SearchForm;
using Enrollment.Forms.Parameters.TextForm;
using Enrollment.Forms.Parameters.Validation;

namespace Enrollment.XPlatform.AutoMapperProfiles
{
    public class FormsParameterToFormsDescriptorMappingProfile : Profile
    {
        public FormsParameterToFormsDescriptorMappingProfile()
        {
			CreateMap<DataFormSettingsParameters, DataFormSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<DirectiveArgumentParameters, DirectiveArgumentDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<DirectiveDefinitionParameters, DirectiveDefinitionDescriptor>();
			CreateMap<DirectiveParameters, DirectiveDescriptor>();
			CreateMap<DropDownItemBindingParameters, DropDownItemBindingDescriptor>();
			CreateMap<DropDownTemplateParameters, DropDownTemplateDescriptor>();
			CreateMap<FieldValidationSettingsParameters, FieldValidationSettingsDescriptor>();
			CreateMap<FormattedLabelItemParameters, FormattedLabelItemDescriptor>();
			CreateMap<FormControlSettingsParameters, FormControlSettingsDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<FormGroupArraySettingsParameters, FormGroupArraySettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<FormGroupBoxSettingsParameters, FormGroupBoxSettingsDescriptor>();
			CreateMap<FormGroupSettingsParameters, FormGroupSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<FormGroupTemplateParameters, FormGroupTemplateDescriptor>();
			CreateMap<FormRequestDetailsParameters, FormRequestDetailsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
				.ForMember(dest => dest.DataType, opts => opts.MapFrom(x => x.DataType.AssemblyQualifiedName));
			CreateMap<FormsCollectionDisplayTemplateParameters, FormsCollectionDisplayTemplateDescriptor>();
			CreateMap<HyperLinkLabelItemParameters, HyperLinkLabelItemDescriptor>();
			CreateMap<HyperLinkSpanItemParameters, HyperLinkSpanItemDescriptor>();
			CreateMap<LabelItemParameters, LabelItemDescriptor>();
			CreateMap<ListFormSettingsParameters, ListFormSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<MultiBindingParameters, MultiBindingDescriptor>();
			CreateMap<MultiSelectFormControlSettingsParameters, MultiSelectFormControlSettingsDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<MultiSelectItemBindingParameters, MultiSelectItemBindingDescriptor>();
			CreateMap<MultiSelectTemplateParameters, MultiSelectTemplateDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<NavigationBarParameters, NavigationBarDescriptor>();
			CreateMap<NavigationMenuItemParameters, NavigationMenuItemDescriptor>();
			CreateMap<RequestDetailsParameters, RequestDetailsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName))
				.ForMember(dest => dest.DataType, opts => opts.MapFrom(x => x.DataType.AssemblyQualifiedName))
				.ForMember(dest => dest.ModelReturnType, opts => opts.MapFrom(x => x.ModelReturnType.AssemblyQualifiedName))
				.ForMember(dest => dest.DataReturnType, opts => opts.MapFrom(x => x.DataReturnType.AssemblyQualifiedName));
			CreateMap<SearchFilterGroupParameters, SearchFilterGroupDescriptor>();
			CreateMap<SearchFilterParameters, SearchFilterDescriptor>();
			CreateMap<SearchFormSettingsParameters, SearchFormSettingsDescriptor>()
				.ForMember(dest => dest.ModelType, opts => opts.MapFrom(x => x.ModelType.AssemblyQualifiedName));
			CreateMap<SpanItemParameters, SpanItemDescriptor>();
			CreateMap<TextFieldTemplateParameters, TextFieldTemplateDescriptor>();
			CreateMap<TextFormSettingsParameters, TextFormSettingsDescriptor>();
			CreateMap<TextGroupParameters, TextGroupDescriptor>();
			CreateMap<TextItemBindingParameters, TextItemBindingDescriptor>();
			CreateMap<ValidationMessageParameters, ValidationMessageDescriptor>();
			CreateMap<ValidationRuleParameters, ValidationRuleDescriptor>();
			CreateMap<ValidatorArgumentParameters, ValidatorArgumentDescriptor>()
				.ForMember(dest => dest.Type, opts => opts.MapFrom(x => x.Type.AssemblyQualifiedName));
			CreateMap<ValidatorDefinitionParameters, ValidatorDefinitionDescriptor>();
			CreateMap<VariableDirectivesParameters, VariableDirectivesDescriptor>();

            CreateMap<FormItemSettingsParameters, FormItemSettingsDescriptor>()
				.Include<FormControlSettingsParameters, FormControlSettingsDescriptor>()
				.Include<FormGroupArraySettingsParameters, FormGroupArraySettingsDescriptor>()
				.Include<FormGroupBoxSettingsParameters, FormGroupBoxSettingsDescriptor>()
				.Include<FormGroupSettingsParameters, FormGroupSettingsDescriptor>()
				.Include<MultiSelectFormControlSettingsParameters, MultiSelectFormControlSettingsDescriptor>();

            CreateMap<SearchFilterParametersBase, SearchFilterDescriptorBase>()
				.Include<SearchFilterGroupParameters, SearchFilterGroupDescriptor>()
				.Include<SearchFilterParameters, SearchFilterDescriptor>();

            CreateMap<LabelItemParametersBase, LabelItemDescriptorBase>()
				.Include<FormattedLabelItemParameters, FormattedLabelItemDescriptor>()
				.Include<HyperLinkLabelItemParameters, HyperLinkLabelItemDescriptor>()
				.Include<LabelItemParameters, LabelItemDescriptor>();

            CreateMap<SpanItemParametersBase, SpanItemDescriptorBase>()
				.Include<HyperLinkSpanItemParameters, HyperLinkSpanItemDescriptor>()
				.Include<SpanItemParameters, SpanItemDescriptor>();

            CreateMap<ItemBindingParameters, ItemBindingDescriptor>()
				.Include<DropDownItemBindingParameters, DropDownItemBindingDescriptor>()
				.Include<MultiSelectItemBindingParameters, MultiSelectItemBindingDescriptor>()
				.Include<TextItemBindingParameters, TextItemBindingDescriptor>();
        }
    }
}