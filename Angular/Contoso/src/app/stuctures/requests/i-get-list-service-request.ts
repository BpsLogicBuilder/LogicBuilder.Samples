export interface IGetListServiceRequest {
    selector: any;
    selectExpandDefinition?: any;
    modelType: string;
    dataType: string;
    modelReturnType?: string;
    dataReturnType?: string;
}