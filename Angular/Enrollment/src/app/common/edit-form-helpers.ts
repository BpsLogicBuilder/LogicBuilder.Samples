import { ObjectHelper } from "./object-helper";
import { abstractControlKind, IFormItemSetting, IFormGroupSettings, IFormGroupArraySettings, IFormGroupData, formTypeEnum, IFormControlSettings, IGroupSettings } from "../stuctures/screens/edit/i-edit-form-settings";
import { FormGroup, FormArray, AbstractControl, FormBuilder, FormControl } from "@angular/forms";

export class EditFormHelpers
{
    static findFormGroupData(formControl: AbstractControl, formGroup: FormGroup, fieldSettings: IFormItemSetting[], formGroupData: IFormGroupData)
    {
        for (let setting of fieldSettings)
        {
            if ((setting.abstractControlType === abstractControlKind.formControl
                || setting.abstractControlType === abstractControlKind.multiSelectFormControl)
                && formGroup.controls[setting.field] && formGroupData)
            {
                if(formControl === formGroup.controls[setting.field])
                    return formGroupData;
            }
            else if (setting.abstractControlType == abstractControlKind.formGroup && formGroup.controls[setting.field] && formGroupData.formGroupData && formGroupData.formGroupData[setting.field])
            {
                if(formControl === formGroup.controls[setting.field])
                    return formGroupData.formGroupData[setting.field];

                let data =  EditFormHelpers.findFormGroupData(formControl, <FormGroup>formGroup.controls[setting.field], (<IFormGroupSettings>setting).fieldSettings, formGroupData.formGroupData[setting.field]);
                if (data)
                    return data;
            }
            else if (setting.abstractControlType == abstractControlKind.formGroupArray && formGroup.controls[setting.field] && formGroupData.formArrayData && formGroupData.formArrayData[setting.field])
            {
                let formGroupArray: FormArray = <FormArray>formGroup.controls[setting.field];
                if (formGroupArray.controls && formGroupArray.controls.length)
                {
                    for (let i in formGroupArray.controls)
                    {
                        let fg: FormGroup = <FormGroup>formGroupArray.controls[i];
                        let data = EditFormHelpers.findFormGroupData(formControl, fg, (<IFormGroupArraySettings>setting).fieldSettings, formGroupData.formArrayData[setting.field].formGroupDataArray[i]);
                        if (data)
                            return data;
                    }
                }
            }
        }

        return null;
    }

    static buildFormGroupData(formGroup: FormGroup, fieldSettings: IFormItemSetting[], formGroupData: IFormGroupData)
    {
        for (let setting of fieldSettings)
        {
            if ((setting.abstractControlType === abstractControlKind.formControl
                || setting.abstractControlType === abstractControlKind.multiSelectFormControl)
                && formGroup.controls[setting.field])
            {
            }
            else if (setting.abstractControlType == abstractControlKind.formGroup && formGroup.controls[setting.field])
            {
                formGroupData.formGroupData = {};
                formGroupData.formGroupData[setting.field] = { displayMessages: {} };
                EditFormHelpers.buildFormGroupData(<FormGroup>formGroup.controls[setting.field], (<IFormGroupSettings>setting).fieldSettings, formGroupData.formGroupData[setting.field]);
            }
            else if (setting.abstractControlType == abstractControlKind.formGroupArray && formGroup.controls[setting.field])
            {
                let formGroupArray: FormArray = <FormArray>formGroup.controls[setting.field];
                formGroupData.formArrayData = {};
                formGroupData.formArrayData[setting.field] = { formGroupDataArray: [] };
                if (formGroupArray.controls && formGroupArray.controls.length)
                {
                    for (let i in formGroupArray.controls)
                    {
                        let fg:FormGroup = <FormGroup>formGroupArray.controls[i];
                        formGroupData.formArrayData[setting.field].formGroupDataArray.push({ displayMessages: {} });
                        EditFormHelpers.buildFormGroupData(fg, (<IFormGroupArraySettings>setting).fieldSettings, formGroupData.formArrayData[setting.field].formGroupDataArray[i]);
                    }
                }
            }
        }
    }

    static getFormGroupData(formGroup: FormGroup, fieldSettings: IFormItemSetting[]): IFormGroupData
    {//call this on the form array's parent formGroup every time a new control is added to a form array
        let formGroupData: IFormGroupData = { displayMessages: {}};
        EditFormHelpers.buildFormGroupData(formGroup, fieldSettings, formGroupData);
        return formGroupData;
    }

    static getAbstractControl(questionSetting: IFormItemSetting, fb: FormBuilder, formGroup: FormGroup, groupSettings: IGroupSettings): AbstractControl
    {
        if (questionSetting.abstractControlType === abstractControlKind.formControl || questionSetting.abstractControlType === abstractControlKind.multiSelectFormControl)
        {
            let formControl: FormControl;
            if (questionSetting.validationSetting)
            {
                if (questionSetting.validationSetting.validators)
                {
                    let fn = ObjectHelper.getValidatorFunctions(questionSetting.validationSetting.validators);
                    formControl =  new FormControl(questionSetting.validationSetting.defaultValue, fn);
                }
                else
                {
                    formControl = new FormControl(questionSetting.validationSetting.defaultValue);
                }
            }
            else
            {
                formControl =  new FormControl();
            }

            return formControl;
        }
        else  if (questionSetting.abstractControlType === abstractControlKind.formGroup)
        {
            let fg = ObjectHelper.buildFormGroup((<IFormGroupSettings>questionSetting).fieldSettings, fb);
            return  fg;
        }
        else if (questionSetting.abstractControlType === abstractControlKind.formGroupArray) {
            return fb.array([]);
        }

        return new FormControl();
    }

    static processValidationMessages(formGroup: FormGroup, fieldSettings: IFormItemSetting[], formGroupData: IFormGroupData, validationMessages: { [key: string]: { [key: string]: string } })
    {
        for (let setting of fieldSettings)
        {
            if ((setting.abstractControlType === abstractControlKind.formControl 
                || setting.abstractControlType === abstractControlKind.multiSelectFormControl) 
                && formGroup.controls[setting.field])
            {
                const c: AbstractControl = formGroup.controls[setting.field];
                if (validationMessages[setting.field]) {
                    formGroupData.displayMessages[setting.field] = '';
                    if ((c.dirty || c.touched) && c.errors) {
                        Object.keys(c.errors).map(messageKey => {
                            if (validationMessages[setting.field][messageKey]) {
                                formGroupData.displayMessages[setting.field] += validationMessages[setting.field][messageKey] + ' ';
                            }
                        });
                    }
                }
            }
            else if (setting.abstractControlType == abstractControlKind.formGroup
                && formGroup.controls[setting.field])
            {
                const fg: FormGroup = <FormGroup>formGroup.controls[setting.field];
                EditFormHelpers.processValidationMessages(fg, (<IFormGroupSettings>setting).fieldSettings, formGroupData.formGroupData[setting.field], (<IFormGroupSettings>setting).validationMessages);
            }
            else if (setting.abstractControlType == abstractControlKind.formGroupArray 
                && formGroup.controls[setting.field])
            {
                const formGroupArray: FormArray =  <FormArray>formGroup.controls[setting.field];
                if (formGroupArray.controls && formGroupArray.controls.length)
                {
                    for (let i in formGroupArray.controls) {
                        EditFormHelpers.processValidationMessages
                        (
                            <FormGroup>formGroupArray.controls[i], 
                            (<IFormGroupArraySettings>setting).fieldSettings, 
                            formGroupData.formArrayData[setting.field].formGroupDataArray[i], 
                            (<IFormGroupArraySettings>setting).validationMessages
                        );
                     }
                }
            }
        }
    }
}