import { Component, OnInit, Input } from '@angular/core';
import { BaseFilterCellComponent, FilterService } from '@progress/kendo-angular-grid';
import { GenericService } from '../../http/generic.service';
import { CompositeFilterDescriptor } from '@progress/kendo-data-query';

@Component({
  selector: 'app-grid-column-dropdown-filter',
  templateUrl: './grid-column-dropdown-filter.component.html',
  styleUrls: ['./grid-column-dropdown-filter.component.css']
})
export class GridColumnDropdownFilterComponent extends BaseFilterCellComponent implements OnInit {

  constructor(filterService: FilterService, private _genericService: GenericService) { 
    super(filterService);
  }

  @Input() public filter: CompositeFilterDescriptor;
  @Input() public filterRowTemplate: any;
  @Input() public textField: string;
  @Input() public valueField: string;

  public data: any;

  ngOnInit() {
    this.getFilterData();
    this.setDefaultItem();
  }

  public get selectedValue(): any {
    const filter = this.filterByField(this.valueField);
    return filter ? filter.value : null;
  }

  public defaultItem: any;
  public setDefaultItem(): any {
    this.defaultItem = this.textField == this.valueField
      ? {
        [this.textField]: 'Select item...'
      }
      : {
        [this.textField]: 'Select item...',
        [this.valueField]: null
      };

    //console.log("this.defaultItem:  " + this.textField);
    //console.log("this.valueField:  " + this.valueField);
  }

  public onChange(value: any): void {
    this.applyFilter(
      value === this.defaultItem[this.valueField] ? // value of the default item
        this.removeFilter(this.valueField) : // remove the filter
        this.updateFilter({ // add a filter for the field with the value
          field: this.valueField,
          operator: 'eq',
          value: value
        })
    ); // update the root filter
  }

  getFilterData(): any {
    this._genericService.getList(this.filterRowTemplate.requestDetails, this.filterRowTemplate.textAndValueSelector).subscribe(r => {
      this.data = r;
      //console.log("this.filterCellTemplate Returned:   " + JSON.stringify(this.data));
    });
  }
}
