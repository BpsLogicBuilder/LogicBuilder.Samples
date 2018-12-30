import { IAggregate } from "./i-aggregate";

export interface IGroup {
    field: string;
    dir?: 'asc' | 'desc';
    aggregates?: IAggregate[];
}