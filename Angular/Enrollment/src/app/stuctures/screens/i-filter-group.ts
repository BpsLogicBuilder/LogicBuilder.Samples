import { IFilterDefinition } from "./i-filter-definition";

export interface IFilterGroup {
    logic: "and" | "or";
    filters?: IFilterDefinition[];
    filterGroups?: IFilterGroup[];
}