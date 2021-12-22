import { Component, OnInit, AfterViewInit, ViewChild, TemplateRef, Input } from '@angular/core';
import { IEditFormSettings, abstractControlKind, IFormControlSettings, IFormGroupData, IFormItemSetting, IFormGroupSettings, IFormGroupArraySettings } from '../../stuctures/screens/edit/i-edit-form-settings';
import { ICommandButton } from '../../stuctures/i-command-button';
import { FormBuilder, FormGroup, FormArray } from '@angular/forms';
import { GenericService } from '../../http/generic.service';
import { UiNotificationService } from '../../common/ui-notification.service';
import { ListManagerService } from '../../common/list-manager.service';
import { EntityType } from '../../stuctures/screens/i-base-model';
import { GenericValidator } from '../../common/generic-validator';
import { ObjectHelper } from '../../common/object-helper';
import { debounceTime } from 'rxjs/operators';
import { ViewTypeEnum } from '../../stuctures/screens/i-view-type';
import { DataSourceRequestState } from '@progress/kendo-data-query';
import { EditFormHelpers } from 'src/app/common/edit-form-helpers';
import { Directives } from 'src/app/common/directives';
import { DateService } from 'src/app/common/date.service';

@Component({
  selector: 'app-generic-create',
  templateUrl: './generic-create.component.html',
  styleUrls: ['./generic-create.component.css']
})
export class GenericCreateComponent implements OnInit, AfterViewInit {
  @ViewChild('labelTemplate', { static: true }) labelTemplate: TemplateRef<any>;
  @ViewChild('textTemplate', { static: true }) textTemplate: TemplateRef<any>;
  @ViewChild('dateTemplate', { static: true }) dateTemplate: TemplateRef<any>;
  @ViewChild('checkBoxTemplate', { static: true }) checkBoxTemplate: TemplateRef<any>;
  @ViewChild('dropDownTemplate', { static: true }) dropDownTemplate: TemplateRef<any>;
  @ViewChild('multiSelectTemplate', { static: true }) multiSelectTemplate: TemplateRef<any>;
  @ViewChild('formGroupTemplate', { static: true }) formGroupTemplate: TemplateRef<any>;
  @ViewChild('formArrayTemplate', { static: true }) formArrayTemplate: TemplateRef<any>;
  @ViewChild('hiddenTemplate', { static: true }) hiddenTemplate: TemplateRef<any>;

  @Input() settings: IEditFormSettings;
  @Input() public commandButtons: ICommandButton[];

  constructor(private fb: FormBuilder,
    private _genericService: GenericService,
    private _dateService: DateService,
    private _uiNotificationService: UiNotificationService,
    private _listManagerService: ListManagerService) { }

  public controlType = abstractControlKind;
  public entity: EntityType;
  public errorMessage: string;
  public itemForm: FormGroup;
  public formSettings: IEditFormSettings;

  private formGroupData?: IFormGroupData;
  private isInsert: boolean = false;

  public getTemplate(templateName: string)
  {
    return this[templateName];
  }

  public getNewIndex(oldStringIndex: string, index: number)
  {
    return oldStringIndex 
            ? oldStringIndex + '_' + String(index) 
            : String(index);
  }

  public getFieldContext(fieldSetting: IFormControlSettings, formGroup: FormGroup, index: string)
  {
    let fGroupData = EditFormHelpers.findFormGroupData(formGroup.controls[fieldSetting.field], this.itemForm, this.formSettings.fieldSettings, this.formGroupData) || { displayMessages: {}};
    return {
      formGroup: formGroup,
      fieldSetting: fieldSetting,
      groupData: fGroupData,
      strIndex: index || ""
    }
  }

  public getFormGroupContext(fieldSetting: IFormItemSetting, formGroup: FormGroup, index: string)
  {
    return {
      formGroup: formGroup.controls[fieldSetting.field],
      fieldSetting: fieldSetting,
      strIndex: index || ""
    }
  }

  public getFormArrayContext(fieldSetting: IFormGroupSettings, formGroup: FormGroup, index: string)
  {
    return {
      formGroup: formGroup,
      arrayControl: formGroup.controls[fieldSetting.field],
      arrayName: fieldSetting.field,
      fieldSetting: fieldSetting,
      strIndex: index || ""
    }
  }

  public getDropDownFieldContext(fieldSetting: IFormControlSettings, formGroup: FormGroup, index: string)
  {
    let fGroupData = EditFormHelpers.findFormGroupData(formGroup.controls[fieldSetting.field], this.itemForm, this.formSettings.fieldSettings, this.formGroupData) || { displayMessages: {}};

    return {
      formGroup: formGroup,
      fieldSetting: fieldSetting,
      dropDownTemplate: fieldSetting.dropDownTemplate,
      textField: fieldSetting.dropDownTemplate.textField,
      valueField: fieldSetting.dropDownTemplate.valueField,
      groupData: fGroupData,
      strIndex: index || ""
    }
  }

  public getMultiSelectFieldContext(fieldSetting: IFormControlSettings, formGroup: FormGroup, index: string)
  {
    let fGroupData = EditFormHelpers.findFormGroupData(formGroup.controls[fieldSetting.field], this.itemForm, this.formSettings.fieldSettings, this.formGroupData) || { displayMessages: {}};
    return {
      formGroup: formGroup,
      fieldSetting: fieldSetting,
      multiSelectTemplate: fieldSetting.multiSelectTemplate,
      textField: fieldSetting.multiSelectTemplate.textField,
      valueField: fieldSetting.multiSelectTemplate.valueField,
      groupData: fGroupData,
      strIndex: index || ""
    }
  }

  public addFormArrayItem(formArray: FormArray, arraySettings: IFormGroupArraySettings): void {
    let formGroup: FormGroup = ObjectHelper.buildFormGroup(arraySettings.fieldSettings, this.fb);
    formArray.push(formGroup);

    Directives.watchFields(arraySettings, formGroup, arraySettings.conditionalDirectives, this.fb);
    formGroup.patchValue(ObjectHelper.getPatchObject(formGroup, arraySettings.fieldSettings, null, this._dateService, this.fb));
  }

  public deleteFormArrayItem(formArray: FormArray, index: number): void {
    formArray.removeAt(index);
    formArray.markAsDirty();
  }

  ngOnInit()
  {
    this.formSettings = this.settings;
    this.itemForm = ObjectHelper.buildFormGroup(this.formSettings.fieldSettings, this.fb);

    Directives.watchFields(this.formSettings, this.itemForm, this.formSettings.conditionalDirectives, this.fb);
    //Don't patch value until data arrives otherwise - will have to re-insert controls with new values depending on the data for that to work
    //Why doesn't it work with watch fields.  Watch fields will show a control depending on the value of the control being watched - not the target.
    //Why not pass in value of the target control?  The target control (if it is hidden) has no value.
    //So how about the initial data loaded.  Possible - we'll have to find that object by a recursive search.
    //Seems ugly - don't know a simple way to get the full path given the Abstract control

    this.formGroupData = EditFormHelpers.getFormGroupData(this.itemForm, this.formSettings.fieldSettings);
    EditFormHelpers.processValidationMessages(this.itemForm, this.formSettings.fieldSettings, this.formGroupData, this.formSettings.validationMessages);
  }

  ngAfterViewInit(): void
  {
    this.itemForm.valueChanges.pipe(debounceTime(500)).subscribe(value =>
    {
      this.formGroupData = EditFormHelpers.getFormGroupData(this.itemForm, this.formSettings.fieldSettings);
      EditFormHelpers.processValidationMessages(this.itemForm, this.formSettings.fieldSettings, this.formGroupData, this.formSettings.validationMessages);
    });
  }

  submitClick(button: ICommandButton): void {
    if (!(this.itemForm.dirty && this.itemForm.valid))
      return;

    let itm: EntityType = Object.assign({}, this.entity, this.itemForm.value);
    itm = this._listManagerService.updateFormEntityState(itm, this.entity, this.itemForm, this.formSettings.fieldSettings, true);
    itm.typeFullName = this.formSettings.modelType;

    this._genericService.insertItem(this.getState(), this.formSettings.requestDetails, itm)
      .subscribe(response => {
        this.navigateNext(button);
      },
        error => this.errorMessage = <any>error);
  }

  navigateNext(button: ICommandButton) {
    this._uiNotificationService.navigateNext({
      viewType: ViewTypeEnum.Create,
      commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
    });
  }

  private getState(): DataSourceRequestState {
    return {
    };
  }
}
