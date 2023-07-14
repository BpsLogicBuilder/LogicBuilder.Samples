export interface IGetItemServiceRequest {
    modelType: string;
    dataType: string;
    selectExpandDefinition?: any;
    filter?: string;
}