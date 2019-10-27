export interface IRequestDetails
{
    dataSourceUrl?: string;
    getUrl?: string;
    updateUrl?: string;
    addUrl?: string;
    deleteUrl?: string;
    modelType?: string;
    dataType?: string;
    includes?: string[];
    selects?: Select;
    distinct?: boolean;
}

//i.e. Dictionary where key is the anonymous type's member name
//and value is the source member in the target class.
export interface Select {
    [x: string]: string;
}