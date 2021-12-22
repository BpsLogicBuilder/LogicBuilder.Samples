import { IDataRequestState } from "../i-data-request-state";
import { IRequestDetails } from "../i-request-details";
import { IFormValidationSetting, ITextFieldTemplate, IDropDownTemplate, IMultiSelectTemplate } from "../edit/i-edit-form-settings";

export interface IInputForm {
    title: string;
    validationMessages?: { [key: string]: { [key: string]: string } };
    conditionalDirectives?: { [key: string]: IDirective[] };
    rows: IInputRow[];
}

export interface IInputRow {
    id: number;
    title: string;
    showTitle: boolean;
    toolTipText: string;
    columns: IInputColumn[];
}

export interface IInputColumn {
    id: number;
    title: string;
    showTitle: boolean;
    columnShare: string;
    toolTipText: string;
    questions?: IInputQuestion[];
    rows?: IInputRow[];
}

export interface IInputQuestion {
    currentValue: any;
    typeString: string;
    id: number;
    variableName: string;
    variableId: string;
    text: string;
    classAttribute: string;
    toolTipText: string;
    htmlType: string;
    directives?: IDirective[];
    readOnly?: boolean;
    errors?: string[];
    hasErrors: boolean;

    placeholder: string;
    textTemplate?: ITextFieldTemplate;
    dropDownTemplate?: IDropDownTemplate;
    multiSelectTemplate?: IMultiSelectTemplate;
    validationSetting?: IFormValidationSetting;
    unchangedValidationSetting?: IFormValidationSetting;
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