import { Component, OnInit, Input } from '@angular/core';
import { GenericService } from '../../http/generic.service';
import { IDetailDropDownTemplate } from 'src/app/stuctures/screens/detail/i-detail-form-settings';
import { SettingsService } from '../../http/settings.service';

@Component({
  selector: '[app-display-dropdown-value]',
  templateUrl: './display-dropdown-value.component.html',
  styleUrls: ['./display-dropdown-value.component.css']
})
export class DisplayDropdownValueComponent implements OnInit {

  @Input() public valueTextTemplate: IDetailDropDownTemplate;
  @Input() public filterValueSourceItem?: any;
  @Input() public selectedValue: any;
  @Input() public modelType?: any;

  constructor(private _genericService: GenericService, private _settingsService: SettingsService) { }

  public selectedText: string;
  public data: any;

  ngOnInit() {
    this.getDropDownData();
  }

  getDropDownData(): any
  {
    if (!(this.filterValueSourceItem && this.valueTextTemplate.reloadItemsFlowName))
    {
      this.getList(this.valueTextTemplate.textAndValueSelector);
      return;
    }

    this._settingsService.getSelector({ entity: Object.assign({typeFullName: this.modelType}, this.filterValueSourceItem), reloadItemsFlowName: this.valueTextTemplate.reloadItemsFlowName}).subscribe(selectorResponse => {
      if (selectorResponse.success)
      {
        this.getList(selectorResponse.selector);
      }
    });
  }

  getList(selector: any) : void{
    this._genericService.getList(this.valueTextTemplate.requestDetails, selector).subscribe(r =>
      {
        this.data = r;
        if(this.data && this.data.length)
        {
          let selected = this.data.find(i => i[this.valueTextTemplate.valueField] == this.selectedValue);
          this.selectedText = selected ? selected[this.valueTextTemplate.textField] : "";
        }
        console.log("this.filterCellTemplate Returned:   " + JSON.stringify(this.data));
      });
  }

}
