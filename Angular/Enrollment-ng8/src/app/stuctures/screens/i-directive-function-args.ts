import { FormGroup, FormBuilder } from "@angular/forms";
import { IInputForm, IDirective, IInputQuestion } from "./input-form/i-input-form";
import { IEditFormSettings, formTypeEnum, IFormItemSetting, IFormGroupData, IFormGroupSettings, IGroupSettings } from "./edit/i-edit-form-settings";

export interface IInputDirectiveFunctionArgs {
    directive: IDirective;
    formGroup: FormGroup; 
    fieldBeingUpdated: string; 
    newValue: any;
    targetControlName: string; 
    targetControlFieldSetting: IInputQuestion
    result: boolean;
    conditionalDirectives: { [key: string]: IDirective[] };
    formType: formTypeEnum;
    fb: FormBuilder;
}

export interface IHandleInputDirectiveArgs {
    directive: IDirective;
    formGroup: FormGroup; 
    fieldBeingUpdated: string; 
    newValue: any;
    targetControlName: string; 
    targetControlFieldSetting: IInputQuestion
    conditionalDirectives: { [key: string]: IDirective[] };
    formType: formTypeEnum;
    fb: FormBuilder;
}

export interface IEditDirectiveFunctionArgs {
    directive: IDirective;
    formGroup: FormGroup; 
    groupSettings: IGroupSettings;
    fieldBeingUpdated: string; 
    newValue: any;
    targetControlName: string; 
    targetControlFieldSetting: IFormItemSetting;
    result: boolean;
    conditionalDirectives: { [key: string]: IDirective[] };
    formType: formTypeEnum;
    fb: FormBuilder;
}

export interface IHandleEditDirectiveArgs {
    directive: IDirective;
    formGroup: FormGroup; 
    groupSettings: IGroupSettings;
    fieldBeingUpdated: string; 
    newValue: any;
    targetControlName: string; 
    targetControlFieldSetting: IFormItemSetting;
    conditionalDirectives: { [key: string]: IDirective[] };
    formType: formTypeEnum;
    fb: FormBuilder;
}