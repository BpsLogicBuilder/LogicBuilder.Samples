import { Component, OnInit, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, FormControl } from '@angular/forms';
import { DataSourceRequestState } from '@progress/kendo-data-query';
import { ObjectHelper } from '../../common/object-helper';
import { IDropDownTemplate } from '../../stuctures/screens/edit/i-edit-form-settings';
import { GridService } from '../../http/grid.service';

@Component({
  selector: 'app-form-field-dropdown',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => FormFieldDropdownComponent),
    }
  ],
  templateUrl: './form-field-dropdown.component.html',
  styleUrls: ['./form-field-dropdown.component.css']
})
export class FormFieldDropdownComponent implements OnInit, ControlValueAccessor
{
  @Input() public dropDownTemplate: IDropDownTemplate;
  @Input() public textField: string;
  @Input() public valueField: string;
  @Input() public filterValueSourceItem?: any;

  _reload: string;
  get reload()
  {
    return this._reload;
  }

  @Input('reload')
  set reload(value: string)
  {
    let set_reload = this;
    setTimeout(function () {
      set_reload.getDropDownData();
    }, 10);
    
    this._reload = value;
    this._clear = value;
  }

  _clear: string;
  @Input('clear')
  set clear(value: string)
  {
    this.data = [];
    this._clear = value;
    this._reload = value;
  }

  get clear()
  {
    return this._clear;
  }

  constructor(private _gridService: GridService)
  {
    this.onTouched = () => { };
    this.onChange = (_: any) => {};
    this.disabled = false;
  }

  public data: any;
  public defaultItem: any;
  private selectedItem: any;
  public disabled: boolean;
  public onChange: Function;
  private onTouched: Function;
  
  ngOnInit()
  {
    this.setDefaultItem();
    this.getDropDownData();
  }

  writeValue(obj: any): void
  {
    this.selectedValue = obj;
  }

  registerOnChange(fn: any): void
  {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void
  {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void
  {
    this.disabled = isDisabled;
  }

  public get selectedValue(): any
  {
    return this.selectedItem;
  }

  public set selectedValue(value:  any)
  {//this hack means angulat gets notified when the value gets set to null.
    //The underlying value does not always change when the user changes the value from the default null item to another item in the list.
    if ((value === null || value === undefined) && (this.selectedItem === null || this.selectedItem === undefined))
    {
      if (value !== this.selectedItem)
        this.selectedItem = value;
      else
        this.selectedItem = value === undefined ? null : undefined;
    }
    else {
      this.selectedItem = value;
    }
  }

  public setDefaultItem(): any
  {
    this.defaultItem = this.textField == this.valueField
      ? {
        [this.textField]: this.dropDownTemplate.placeHolderText
      }
      : {
        [this.textField]: this.dropDownTemplate.placeHolderText,
        [this.valueField]: null
      };
  }

  getDropDownData(): any
  {
    if (this.dropDownTemplate.state 
      && this.dropDownTemplate.state.filterGroup 
      && ObjectHelper.FilterRequiresValueSource(this.dropDownTemplate.state.filterGroup)
      && !this.filterValueSourceItem)
      return;
      
    let state: DataSourceRequestState = {
      skip: this.dropDownTemplate.state ? this.dropDownTemplate.state.skip : null,
      take: this.dropDownTemplate.state ? this.dropDownTemplate.state.take : null,
      sort: this.dropDownTemplate.state && this.dropDownTemplate.state.sort ? ObjectHelper.getSortDescriptors(this.dropDownTemplate.state.sort) : null,
      filter: this.dropDownTemplate.state && this.dropDownTemplate.state.filterGroup
        ? ObjectHelper.getCompositeFilter(this.dropDownTemplate.state.filterGroup, this.filterValueSourceItem)
        : null
    };

    this._gridService.getFilterData(state, this.dropDownTemplate.requestDetails).subscribe(r =>
    {
      this.data = r;
      
      console.log("this.filterCellTemplate Returned:   " + JSON.stringify(this.data));
      console.log("this.textField:   " + JSON.stringify(this.textField));
      console.log("this.valueField:   " + JSON.stringify(this.valueField));
    });
  }
}
