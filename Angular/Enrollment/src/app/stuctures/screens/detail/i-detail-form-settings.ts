import { IRequestDetails } from "../i-request-details";
import { IFilterGroup } from "../i-filter-group";
import { IDataRequestState } from "../i-data-request-state";

export interface IDetailFormSettings {
    title: string;
    displayField: string;
    requestDetails: IRequestDetails;
    fieldSettings?: IDetailItem[];
    filterGroup?: IFilterGroup;
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
    state: IDataRequestState,
    requestDetails: IRequestDetails,
}

export interface IDetailGroupTemplate {
    templateName: string;
}

export interface IDetailListTemplate {
    templateName: string;
}