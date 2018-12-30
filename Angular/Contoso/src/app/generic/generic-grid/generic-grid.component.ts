import { Component, OnInit, ViewChild, Input, TemplateRef } from '@angular/core';

import { DataStateChangeEvent, FilterService } from '@progress/kendo-angular-grid';
import { DataSourceRequestState, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { GridService } from '../../http/grid.service';
import { IGridSettings, IColumnSettings } from '../../stuctures/screens/grid/i-grid-settings';
import { ICommandButton } from '../../stuctures/i-command-button';
import { IGridResult } from '../../stuctures/screens/grid/i-grid-result';
import { ObjectHelper } from '../../common/object-helper';
import { IGridRequest } from '../../stuctures/screens/requests/i-requests-base';
import { ViewTypeEnum } from '../../stuctures/screens/i-view-type';
import { UiNotificationService } from '../../common/ui-notification.service';
import { EntityType } from '../../stuctures/screens/i-base-model';



@Component({
  selector: 'app-generic-grid',
  templateUrl: './generic-grid.component.html',
  styleUrls: ['./generic-grid.component.css']
})
export class GenericGridComponent implements OnInit {
  @ViewChild('gridCellTemplate_Date') gridCellTemplate_Date: TemplateRef<any>;
  @ViewChild('gridCellTemplate_Currency') gridCellTemplate_Currency: TemplateRef<any>;
  @ViewChild('groupHeaderTemplate') groupHeaderTemplate: TemplateRef<any>;
  @ViewChild('groupHeaderTemplate_Date') groupHeaderTemplate_Date: TemplateRef<any>;
  @ViewChild('groupHeaderTemplate_Currency') groupHeaderTemplate_Currency: TemplateRef<any>;
  @ViewChild('groupFooterTemplate') groupFooterTemplate: TemplateRef<any>;
  @ViewChild('groupFooterTemplate_Date') groupFooterTemplate_Date: TemplateRef<any>;
  @ViewChild('groupFooterTemplate_Currency') groupFooterTemplate_Currency: TemplateRef<any>;
  @ViewChild('gridFooterTemplate') gridFooterTemplate: TemplateRef<any>;
  @ViewChild('gridFooterTemplate_Date') gridFooterTemplate_Date: TemplateRef<any>;
  @ViewChild('gridFooterTemplate_Currency') gridFooterTemplate_Currency: TemplateRef<any>;
  @ViewChild('itemListTemplate') itemListTemplate: TemplateRef<any>;
  @ViewChild('filterRowTemplateDropDown') filterRowTemplateDropDown: TemplateRef<any>;
  @ViewChild('filterMenuTemplateMultiSelect') filterMenuTemplateMultiSelect: TemplateRef<any>;

  @Input() settings: IGridSettings;
  @Input() public commandButtons: ICommandButton[];
  @Input() public filterValueSourceItem?: any;

  constructor(private _gridService: GridService, private _uiNotificationService: UiNotificationService) { }

  public gridButtons: ICommandButton[];
  public aggregates: any[];
  public items: IGridResult;
  public aggregateResult: any = undefined;
  public gridSettings: IGridSettings;
  public state: DataSourceRequestState;

  public getTemplate(templateName: string) {
    return this[templateName];
  }

  public getContext(dataItem: any, columnSetting: IColumnSettings) {
    return {
      $implicit: dataItem,
      field: columnSetting.field
    }
  }

  public getRowFilterContext(filter: CompositeFilterDescriptor, columnSetting: IColumnSettings) {
    return {
      $implicit: filter,
      isPrimitive: columnSetting.filterRowTemplate.isPrimitive,
      field: columnSetting.field,
      filterRowTemplate: columnSetting.filterRowTemplate,
      textField: columnSetting.filterRowTemplate.textField,
      valueField: columnSetting.filterRowTemplate.valueField
    }
  }

  public getMenuFilterContext(filter: CompositeFilterDescriptor, columnSetting: IColumnSettings, filterService: FilterService) {
    return {
      $implicit: filter,
      filterService: filterService,
      isPrimitive: columnSetting.filterMenuTemplate.isPrimitive,
      field: columnSetting.field,
      filterMenuTemplate: columnSetting.filterMenuTemplate,
      textField: columnSetting.filterMenuTemplate.textField,
      valueField: columnSetting.filterMenuTemplate.valueField
    }
  }

  public getListContext(dataItem: any, columnSetting: IColumnSettings) {
    return {
      $implicit: dataItem,
      field: columnSetting.field,
      displayMember: columnSetting.cellListTemplate.displayMember
    }
  }

  public getGroupFooterContext(groupAggregates: any, columnSetting: IColumnSettings) {
    return {
      field: columnSetting.field,
      columnTitle: columnSetting.title,
      aggregates: columnSetting.groupFooterTemplate.aggregates,
      groupAggregates: groupAggregates
    }
  }

  public getGroupHeaderContext(groupAggregates: any, columnSetting: IColumnSettings) {
    return {
      field: columnSetting.field,
      columnTitle: columnSetting.title,
      aggregates: columnSetting.groupHeaderTemplate.aggregates,
      groupAggregates: groupAggregates.aggregates
    }
  }

  public getGridFooterContext(gridAggregateResult: any, columnSetting: IColumnSettings) {
    return {
      field: columnSetting.field,
      columnTitle: columnSetting.title,
      aggregates: columnSetting.groupFooterTemplate.aggregates,
      gridAggregateResult: gridAggregateResult
    }
  }

  public dataStateChange(state: DataStateChangeEvent): void {
    if (state && state.group) {
      state.group.map(group => group.aggregates = this.gridSettings.aggregates);
    }

    this.state = state;
    this.state.aggregates = this.gridSettings.aggregates;

    this.updateGrid();
  }

  public filterChange(filter: CompositeFilterDescriptor): void {
  }

  ngOnInit() {
    this.gridSettings = this.settings;
    this.gridButtons = this.commandButtons.filter(btn => btn.gridCommandButton === true);
    this.state = {
      skip: this.gridSettings.state ? this.gridSettings.state.skip : null,
      take: this.gridSettings.state ? this.gridSettings.state.take : null,
      filter: this.gridSettings.state && this.gridSettings.state.filterGroup
        ? ObjectHelper.getCompositeFilter(this.gridSettings.state.filterGroup, this.filterValueSourceItem)
        : null,
      group: this.settings.state && this.settings.state.group
        ? ObjectHelper.getGroupDescriptors(this.settings.state.group)
        : null,
      aggregates: this.gridSettings.aggregates
    }

    this.updateGrid();
  }

  updateGrid(): void {
    this._gridService.fetch(this.state, this.gridSettings.requestDetails).subscribe(r => {
      this.items = r;
      this.aggregateResult = r.aggregateResult;
      //console.log("All Results: " + JSON.stringify(this.items));
    });
  }

  gridCommandClick(item: EntityType, button: ICommandButton) {
    this.doPost({
      filterGroup: ObjectHelper.updateFilterValueFields(this.gridSettings.itemFilter, item),
      viewType: ViewTypeEnum.Grid,
      commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
    });
  }

  formCommandClick(button: ICommandButton) {
    this._uiNotificationService.navigateNext({
      viewType: ViewTypeEnum.Grid,
      commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
    });
  }

  doPost(request: IGridRequest): void {
    this._uiNotificationService.navigateNext(request);
  }

}

