import { Component, OnInit, AfterViewInit, EventEmitter, Input, Output } from '@angular/core';
import { CompositeFilterDescriptor, distinct, filterBy, FilterDescriptor } from '@progress/kendo-data-query';
import { FilterService } from '@progress/kendo-angular-grid';
import { GenericService } from '../../http/generic.service';

@Component({
  selector: 'app-grid-column-multiselect-filter',
  templateUrl: './grid-column-multiselect-filter.component.html',
  styleUrls: ['./grid-column-multiselect-filter.component.css']
})
export class GridColumnMultiselectFilterComponent implements OnInit, AfterViewInit {
  @Input() public isPrimitive: boolean;
  @Input() public currentFilter: CompositeFilterDescriptor;
  @Input() public filterMenuTemplate: any;
  @Input() public textField;
  @Input() public valueField;
  @Input() public filterService: FilterService;
  @Input() public field: string;
  @Output() public valueChange = new EventEmitter<number[]>();

  constructor(private _genericService: GenericService) { }

  public data: any;
  public currentData: any;
  public showFilter = true;
  private value: any[] = [];

  ngOnInit() {
    this.getFilterData();
  }

  protected textAccessor = (dataItem: any) => this.isPrimitive ? dataItem : dataItem[this.textField];
  protected valueAccessor = (dataItem: any) => this.isPrimitive ? dataItem : dataItem[this.valueField];

  public ngAfterViewInit() {
  }

  public isItemSelected(item) {
    return this.value.some(x => x === this.valueAccessor(item));
  }

  public onSelectionChange(item) {
    if (this.value.some(x => x === item)) {
      this.value = this.value.filter(x => x !== item);
    } else {
      this.value.push(item);
    }

    this.filterService.filter({
      filters: this.value.map(value => ({
        field: this.field,
        operator: 'eq',
        value
      })),
      logic: 'or'
    });
  }

  public onInput(e: any) {
    this.currentData = distinct([
      ...this.currentData.filter(dataItem => this.value.some(val => val === this.valueAccessor(dataItem))),
      ...filterBy(this.data, {
        operator: 'contains',
        field: this.textField,
        value: e.target.value
      })],
      this.textField
    );
  }

  getFilterData(): any {
    this._genericService.getList(this.filterMenuTemplate.requestDetails, this.filterMenuTemplate.textAndValueSelector).subscribe(r => {
      this.data = r;
      console.log("this.MultiSelect Returned:   " + JSON.stringify(this.data));

      this.currentData = this.data;
      this.value = this.currentFilter.filters.map((f: FilterDescriptor) => f.value);

      this.showFilter = typeof this.textAccessor(this.currentData[0]) === 'string';
    });
  }
}
