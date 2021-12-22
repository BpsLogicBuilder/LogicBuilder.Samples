import { ISort } from "./i-sort";
import { IGroup } from "./i-group";
import { IFilterGroup } from "./i-filter-group";
import { IAggregate } from "./i-aggregate";

export interface IDataRequestState {
    skip?: number;
    take?: number;
    sort?: ISort[];
    group?: IGroup[];
    filterGroup?: IFilterGroup;
    aggregates?: IAggregate[];
}