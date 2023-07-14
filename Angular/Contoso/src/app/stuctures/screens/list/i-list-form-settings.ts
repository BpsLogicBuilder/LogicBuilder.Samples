import { IRequestDetails } from "../i-request-details";
import { IDetailItem } from "../detail/i-detail-form-settings";

export interface IListFormSettings {
    title: string;
    fieldsSelector: any;
    requestDetails: IRequestDetails;
    fieldSettings?: IDetailItem[];
}
