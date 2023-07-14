import { IRequestDetails } from "../i-request-details";
import { IDataRequestState } from "../i-data-request-state";
import { IFilterGroup } from "../i-filter-group";

export interface IEditFormSettings extends IGroupSettings {
    title: string;
    displayField: string;
    requestDetails: IRequestDetails;
    filterGroup?: IFilterGroup;
    modelType?: string;
}

export interface IFormControlSettings extends IFormItemSetting {
    abstractControlType: abstractControlKind;
    field: string;
    domElementId: string;
    title: string;
    type: 'text' | 'numeric' | 'boolean' | 'date';

    placeholder: string;
    textTemplate?: ITextFieldTemplate;
    dropDownTemplate?: IDropDownTemplate;
    multiSelectTemplate?: IMultiSelectTemplate;
    validationSetting?: IFormValidationSetting;
    unchangedValidationSetting?: IFormValidationSetting;
}

export interface IFormValidationSetting {
    defaultValue: any;
    validators?: IValidatorDescription[];
}

export interface IValidatorDescription {
    className: string;
    functionName: string;
    arguments?: any;
}

export interface ITextFieldTemplate {
    templateName: string;
}

export interface IDropDownTemplate {
    templateName: string;
    placeHolderText: string;
    textField: string;
    valueField: string;
    textAndValueSelector: any;
    requestDetails: IRequestDetails;
    reloadItemsFlowName?: string;
    reload?: string,
    clear?: string
}

export interface IMultiSelectTemplate {
    templateName: string;
    placeHolderText: string;
    textField: string;
    valueField: string;
    textAndValueSelector: any;
    requestDetails: IRequestDetails;
    modelType?: string;
}

export interface IMultiSelectFormControlSettings extends IFormControlSettings {
    keyFields?: string[];
}

export interface IFormGroupData {
    displayMessages: { [key: string]: string };
    formGroupData?: { [key: string]: IFormGroupData };
    formArrayData?: { [key: string]: IFormArrayData };
}

export interface IFormArrayData {
    formGroupDataArray: IFormGroupData[];
}

export interface IGroupBoxSettings extends IFormItemSetting, IGroupSettings {
    title: string;
    showTile: boolean;
}

export interface IFormGroupSettings extends IFormItemSetting, IGroupSettings {
    field: string;
    title: string;
    showTile: boolean;
    modelType?: string;
}

export interface IGroupSettings {
    fieldSettings?: Array<IFormItemSetting>;
    validationMessages?: { [key: string]: { [key: string]: string } };
    conditionalDirectives?: { [key: string]: IDirective[] };
}

export interface IFormGroupArraySettings extends IFormGroupSettings {
    keyFields?: string[];
    arrayElementType?: string;
}

export interface IFormItemSetting {
    abstractControlType : abstractControlKind;
    [x: string]: any;
}

export enum abstractControlKind {
    formControl = 0,
    multiSelectFormControl = 1,
    formGroup = 2,
    formGroupArray = 3,
    groupBox = 4
}

export interface IDomainRequest {
    state: IDataRequestState;
    requestDetails: IRequestDetails;
}

export interface IDirective {
    directiveDescription: IDirectiveDescription;
    conditionGroup: IConditionGroup;
}

export interface ICondition {
    operator: string;
    leftVariable: string;
    rightVariable?: string;
    value?: any;
}

export interface IConditionGroup {
    logic: 'or' | 'and';
    conditions?: ICondition[];
    conditionGroups?: IConditionGroup[];
}

export interface IDirectiveDescription {
    className: string;
    functionName: string;
    arguments?: any;
}