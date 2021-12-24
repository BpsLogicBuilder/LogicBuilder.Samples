export interface DataRequest
{
    options: any;
    modelType: string;
    dataType: string;
    includes?: string[];
    selects?: any;
    distinct?: boolean;
    selectExpandDefinition?: any;
}