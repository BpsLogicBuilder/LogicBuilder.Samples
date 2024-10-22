import { Component, OnInit, AfterViewInit, ViewChild, TemplateRef, Input } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, UntypedFormArray } from '@angular/forms';
import { GenericService } from '../../http/generic.service';
import { DateService } from '../../common/date.service';
import { UiNotificationService } from '../../common/ui-notification.service';
import { ListManagerService } from '../../common/list-manager.service';
import { abstractControlKind, IEditFormSettings, IFormControlSettings, IFormGroupData, IFormGroupArraySettings, IFormItemSetting, IFormGroupSettings } from '../../stuctures/screens/edit/i-edit-form-settings';
import { EntityType } from '../../stuctures/screens/i-base-model';
import { ICommandButton } from '../../stuctures/i-command-button';
import { ObjectHelper } from '../../common/object-helper';
import { debounceTime } from 'rxjs/operators';
import { ViewTypeEnum } from '../../stuctures/screens/i-view-type';
import { EditFormHelpers } from 'src/app/common/edit-form-helpers';
import { EntityStateType } from 'src/app/stuctures/screens/entity-state-type';
import { Directives } from 'src/app/common/directives';

@Component({
  selector: 'app-generic-edit',
  templateUrl: './generic-edit.component.html',
  styleUrls: ['./generic-edit.component.css']
})
export class GenericEditComponent implements OnInit, AfterViewInit
{
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

  constructor(private fb: UntypedFormBuilder,
    private _genericService: GenericService,
    private _dateService: DateService,
    private _uiNotificationService: UiNotificationService,
    private _listManagerService: ListManagerService) { }

  public controlType = abstractControlKind;
  public entity: EntityType;
  public errorMessage: string;
  public itemForm: UntypedFormGroup;
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

  public getFieldContext(fieldSetting: IFormControlSettings, formGroup: UntypedFormGroup, index: string)
  {
    let fGroupData = EditFormHelpers.findFormGroupData(formGroup.controls[fieldSetting.field], this.itemForm, this.formSettings.fieldSettings, this.formGroupData) || { displayMessages: {}};
    return {
      formGroup: formGroup,
      fieldSetting: fieldSetting,
      groupData: fGroupData,
      strIndex: index || ""
    }
  }

  public getFormGroupContext(fieldSetting: IFormItemSetting, formGroup: UntypedFormGroup, index: string)
  {
    return {
      formGroup: formGroup.controls[fieldSetting.field],
      fieldSetting: fieldSetting,
      strIndex: index || ""
    }
  }

  public getGroupBoxContext(fieldSetting: IFormItemSetting, formGroup: UntypedFormGroup, index: string)
  {
    return {
      formGroup: formGroup,
      fieldSetting: fieldSetting,
      strIndex: index || ""
    }
  }

  public getFormArrayContext(fieldSetting: IFormGroupSettings, formGroup: UntypedFormGroup, index: string)
  {
    return {
      formGroup: formGroup,
      arrayControl: formGroup.controls[fieldSetting.field],
      arrayName: fieldSetting.field,
      fieldSetting: fieldSetting,
      strIndex: index || ""
    }
  }

  public getDropDownFieldContext(fieldSetting: IFormControlSettings, formGroup: UntypedFormGroup, index: string)
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

  public getMultiSelectFieldContext(fieldSetting: IFormControlSettings, formGroup: UntypedFormGroup, index: string)
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

  public addFormArrayItem(formArray: UntypedFormArray, arraySettings: IFormGroupArraySettings): void {
    let formGroup: UntypedFormGroup = ObjectHelper.buildFormGroup(arraySettings.fieldSettings, this.fb);
    formArray.push(formGroup);

    Directives.watchFields(arraySettings, formGroup, arraySettings.conditionalDirectives, this.fb);
    formGroup.patchValue(ObjectHelper.getPatchObject(formGroup, arraySettings.fieldSettings, null, this._dateService, this.fb));
  }

  public deleteFormArrayItem(formArray: UntypedFormArray, index: number): void {
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

    this.getItem();
  }

  ngAfterViewInit(): void
  {
    this.itemForm.valueChanges.pipe(debounceTime(500)).subscribe(value =>
    {
      this.formGroupData = EditFormHelpers.getFormGroupData(this.itemForm, this.formSettings.fieldSettings);
      EditFormHelpers.processValidationMessages(this.itemForm, this.formSettings.fieldSettings, this.formGroupData, this.formSettings.validationMessages);
    });
  }

  getItem()
  {
    this._genericService.getItem(this.formSettings.requestDetails).subscribe(
      itm =>
      {
        this.entity = itm;
        if (!this.entity)
        {
          this.isInsert = true;
          this.entity = { entityState: EntityStateType.Added, typeFullName: this.formSettings.requestDetails.modelType};
        }

        //Getting the patch object may add form array items based on data so create thise first
        let patchObject = ObjectHelper.getPatchObject(this.itemForm, this.formSettings.fieldSettings, this.entity, this._dateService, this.fb);
        //Now watch the itemForm fields
        Directives.watchFields(this.formSettings, this.itemForm, this.formSettings.conditionalDirectives, this.fb);
        //Then directives will take effect as the form values changes
        this.itemForm.patchValue(patchObject);
      },
      error => this.errorMessage = <any>error);
  }

  submitClick(button: ICommandButton): void
  {
    if (!(this.itemForm.dirty && this.itemForm.valid))
      return;

    let itm: EntityType = Object.assign({}, this.entity, this.itemForm.value);
    itm = this._listManagerService.updateFormEntityState(itm, this.entity, this.itemForm, this.formSettings.fieldSettings, this.isInsert);

    this._genericService.updateItem(itm, this.formSettings.requestDetails)
      .subscribe(response =>
      {
        this.navigateNext(button);
      },
        error => this.errorMessage = <any>error);
  }

  navigateNext(button: ICommandButton)
  {
    this._uiNotificationService.navigateNext({
      viewType: ViewTypeEnum.Edit,
      commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
    });
  }
}
