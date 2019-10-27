import { Component, OnInit, Input } from '@angular/core';
import { IDropDownTemplate } from 'src/app/stuctures/screens/edit/i-edit-form-settings';
import { GridService } from 'src/app/http/grid.service';
import { DataSourceRequestState } from '@progress/kendo-data-query';
import { ObjectHelper } from 'src/app/common/object-helper';
import { IDetailDropDownTemplate } from 'src/app/stuctures/screens/detail/i-detail-form-settings';

@Component({
  selector: '[app-display-dropdown-value]',
  templateUrl: './display-dropdown-value.component.html',
  styleUrls: ['./display-dropdown-value.component.css']
})
export class DisplayDropdownValueComponent implements OnInit {

  @Input() public valueTextTemplate: IDetailDropDownTemplate;
  @Input() public filterValueSourceItem?: any;
  @Input() public selectedValue: any;

  constructor(private _gridService: GridService) { }

  public selectedText: string;
  public data: any;

  ngOnInit() {
    this.getDropDownData();
  }

  getDropDownData(): any
  {
    let state: DataSourceRequestState = {
      skip: this.valueTextTemplate.state ? this.valueTextTemplate.state.skip : null,
      take: this.valueTextTemplate.state ? this.valueTextTemplate.state.take : null,
      sort: this.valueTextTemplate.state && this.valueTextTemplate.state.sort ? ObjectHelper.getSortDescriptors(this.valueTextTemplate.state.sort) : null,
      filter: this.valueTextTemplate.state && this.valueTextTemplate.state.filterGroup
        ? ObjectHelper.getCompositeFilter(this.valueTextTemplate.state.filterGroup, this.filterValueSourceItem)
        : null
    };

    this._gridService.getFilterData(state, this.valueTextTemplate.requestDetails).subscribe(r =>
    {
      this.data = r;
      
      let selected = this.data.find(i => i[this.valueTextTemplate.valueField] == this.selectedValue);
      this.selectedText = selected ? selected[this.valueTextTemplate.textField] : "";
      console.log("this.filterCellTemplate Returned:   " + JSON.stringify(this.data));
    });
  }

}
