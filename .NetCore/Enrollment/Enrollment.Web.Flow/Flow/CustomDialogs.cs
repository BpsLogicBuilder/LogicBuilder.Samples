using AutoMapper;
using Enrollment.Forms.Parameters.Common;
using Enrollment.Forms.View;
using Enrollment.Forms.View.Common;
using Enrollment.Web.Flow.Cache;
using Enrollment.Web.Flow.ScreenSettings.Views;
using LogicBuilder.Forms.Parameters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Enrollment.Web.Flow
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
            switch (viewType)
            {
                case ViewType.Edit:
                case ViewType.Create:
                    this.flowDataCache.ScreenSettings = new ScreenSettings<EditFormSettingsView>
                    (
                        mapper.Map<EditFormSettingsView>(setting),
                        mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonView>>(buttons), 
                        viewType
                    );
                    break;
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Properties.Resources.invalidArgumentFormat, "viewType"));
            }
        }

        public void DisplayDetailForm(DetailFormSettingsParameters setting, ViewType viewType, ICollection<ConnectorParameters> buttons)
        {
            switch (viewType)
            {
                case ViewType.Detail:
                case ViewType.Delete:
                    this.flowDataCache.ScreenSettings = new ScreenSettings<DetailFormSettingsView>
                    (
                        mapper.Map<DetailFormSettingsView>(setting),
                        mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonView>>(buttons), 
                        viewType
                    );
                    break;
                default:
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Properties.Resources.invalidArgumentFormat, "viewType"));
            }
        }

        public void DisplayHtmlForm(HtmlPageSettingsParameters setting, ICollection<ConnectorParameters> buttons)
           => this.flowDataCache.ScreenSettings = new ScreenSettings<HtmlPageSettingsView>
           (
               mapper.Map<HtmlPageSettingsView>(setting),
               mapper.Map<IEnumerable<ConnectorParameters>, IEnumerable<CommandButtonView>>(buttons), 
               ViewType.Html
           );
    }
}
