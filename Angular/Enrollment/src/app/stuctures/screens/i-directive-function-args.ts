import { UntypedFormGroup, UntypedFormBuilder } from "@angular/forms";
import { IEditFormSettings, IFormItemSetting, IFormGroupData, IFormGroupSettings, IGroupSettings, IDirective } from "./edit/i-edit-form-settings";

export interface IEditDirectiveFunctionArgs {
    directive: IDirective;
    formGroup: UntypedFormGroup; 
    groupSettings: IGroupSettings;
    fieldBeingUpdated: string; 
    newValue: any;
    targetControlName: string; 
    targetControlFieldSetting: IFormItemSetting;
    result: boolean;
    conditionalDirectives: { [key: string]: IDirective[] };
    fb: UntypedFormBuilder;
}

export interface IHandleEditDirectiveArgs {
    directive: IDirective;
    formGroup: UntypedFormGroup; 
    groupSettings: IGroupSettings;
    fieldBeingUpdated: string; 
    newValue: any;
    targetControlName: string; 
    targetControlFieldSetting: IFormItemSetting;
    conditionalDirectives: { [key: string]: IDirective[] };
    fb: UntypedFormBuilder;
}