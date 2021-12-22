import { Validators } from "@angular/forms";
import { NumberValidators, CustomValidators } from "./numbers-validator";

export class ValidatorsManager
{
    public static GetValidatorClass(className: string)
    {
        switch (className)
        {
            case "Validators":
                return Validators;
            case "NumberValidators":
                return NumberValidators;
            case "CustomValidators":
                return CustomValidators;
            default:
                return undefined;
        }
    }
}