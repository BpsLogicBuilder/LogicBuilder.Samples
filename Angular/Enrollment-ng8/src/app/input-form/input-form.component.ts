import { Component, OnInit, ViewChild, TemplateRef, Input, AfterViewInit } from '@angular/core';
import { IInputForm, IInputQuestion, IInputColumn, IInputRow, IDirective } from '../stuctures/screens/input-form/i-input-form';
import { ICommandButton } from '../stuctures/i-command-button';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DateService } from '../common/date.service';
import { UiNotificationService } from '../common/ui-notification.service';
import { ListManagerService } from '../common/list-manager.service';
import { abstractControlKind, formTypeEnum } from '../stuctures/screens/edit/i-edit-form-settings';
import { GenericValidator } from '../common/generic-validator';
import { debounceTime } from 'rxjs/operators';
import { ObjectHelper } from '../common/object-helper';
import { ViewTypeEnum } from '../stuctures/screens/i-view-type';
import { IInputFormRequest } from '../stuctures/screens/requests/i-requests-base';
import { InputFormHelper } from '../common/input-form-helpers';
import { Directives } from '../common/directives';

@Component({
  selector: 'app-input-form',
  templateUrl: './input-form.component.html',
  styleUrls: ['./input-form.component.css']
})
export class InputFormComponent implements OnInit, AfterViewInit
{
  @ViewChild('labelTemplate', { static: true }) labelTemplate: TemplateRef<any>;
  @ViewChild('textTemplate', { static: true }) textTemplate: TemplateRef<any>;
  @ViewChild('phoneTemplate', { static: false }) phoneTemplate: TemplateRef<any>;
  @ViewChild('emailTemplate', { static: false }) emailTemplate: TemplateRef<any>;
  @ViewChild('zipCodeTemplate', { static: false }) zipCodeTemplate: TemplateRef<any>;
  @ViewChild('dateTemplate', { static: true }) dateTemplate: TemplateRef<any>;
  @ViewChild('dropDownTemplate', { static: true }) dropDownTemplate: TemplateRef<any>;
  @ViewChild('multiSelectTemplate', { static: true }) multiSelectTemplate: TemplateRef<any>;
  @ViewChild('checkBoxTemplate', { static: true }) checkBoxTemplate: TemplateRef<any>;
  @ViewChild('formGroupTemplate', { static: false }) formGroupTemplate: TemplateRef<any>;
  @ViewChild('rowTemplate', { static: true }) rowTemplate: TemplateRef<any>;
  @ViewChild('columnTemplate', { static: true }) columnTemplate: TemplateRef<any>;
  @ViewChild('hiddenTemplate', { static: true }) hiddenTemplate: TemplateRef<any>;

  @Input() settings: IInputForm;
  @Input() public commandButtons: ICommandButton[];

  constructor(private fb: FormBuilder,
    private _dateService: DateService,
    private _uiNotificationService: UiNotificationService,
    private _listManagerService: ListManagerService) { }

  public controlType = abstractControlKind;
  public errorMessage: string;
  public itemForm: FormGroup;
  public displayMessage: { [key: string]: string } = {};

  private validationMessages: { [key: string]: { [key: string]: string } };
  private conditionalDirectives: { [key: string]: IDirective[] };
  private genericValidator: GenericValidator;
  private formSettings: IInputForm;

  public getTemplate(templateName: string)
  {
    return this[templateName];
  }

  public getFieldContext(questionSetting: IInputQuestion, formGroup: FormGroup)
  {
    return {
      $implicit: questionSetting,
      formGroup: formGroup
    }
  }

  public getColumnContext(columnSetting: IInputColumn, formGroup: FormGroup)
  {
    return {
      $implicit: columnSetting,
      formGroup: formGroup
    }
  }

  public getRowContext(rowSetting: IInputRow, formGroup: FormGroup)
  {
    return {
      $implicit: rowSetting,
      formGroup: formGroup
    }
  }

  public getDropDownFieldContext(questionSetting: IInputQuestion, formGroup: FormGroup)
  {
    return {
      $implicit: questionSetting,
      formGroup: formGroup,
      dropDownTemplate: questionSetting.dropDownTemplate,
      textField: questionSetting.dropDownTemplate.textField,
      valueField: questionSetting.dropDownTemplate.valueField
    }
  }

  public getMultiSelectFieldContext(questionSetting: IInputQuestion, formGroup: FormGroup)
  {
    return {
      $implicit: questionSetting,
      formGroup: formGroup,
      multiSelectTemplate: questionSetting.multiSelectTemplate,
      textField: questionSetting.multiSelectTemplate.textField,
      valueField: questionSetting.multiSelectTemplate.valueField
    }
  }

  ngOnInit()
  {
    this.formSettings = this.settings;
    this.validationMessages = this.formSettings.validationMessages;
    this.conditionalDirectives = this.formSettings.conditionalDirectives;
    this.genericValidator = new GenericValidator(this.validationMessages);
    this.itemForm = InputFormHelper.buildInputFormGroup({}, this.formSettings, this.fb);

    Object.keys(this.conditionalDirectives).forEach(targetField =>
    {
      this.conditionalDirectives[targetField].forEach(directive =>
      {
        let fieldsToWatch: string[] = ObjectHelper.getConditionsFieldsToWatch(directive.conditionGroup, this.itemForm.value);
        fieldsToWatch.forEach(fieldBeingUpdated =>
        {
          this.itemForm.get(fieldBeingUpdated).valueChanges.subscribe(value => Directives.handleDirective({
            directive: directive,
            formGroup: this.itemForm,
            fieldBeingUpdated: fieldBeingUpdated,
            newValue: value,
            targetControlName: targetField,
            targetControlFieldSetting: ObjectHelper.findQuestionSetting(targetField, this.formSettings),
            conditionalDirectives:  this.conditionalDirectives,
            formType: formTypeEnum.inputForm,
            fb: this.fb
          }));
        });
      });
    });

    this.itemForm.patchValue(ObjectHelper.getPatchObjectFromInputForm(this.formSettings, this._dateService));
  }

  ngAfterViewInit(): void
  {
    this.itemForm.valueChanges.pipe(debounceTime(500)).subscribe(value =>
    {
      this.displayMessage = this.genericValidator.processMessages(this.itemForm);
      console.log(JSON.stringify(this.displayMessage));
    });
  }

  saveClick(button: ICommandButton)
  {

    if (!(this.itemForm.dirty && this.itemForm.valid))
      return;

    this.formSettings = InputFormHelper.updateInputFormFromPatchObject(this.itemForm.value, this.formSettings)
    this.doPost({
      form: this.formSettings,
      viewType: ViewTypeEnum.InputForm,
      commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
    });
  }

  cancelClick(button: ICommandButton)
  {
    this._uiNotificationService.navigateNext({
      viewType: ViewTypeEnum.InputForm,
      commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
    });
  }

  doPost(request: IInputFormRequest): void
  {
    this._uiNotificationService.navigateNext(request);
  }
}
