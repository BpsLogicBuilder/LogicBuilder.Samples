using AutoMapper;
using Enrollment.Forms.Parameters.Common;
using Enrollment.Forms.View;
using Enrollment.Forms.View.Common;
using Enrollment.Spa.Flow.Cache;
using Enrollment.Spa.Flow.ScreenSettings.Views;
using LogicBuilder.Forms.Parameters;
using System;
using System.Collections.Generic;

namespace Enrollment.Spa.Flow
{
    public class CustomDialogs : ICustomDialogs
    {
        public CustomDialogs(IMapper mapper, FlowDataCache flowDataCache)
        {
            this.flowDataCache = flowDataCache;
            this.mapper = mapper;
        }

        #region Fields
        private readonly FlowDataCache flowDataCache;
        private readonly IMapper mapper;
        #endregion Fields

        public void DisplayGrid(GridSettingsParameters setting, ICollection<ConnectorParameters> buttons)
            => this.flowDataCache.ScreenSettings = new ScreenSettings<GridSettingsView>
            (
                mapper.Map<GridSettingsView>(setting),
                mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonView>>(buttons),
                ViewType.Grid
            );

        public void DisplayEditForm(EditFormSettingsParameters setting, ViewType viewType, ICollection<ConnectorParameters> buttons)
        {
            this.flowDataCache.ScreenSettings = viewType switch
            {
                ViewType.Edit or ViewType.Create => new ScreenSettings<EditFormSettingsView>
                (
                    mapper.Map<EditFormSettingsView>(setting),
                    mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonView>>(buttons),
                    viewType
                ),
                _ => throw new ArgumentException($"{nameof(viewType)}: {{B3B3788E-738A-4E6B-8B95-C6DDC2C4AA5D}}"),
            };
        }

        public void DisplayDetailForm(DetailFormSettingsParameters setting, ViewType viewType, ICollection<ConnectorParameters> buttons)
        {
            this.flowDataCache.ScreenSettings = viewType switch
            {
                ViewType.Detail or ViewType.Delete => new ScreenSettings<DetailFormSettingsView>
                (
                    mapper.Map<DetailFormSettingsView>(setting),
                    mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonView>>(buttons),
                    viewType
                ),
                _ => throw new ArgumentException($"{nameof(viewType)}: {{097C0872-CC75-4772-9570-64D9868DF970}}"),
            };
        }

        public void DisplayHtmlForm(HtmlPageSettingsParameters setting, ICollection<ConnectorParameters> buttons)
           => this.flowDataCache.ScreenSettings = new ScreenSettings<HtmlPageSettingsView>
           (
               mapper.Map<HtmlPageSettingsView>(setting),
               mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonView>>(buttons),
               ViewType.Html
           );

        public void DisplayListForm(ListFormSettingsParameters setting, ICollection<ConnectorParameters> buttons)
           => this.flowDataCache.ScreenSettings = new ScreenSettings<ListFormSettingsView>
           (
               mapper.Map<ListFormSettingsView>(setting),
               mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonView>>(buttons),
               ViewType.About
           );
    }
}
