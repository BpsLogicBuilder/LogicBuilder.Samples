import { ICommandButtonRequest } from "./i-command-button-request";
import { IFlowState } from "../../i-flow-state";
import { ViewTypeEnum } from "../i-view-type";

export interface IRequestsBase {
    commandButtonRequest: ICommandButtonRequest;
    flowState?: IFlowState;
    viewType: ViewTypeEnum;
}

export interface IGridRequest extends IRequestsBase  {
    entity?: any;
}

export interface IEditRequest extends IRequestsBase  {
}

export interface IDetailRequest extends IRequestsBase  {
    entity?: any;
}

export interface IDeleteRequest extends IRequestsBase  {
    entity: any;
}