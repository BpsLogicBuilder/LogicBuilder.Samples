import { Component, OnInit, ViewChild, Input, TemplateRef } from '@angular/core';
import { IDetailFormSettings, detailKind, IDetailFieldSetting, IDetailListSetting } from '../../stuctures/screens/detail/i-detail-form-settings';
import { ICommandButton } from '../../stuctures/i-command-button';
import { GenericService } from '../../http/generic.service';
import { UiNotificationService } from '../../common/ui-notification.service';
import { EntityType } from '../../stuctures/screens/i-base-model';
import { ViewTypeEnum } from '../../stuctures/screens/i-view-type';

@Component({
  selector: 'app-generic-delete',
  templateUrl: './generic-delete.component.html',
  styleUrls: ['./generic-delete.component.css']
})
export class GenericDeleteComponent implements OnInit {
  @ViewChild('currencyTemplate', { static: true }) currencyTemplate: TemplateRef<any>;
  @ViewChild('textTemplate', { static: true }) textTemplate: TemplateRef<any>;
  @ViewChild('dateTemplate', { static: true }) dateTemplate: TemplateRef<any>;
  @ViewChild('listTemplate', { static: true }) listTemplate: TemplateRef<any>;

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
    this._genericService.getItem(this.formSettings.requestDetails).subscribe(
      itm => {
        this.entity = itm;
      },
      error => this.errorMessage = <any>error);
  }

  submitClick(button: ICommandButton) {
    this._genericService.deleteItem(this.entity, this.formSettings.requestDetails)
      .subscribe(response => {
        this._uiNotificationService.navigateNext({
          viewType: ViewTypeEnum.Delete,
          commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
        });
      },
        error => this.errorMessage = <any>error);
  }

  navigateNext(button: ICommandButton) {
    this._uiNotificationService.navigateNext({
      viewType: ViewTypeEnum.Delete,
      commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
    });
  }
}
