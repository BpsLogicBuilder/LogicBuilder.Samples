import { Component, OnInit, Input, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';
import { ObjectHelper } from '../../common/object-helper';
import { DataSourceRequestState } from '@progress/kendo-data-query';
import { IMultiSelectTemplate } from '../../stuctures/screens/edit/i-edit-form-settings';
import { GridService } from '../../http/grid.service';

@Component({
  selector: 'app-form-field-multiselect',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => FormFieldMultiselectComponent),
    }
  ],
  templateUrl: './form-field-multiselect.component.html',
  styleUrls: ['./form-field-multiselect.component.css']
})
export class FormFieldMultiselectComponent implements OnInit, ControlValueAccessor {

  @Input() public multiSelectTemplate: IMultiSelectTemplate;
  @Input() public textField: string;
  @Input() public valueField: string;
  @Input() public filterValueSourceItem?: any;

  constructor(private _gridService: GridService) {
    this.onTouched = () => { };
    this.onChange = (_: any) => {};
    this.disabled = false;
  }

  public data: any;
  public placeholder: string;
  public onChange: Function; 
  
  private selectedItems: any;
  private disabled: boolean;
  private onTouched: Function;

  ngOnInit() {
    this.getDropDownData();
    this.placeholder = this.multiSelectTemplate.placeHolderText;
  }

  writeValue(obj: any): void {
    this.selectedItems = obj;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn; 
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn; 
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  public get selectedValues(): any {
    return this.selectedItems;
  }

  public onValueChange(value) {
    console.log("valueChange : ", value);
  }

  getDropDownData(): any {
    let state: DataSourceRequestState = {
      skip: this.multiSelectTemplate.state ? this.multiSelectTemplate.state.skip : null,
      take: this.multiSelectTemplate.state ? this.multiSelectTemplate.state.take : null,
      sort: this.multiSelectTemplate.state && this.multiSelectTemplate.state.sort ? ObjectHelper.getSortDescriptors(this.multiSelectTemplate.state.sort) : null,
      filter: this.multiSelectTemplate.state && this.multiSelectTemplate.state.filterGroup
        ? ObjectHelper.getCompositeFilter(this.multiSelectTemplate.state.filterGroup, this.filterValueSourceItem)
        : null
    }

    this._gridService.getFilterData(state, this.multiSelectTemplate.requestDetails).subscribe(r => {
      this.data = r;
      console.log("this.multiSelectTemplate Returned:   " + JSON.stringify(this.data));
      console.log("this.textField:   " + JSON.stringify(this.textField));
      console.log("this.valueField:   " + JSON.stringify(this.valueField));
    });
  }
}
