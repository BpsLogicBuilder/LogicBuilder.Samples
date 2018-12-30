import { ViewTypeEnum } from "./i-view-type";
import { ICommandButton } from "../i-command-button";
import { IGridSettings } from "./grid/i-grid-settings";
import { IEditFormSettings } from "./edit/i-edit-form-settings";
import { IValidationResult } from "../i-validation-result";

export interface IScreenSettingsBase {
    viewType: ViewTypeEnum;
    commandButtons: ICommandButton[];
    validationResults: IValidationResult[];
    [x: string]: any;
}

// export interface IGridDialogSettings extends IDialogSettingsBase {
//     settings: IGridSettings;
// }

// export interface IEditFormDialogSettings extends IDialogSettingsBase {
//     settings: IEditFormSettings;
// }

// export interface IGenericSettings<T> extends IDialogSettingsBase {
//     settings: T;
// }
