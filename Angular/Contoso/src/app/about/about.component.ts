import { Component, OnInit, Input, ViewChild, TemplateRef } from '@angular/core';
import { GridService } from '../http/grid.service';
import { DateService } from '../common/date.service';
import { UiNotificationService } from '../common/ui-notification.service';
import { IAboutFormSettings } from '../stuctures/screens/about/i-about-form-settings';
import { ICommandButton } from '../stuctures/i-command-button';
import { DataSourceRequestState } from '@progress/kendo-data-query';
import { ObjectHelper } from '../common/object-helper';
import { IDetailFieldSetting } from '../stuctures/screens/detail/i-detail-form-settings';
import { ViewTypeEnum } from '../stuctures/screens/i-view-type';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {
  @ViewChild('textTemplate') textTemplate: TemplateRef<any>;
  @ViewChild('dateTemplate') dateTemplate: TemplateRef<any>;

  @Input() settings: IAboutFormSettings;
  @Input() public commandButtons: ICommandButton[];

  constructor(private _gridService: GridService, private _dateService: DateService, private _uiNotificationService: UiNotificationService) { }

  public aboutData: any[];

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
    let state: DataSourceRequestState = {
      skip: this.settings.state ? this.settings.state.skip : null,
      take: this.settings.state ? this.settings.state.take : null,
      filter: this.settings.state && this.settings.state.filterGroup
        ? ObjectHelper.getCompositeFilter(this.settings.state.filterGroup)
        : null,
      group: this.settings.state && this.settings.state.group
        ? ObjectHelper.getGroupDescriptors(this.settings.state.group)
        : null,
      aggregates: this.settings.state.aggregates
    }

    this._gridService.fetch(state, this.settings.requestDetails).subscribe(r => {
      this.aboutData = ObjectHelper.getAboutData(r.data, this._dateService);
    });
  }

  navigateNext(button: ICommandButton) {
    this._uiNotificationService.navigateNext({
      viewType: ViewTypeEnum.About,
      commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
    });
  }
}
