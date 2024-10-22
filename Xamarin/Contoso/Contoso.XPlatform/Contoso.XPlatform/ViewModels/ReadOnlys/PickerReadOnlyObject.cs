﻿using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Common.Configuration.ExpressionDescriptors;
using Contoso.Forms.Configuration;
using Contoso.Parameters.Expressions;
using Contoso.Utils;
using Contoso.XPlatform.Constants;
using Contoso.XPlatform.Flow.Requests;
using Contoso.XPlatform.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.XPlatform.ViewModels.ReadOnlys
{
    public class PickerReadOnlyObject<T> : ReadOnlyObjectBase<T>, IHasItemsSourceReadOnly
    {
        public PickerReadOnlyObject(
            IHttpService httpService,
            IMapper mapper,
            IUiNotificationService uiNotificationService,
            string name,
            string title,
            string stringFormat,
            DropDownTemplateDescriptor dropDownTemplate) : base(name, dropDownTemplate.TemplateName, uiNotificationService)
        {
            this._dropDownTemplate = dropDownTemplate;
            this.httpService = httpService;
            _defaultTitle = title;
            _stringFormat = stringFormat;
            /*MemberNotNull unvailable in 2.1*/
            _title = null!;
            /*MemberNotNull unavailable in 2.1*/
            this.Title = _defaultTitle;
            this.mapper = mapper;
            GetItemSource();
        }

        private readonly IHttpService httpService;
        private readonly DropDownTemplateDescriptor _dropDownTemplate;
        private List<object>? _items;
        private readonly IMapper mapper;
        private readonly string _defaultTitle;
        private readonly string _stringFormat;

        public DropDownTemplateDescriptor DropDownTemplate => _dropDownTemplate;

        public string DisplayText
        {
            get
            {
                if (SelectedItem == null)
                    return string.Empty;

                if (string.IsNullOrEmpty(_stringFormat))
                    return SelectedItem.GetPropertyValue<string>(_dropDownTemplate.TextField);

                return string.Format
                (
                    CultureInfo.CurrentCulture,
                    _stringFormat,
                    SelectedItem.GetPropertyValue<string>(_dropDownTemplate.TextField)
                );
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            //[MemberNotNull(nameof(_title))]
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        private string? _placeholder;
        public string? Placeholder
        {
            get => _placeholder;
            set
            {
                if (_placeholder == value)
                    return;

                _placeholder = value;
                OnPropertyChanged();
            }
        }

        public object? SelectedItem
        {
            get
            {
                if (_items?.Any() != true)
                    return null;

                return _items.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value!,/*EqualityComparer not built for nullable reference types in 2.1*/
                        i.GetPropertyValue<T>(_dropDownTemplate.ValueField)
                    )
                );
            }
        }

        public override T? Value
        {
            get { return base.Value; }
            set
            {
                base.Value = value;
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        public List<object>? Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private async void GetItemSource()
        {
            await GetItems(this.DropDownTemplate.TextAndValueSelector);
            OnPropertyChanged(nameof(SelectedItem));
            OnPropertyChanged(nameof(DisplayText));
        }

        private async Task GetItems(SelectorLambdaOperatorDescriptor selector)
        {
            try
            {
                BaseResponse response = await this.httpService.GetObjectDropDown
                (
                    new GetTypedListRequest
                    {
                        DataType = this._dropDownTemplate.RequestDetails.DataType,
                        ModelType = this._dropDownTemplate.RequestDetails.ModelType,
                        ModelReturnType = this._dropDownTemplate.RequestDetails.ModelReturnType,
                        DataReturnType = this._dropDownTemplate.RequestDetails.DataReturnType,
                        Selector = selector
                    },
                    this._dropDownTemplate.RequestDetails.DataSourceUrl
                );

                if (response.Success != true)
                {
#if DEBUG
                    await App.Current!.MainPage!.DisplayAlert/*App.Current.MainPage is not null at this point*/
                    (
                        "Errors",
                        string.Join(Environment.NewLine, response.ErrorMessages),
                        "Ok"
                    );
#endif
                    return;
                }

                Items = ((GetListResponse)response).List.Cast<object>().ToList();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async void Reload(object entity, Type entityType)
        {
            using (IScopedFlowManagerService flowManagerService = App.ServiceProvider.GetRequiredService<IScopedFlowManagerService>())
            {
                flowManagerService.SetFlowDataCacheItem
                (
                    entityType.FullName ?? throw new ArgumentException($"{nameof(entityType.FullName)}: {{2C2CB57D-A9D0-40C0-80C5-95E1CDE100E4}}"), 
                    entity
                );

                await flowManagerService.RunFlow
                (
                    new NewFlowRequest
                    {
                        InitialModuleName = this._dropDownTemplate.ReloadItemsFlowName
                    }
                );

                if ((flowManagerService.GetFlowDataCacheItem(FlowDataCacheItemKeys.Get_Selector_Success) ?? false).Equals(false))
                    return;

                SelectorLambdaOperatorDescriptor selector = this.mapper.Map<SelectorLambdaOperatorDescriptor>
                (
                    flowManagerService.GetFlowDataCacheItem(typeof(SelectorLambdaOperatorParameters).FullName!)/*FullName of known type*/
                );

                this.Title = this._dropDownTemplate.LoadingIndicatorText;

                await GetItems(selector);
            }

            this.Title = _defaultTitle;

            Value = GetExistingValue() ?? default;

            T? GetExistingValue()
            {
                object? existing = Items?.FirstOrDefault
                (
                    i => EqualityComparer<T>.Default.Equals
                    (
                        Value!,/*EqualityComparer not built for nullable reference types in 2.1*/
                        i.GetPropertyValue<T>(_dropDownTemplate.ValueField)
                    )
                );

                return existing == null
                    ? default
                    : existing.GetPropertyValue<T>(_dropDownTemplate.ValueField);
            }
        }

        public override void Clear()
        {
            Items = null;
            Value = default;
        }
    }
}
