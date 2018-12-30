export interface IAggregate {
    field: string;
    aggregate: 'count' | 'sum' | 'average' | 'min' | 'max';
}