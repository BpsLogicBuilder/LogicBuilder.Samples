using AutoMapper;
using CheckMySymptoms.Flow.Cache;
using CheckMySymptoms.Flow.ScreenSettings;
using CheckMySymptoms.Flow.ScreenSettings.Views;
using CheckMySymptoms.Forms.Parameters.Common;
using CheckMySymptoms.Forms.View;
using CheckMySymptoms.Forms.View.Common;
using LogicBuilder.Attributes;
using LogicBuilder.Forms.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckMySymptoms.Flow
{
    public class CustomDialogs : ICustomDialogs
    {
        public CustomDialogs(IMapper mapper, ScreenData screenData, FlowDataCache flowDataCache)
        {
            this.screenData = screenData;
            this.flowDataCache = flowDataCache;
            this.mapper = mapper;
        }

        #region Fields
        private readonly ScreenData screenData;
        private readonly FlowDataCache flowDataCache;
        private readonly IMapper mapper;
        #endregion Fields

        #region Methods
        public void DisplayHtmlForm([Comments("Configuration details for the form.")] HtmlPageSettingsParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            HtmlPageSettingsView formView = this.mapper.Map<HtmlPageSettingsView>(setting);
            string dialogListItem = formView.ContentTemplate != null
                ? formView.ContentTemplate.Title ?? string.Empty
                : formView?.MessageTemplate?.Message ?? string.Empty;

            this.screenData.ScreenSettings = new ScreenSettings<HtmlPageSettingsView>
            (
                formView,
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonView>>(buttons),
                ViewType.Html,
                new MenuItem { Text = dialogListItem, Icon = formView.Icon }
            );
        }

        public void DisplaySelectForm([Comments("Configuration details for the form.")] MessageTemplateParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            MessageTemplateView view = this.mapper.Map<MessageTemplateView>(setting);
            this.screenData.ScreenSettings = new ScreenSettings<MessageTemplateView>
            (
                view,
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonView>>(buttons),
                ViewType.Select,
                new MenuItem { Text = view.Message, Icon = view.Icon }
            );
        }

        public void DisplayFlowComplete([Comments("Configuration details for the form.")] FlowCompleteParameters setting, [ListEditorControl(ListControlType.Connectors)] ICollection<ConnectorParameters> buttons)
        {
            FlowCompleteView view = this.mapper.Map<FlowCompleteView>(setting);
            view.Symptoms = flowDataCache.Symptoms;
            view.Diagnoses = flowDataCache.Diagnoses;
            view.Treatments = flowDataCache.Treatments;
            this.screenData.ScreenSettings = new ScreenSettings<FlowCompleteView>
            (
                view,
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonView>>(buttons),
                ViewType.FlowComplete,
                new MenuItem { Text = view.Message, Icon = view.Icon }
            );
        }
        #endregion Methods

    }
}
