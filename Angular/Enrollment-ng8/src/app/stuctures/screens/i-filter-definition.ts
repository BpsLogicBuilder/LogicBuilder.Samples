export interface IFilterDefinition {
    field?: string;
    operator: string;
    value?: any;
    ignoreCase?: boolean;
    valueSourceType?: string;
    valueSourceMember?: string;
}