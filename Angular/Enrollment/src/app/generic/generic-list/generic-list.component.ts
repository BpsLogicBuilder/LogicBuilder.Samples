import { Component, OnInit, Input, ViewChild, TemplateRef } from '@angular/core';
import { GenericService } from '../../http/generic.service';
import { UiNotificationService } from '../../common/ui-notification.service';
import { IListFormSettings } from '../../stuctures/screens/list/i-list-form-settings';
import { ICommandButton } from '../../stuctures/i-command-button';
import { IDetailFieldSetting } from '../../stuctures/screens/detail/i-detail-form-settings';
import { ViewTypeEnum } from '../../stuctures/screens/i-view-type';

@Component({
  selector: 'app-generic-list',
  templateUrl: './generic-list.component.html',
  styleUrls: ['./generic-list.component.css']
})
export class GenericListComponent implements OnInit {
  @ViewChild('textTemplate', { static: true }) textTemplate: TemplateRef<any>;
  @ViewChild('dateTemplate', { static: true }) dateTemplate: TemplateRef<any>;

  @Input() settings: IListFormSettings;
  @Input() public commandButtons: ICommandButton[];

  constructor(private _genericService: GenericService, private _uiNotificationService: UiNotificationService) { }

  public listData: any[];

  public getTemplate(templateName: string) {
    return this[templateName];
  }

  public getFieldContext(entity: any, fieldSetting: IDetailFieldSetting) {
    return {
      $implicit: entity,
      fieldSetting: fieldSetting
    }
  }

  ngOnInit() {
    this.getItems();
  }

  getItems() {
    this._genericService.getList(this.settings.requestDetails, this.settings.fieldsSelector).subscribe(r => {
      this.listData = r;
    });
  }

  navigateNext(button: ICommandButton) {
    this._uiNotificationService.navigateNext({
      viewType: ViewTypeEnum.List,
      commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
    });
  }
}
