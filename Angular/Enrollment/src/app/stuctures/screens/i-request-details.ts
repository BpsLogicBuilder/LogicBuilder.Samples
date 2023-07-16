export interface IRequestDetails
{
    dataSourceUrl?: string;
    modelType?: string;
    dataType?: string;
    modelReturnType?: string;
    dataReturnType?: string;
    selectExpandDefinition?: any;
}

export interface IFormRequestDetails
{
    getUrl?: string;
    updateUrl?: string;
    addUrl?: string;
    deleteUrl?: string;
    modelType?: string;
    dataType?: string;
    filter?: any;
    selectExpandDefinition?: any;
}

export interface IGridRequestDetails
{
    dataSourceUrl?: string;
    modelType?: string;
    dataType?: string;
    selectExpandDefinition?: any;
}