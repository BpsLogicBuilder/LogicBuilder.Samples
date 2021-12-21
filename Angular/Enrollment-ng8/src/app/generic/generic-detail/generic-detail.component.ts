import { Component, OnInit, ViewChild, TemplateRef, Input } from '@angular/core';
import { GenericService } from '../../http/generic.service';
import { UiNotificationService } from '../../common/ui-notification.service';
import { ICommandButton } from '../../stuctures/i-command-button';
import { IDetailFormSettings, IDetailFieldSetting, IDetailListSetting, detailKind, IDetailGroupSetting } from '../../stuctures/screens/detail/i-detail-form-settings';
import { EntityType } from '../../stuctures/screens/i-base-model';
import { DataSourceRequestState } from '@progress/kendo-data-query';
import { ObjectHelper } from '../../common/object-helper';
import { ViewTypeEnum } from '../../stuctures/screens/i-view-type';
import { IDetailRequest } from '../../stuctures/screens/requests/i-requests-base';

@Component({
  selector: 'app-generic-detail',
  templateUrl: './generic-detail.component.html',
  styleUrls: ['./generic-detail.component.css']
})
export class GenericDetailComponent implements OnInit {
  @ViewChild('currencyTemplate', { static: true }) currencyTemplate: TemplateRef<any>;
  @ViewChild('textTemplate', { static: true }) textTemplate: TemplateRef<any>;
  @ViewChild('valueTextTemplate', { static: true }) valueTextTemplate: TemplateRef<any>;
  @ViewChild('booleanTemplate', { static: true }) booleanTemplate: TemplateRef<any>;
  @ViewChild('dateTemplate', { static: true }) dateTemplate: TemplateRef<any>;
  @ViewChild('listTemplate', { static: true }) listTemplate: TemplateRef<any>;
  @ViewChild('groupTemplate', { static: true }) groupTemplate: TemplateRef<any>;

  @Input() settings: IDetailFormSettings;
  @Input() public commandButtons: ICommandButton[];

  constructor(private _genericService: GenericService,
    private _uiNotificationService: UiNotificationService) { }

  public detailType = detailKind;
  public entity: EntityType;
  public errorMessage: string;
  public formSettings: IDetailFormSettings;

  public getTemplate(templateName: string) {
    return this[templateName];
  }

  public getFieldContext(entity: EntityType, fieldSetting: IDetailFieldSetting) {
    return {
      $implicit: entity,
      fieldSetting: fieldSetting
    }
  }

  public getGroupContext(entity: EntityType, fieldSetting: IDetailGroupSetting) {
    return {
      $implicit: entity[fieldSetting.field],
      fieldSetting: fieldSetting
    }
  }

  public getListContext(entity: EntityType, fieldSetting: IDetailListSetting) {
    return {
      $implicit: entity[fieldSetting.field],
      fieldSetting: fieldSetting
    }
  }

  ngOnInit() {
    this.formSettings = this.settings;
    this.getItem();
  }

  getItem() {
    this._genericService.getItem(this.getState(), this.formSettings.requestDetails).subscribe(
      itm => {
        this.entity = itm;
      },
      error => this.errorMessage = <any>error);
  }

  private getState(): DataSourceRequestState {
    return {
      filter: ObjectHelper.getCompositeFilter(this.formSettings.filterGroup)
    }
  }

  submitClick(button: ICommandButton) {
    this.doPost({
      filterGroup: this.formSettings.filterGroup,
      viewType: ViewTypeEnum.Detail,
      commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
    });
  }

  navigateNext(button: ICommandButton) {
    this._uiNotificationService.navigateNext({
      viewType: ViewTypeEnum.Detail,
      commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
    });
  }

  doPost(request: IDetailRequest): void {
    this._uiNotificationService.navigateNext(request);
  }
}
