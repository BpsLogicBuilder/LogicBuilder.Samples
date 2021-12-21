using AutoMapper;
using Contoso.Forms.Configuration;
using Contoso.Forms.Configuration.DataForm;
using Contoso.Forms.Configuration.ListForm;
using Contoso.Forms.Configuration.SearchForm;
using Contoso.Forms.Configuration.TextForm;
using Contoso.Forms.Parameters.DataForm;
using Contoso.Forms.Parameters.ListForm;
using Contoso.Forms.Parameters.SearchForm;
using Contoso.Forms.Parameters.TextForm;
using Contoso.XPlatform.Flow.Cache;
using Contoso.XPlatform.Flow.Settings.Screen;
using LogicBuilder.Attributes;
using LogicBuilder.Forms.Parameters;
using System;
using System.Collections.Generic;

namespace Contoso.XPlatform.Flow
{
    public class DialogFunctions : IDialogFunctions
    {
        public DialogFunctions(ScreenData screenData, IMapper mapper)
        {
            this.screenData = screenData;
            this.mapper = mapper;
        }

        #region Fields
        private readonly ScreenData screenData;
        private readonly IMapper mapper;
        #endregion Fields

        public void DisplayEditCollection([Comments("Configuration details for the form.")] SearchFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<SearchFormSettingsDescriptor>
            (
                mapper.Map<SearchFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.SearchPage
            );
        }

        public void DisplayEditForm([Comments("Configuration details for the form.")] DataFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<DataFormSettingsDescriptor>
            (
                mapper.Map<DataFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.EditForm
            );
        }

        public void DisplayDetailForm([Comments("Configuration details for the form.")] DataFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<DataFormSettingsDescriptor>
            (
                mapper.Map<DataFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.DetailForm
            );
        }

        public void DisplayReadOnlyCollection([Comments("Configuration details for the form.")] ListFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<ListFormSettingsDescriptor>
            (
                mapper.Map<ListFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.ListPage
            );
        }

        public void DisplayTextForm([Comments("Configuration details for the form.")] TextFormSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            this.screenData.ScreenSettings = new ScreenSettings<TextFormSettingsDescriptor>
            (
                mapper.Map<TextFormSettingsDescriptor>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonDescriptor>>(buttons),
                ViewType.TextPage
            );
        }
    }
}
