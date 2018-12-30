import { IFilterGroup } from "../i-filter-group";
import { IDataRequestState } from "../i-data-request-state";
import { IAggregate } from "../i-aggregate";
import { IRequestDetails } from "../i-request-details";

export interface IGridSettings {
    title: string;
    sortable: boolean;
    pageable: boolean;
    scrollable: string;
    groupable: boolean;
    filterable: boolean | "row" | "menu" | "menu, row";
    columns: IColumnSettings[];
    gridId?: number;
    itemFilter?: IFilterGroup;
    height?: number;
    commandColumn?: ICommandColumn;
    state?: IDataRequestState;
    aggregates?: IAggregate[];
    requestDetails?: IRequestDetails;
    detailGridSettings?: IGridSettings;
}

export interface IColumnSettings {
    field: string;
    title: string;
    type: 'text' | 'numeric' | 'boolean' | 'date';
    groupable?: boolean;
    width?: number;
    format?: string;
    filter?: 'text' | 'numeric' | 'boolean' | 'date';
    filterRowTemplate?: IFilterTemplate;
    filterMenuTemplate?: IFilterTemplate;
    cellTemplate?: ICellTemplate;
    cellListTemplate?: ICellListTemplate;
    groupHeaderTemplate?: IAggregateTemplate;
    groupFooterTemplate?: IAggregateTemplate;
    gridFooterTemplate?: IAggregateTemplate;
}

export interface ICellTemplate {
    templateName: string;
}

export interface ICellListTemplate {
    templateName: string;
    displayMember: string;
}

export interface IAggregateTemplate {
    templateName: string;
    aggregates: IAggregateTemplateFields[]
}

export interface IAggregateTemplateFields {
    label: string;
    function: string;
}

export interface IFilterTemplate {
    templateName: string;
    isPrimitive: boolean;
    textField: string;
    valueField: string;
    state: IDataRequestState;
    requestDetails: IRequestDetails;
}

export interface ICommandColumn {
    title: string;
    width?: number;
}