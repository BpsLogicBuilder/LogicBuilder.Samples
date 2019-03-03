using AutoMapper;
using CheckMySymptoms.Forms.Parameters.Common;
using CheckMySymptoms.Forms.View.Common;
using System;
using System.Collections.Generic;

namespace CheckMySymptoms.AutoMapperProfiles
{
    public class CommonParametersMappingProfile : Profile
    {
        public CommonParametersMappingProfile()
        {
			CreateMap<AboutFormSettingsParameters, AboutFormSettingsView>().ReverseMap();
			CreateMap<AggregateDefinitionParameters, AggregateDefinitionView>().ReverseMap();
			CreateMap<AggregateTemplateFieldsParameters, AggregateTemplateFieldsView>().ReverseMap();
			CreateMap<AggregateTemplateParameters, AggregateTemplateView>().ReverseMap();
			CreateMap<CellListTemplateParameters, CellListTemplateView>().ReverseMap();
			CreateMap<CellTemplateParameters, CellTemplateView>().ReverseMap();
			CreateMap<ColumnSettingsParameters, ColumnSettingsView>().ReverseMap();
			CreateMap<CommandColumnParameters, CommandColumnView>().ReverseMap();
			CreateMap<ConditionGroupParameters, ConditionGroupView>().ReverseMap();
			CreateMap<ConditionParameters, ConditionView>().ReverseMap();
			CreateMap<ContentTemplateParameters, ContentTemplateView>().ReverseMap();
			CreateMap<DataRequestStateParameters, DataRequestStateView>().ReverseMap();
			CreateMap<DetailDropDownTemplateParameters, DetailDropDownTemplateView>().ReverseMap();
			CreateMap<DetailFieldSettingParameters, DetailFieldSettingView>().ReverseMap();
			CreateMap<DetailFieldTemplateParameters, DetailFieldTemplateView>().ReverseMap();
			CreateMap<DetailFormSettingsParameters, DetailFormSettingsView>().ReverseMap();
			CreateMap<DetailGroupSettingsParameters, DetailGroupSettingsView>().ReverseMap();
			CreateMap<DetailGroupTemplateParameters, DetailGroupTemplateView>().ReverseMap();
			CreateMap<DetailListSettingsParameters, DetailListSettingsView>().ReverseMap();
			CreateMap<DetailListTemplateParameters, DetailListTemplateView>().ReverseMap();
			CreateMap<DirectiveArgumentParameters, DirectiveArgumentView>().ReverseMap();
			CreateMap<DirectiveDescriptionParameters, DirectiveDescriptionView>().ReverseMap();
			CreateMap<DirectiveParameters, DirectiveView>().ReverseMap();
			CreateMap<DomainRequestParameters, DomainRequestView>().ReverseMap();
			CreateMap<DropDownTemplateParameters, DropDownTemplateView>().ReverseMap();
			CreateMap<DummyConstructor, DummyConstructor>().ReverseMap();
			CreateMap<EditFormSettingsParameters, EditFormSettingsView>().ReverseMap();
			CreateMap<FilterDefinitionParameters, FilterDefinitionView>().ReverseMap();
			CreateMap<FilterGroupParameters, FilterGroupView>().ReverseMap();
			CreateMap<FilterTemplateParameters, FilterTemplateView>().ReverseMap();
			CreateMap<FlowCompleteParameters, FlowCompleteView>().ReverseMap();
			CreateMap<FormControlSettingsParameters, FormControlSettingsView>().ReverseMap();
			CreateMap<FormGroupArraySettingsParameters, FormGroupArraySettingsView>().ReverseMap();
			CreateMap<FormGroupSettingsParameters, FormGroupSettingsView>().ReverseMap();
			CreateMap<FormGroupTemplateParameters, FormGroupTemplateView>().ReverseMap();
			CreateMap<FormValidationSettingParameters, FormValidationSettingView>().ReverseMap();
			CreateMap<GridSettingsParameters, GridSettingsView>().ReverseMap();
			CreateMap<GroupParameters, GroupView>().ReverseMap();
			CreateMap<HtmlPageSettingsParameters, HtmlPageSettingsView>().ReverseMap();
			CreateMap<MessageTemplateParameters, MessageTemplateView>().ReverseMap();
			CreateMap<MultiSelectFormControlSettingsParameters, MultiSelectFormControlSettingsView>().ReverseMap();
			CreateMap<MultiSelectTemplateParameters, MultiSelectTemplateView>().ReverseMap();
			CreateMap<RequestDetailsParameters, RequestDetailsView>().ReverseMap();
			CreateMap<SelectParameters, SelectView>().ReverseMap();
			CreateMap<SortParameters, SortView>().ReverseMap();
			CreateMap<TextFieldTemplateParameters, TextFieldTemplateView>().ReverseMap();
			CreateMap<ValidationMessageParameters, ValidationMessageView>().ReverseMap();
			CreateMap<ValidationMethodParameters, ValidationMethodView>().ReverseMap();
			CreateMap<ValidatorArgumentParameters, ValidatorArgumentView>().ReverseMap();
			CreateMap<ValidatorDescriptionParameters, ValidatorDescriptionView>().ReverseMap();
			CreateMap<VariableDirectivesParameters, VariableDirectivesView>().ReverseMap();
        }
    }
}