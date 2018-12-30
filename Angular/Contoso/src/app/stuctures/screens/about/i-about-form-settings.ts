import { IRequestDetails } from "../i-request-details";
import { IDataRequestState } from "../i-data-request-state";
import { IDetailItem } from "../detail/i-detail-form-settings";

export interface IAboutFormSettings {
    title: string;
    requestDetails: IRequestDetails;
    state: IDataRequestState,
    fieldSettings?: IDetailItem[];
}