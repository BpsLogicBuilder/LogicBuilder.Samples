import { IRequestDetails } from "../i-request-details";

export interface IDetailFormSettings {
    title: string;
    displayField: string;
    requestDetails: IRequestDetails;
    fieldSettings?: IDetailItem[];
}

export interface IDetailGroupSetting extends IDetailItem {
    field: string;
    title: string;
    fields: IDetailItem[];
    groupTemplate: IDetailGroupTemplate;
}

export interface IDetailListSetting extends IDetailItem {
    field: string;
    title: string;
    fields: IDetailItem[];
    listTemplate: IDetailListTemplate;
}

export interface IDetailFieldSetting extends IDetailItem  {
    field: string;
    title: string;
    type: 'text' | 'numeric' | 'boolean' | 'date';
    fieldTemplate?: IDetailFieldTemplate;
    valueTextTemplate?: IDetailDropDownTemplate;
}

export interface IDetailItem  {
    detailType: detailKind;
    [x: string]: any;
}

export enum detailKind {
    field = 0,
    group = 1,
    list = 2
}

export interface IDetailFieldTemplate {
    templateName: string;
}

export interface IDetailDropDownTemplate {
    templateName: string;
    placeHolderText: string;
    textField: string;
    valueField: string;
    textAndValueSelector: any;
    requestDetails: IRequestDetails;
    reloadItemsFlowName?: string;
}

export interface IDetailGroupTemplate {
    templateName: string;
}

export interface IDetailListTemplate {
    templateName: string;
}