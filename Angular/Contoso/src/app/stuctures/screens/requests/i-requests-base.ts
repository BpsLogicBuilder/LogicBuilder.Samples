import { ICommandButtonRequest } from "./i-command-button-request";
import { IFlowState } from "../../i-flow-state";
import { ViewTypeEnum } from "../i-view-type";
import { IFilterGroup } from "../i-filter-group";
import { IInputForm } from "../input-form/i-input-form";

export interface IRequestsBase {
    commandButtonRequest: ICommandButtonRequest;
    flowState?: IFlowState;
    viewType: ViewTypeEnum;
}

export interface IInputFormRequest extends IRequestsBase  {
    form?: IInputForm;
}

export interface IGridRequest extends IRequestsBase  {
    filterGroup?: IFilterGroup;
}

export interface IEditRequest extends IRequestsBase  {
}

export interface IDetailRequest extends IRequestsBase  {
    filterGroup?: IFilterGroup;
}

export interface IDeleteRequest extends IRequestsBase  {
    filterGroup?: IFilterGroup;
}