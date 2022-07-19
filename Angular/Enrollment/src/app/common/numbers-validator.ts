import { ValidatorFn, AbstractControl, UntypedFormGroup } from "@angular/forms";

export class NumberValidators
{
    static range(min: number, max: number): ValidatorFn
    {
        return (c: AbstractControl): { [key: string]: boolean } | null =>
        {
            if (c.value && (isNaN(c.value) || c.value < min || c.value > max))
            {
                return { 'range': true };
            }
            return null;
        };
    }

    static mustBeANumber(c: AbstractControl): { [key: string]: boolean } | null
    {
        if (c.value && (isNaN(c.value)))
        {
            return { 'mustBeANumber': true };
        }
        return null;
    }
}

export class CustomValidators
{
    static fieldMatcher(otherField: string): ValidatorFn
    {
        return (c: AbstractControl): { [key: string]: boolean } | null =>
        {
            const formGroup: UntypedFormGroup = <UntypedFormGroup>c.parent;
            const otherControl = formGroup.get(otherField);

            if (!otherControl.value)
                return null;
    
            if (c.value === otherControl.value)
            {
                if (otherControl.hasError('fieldMatcher'))
                {
                    //clear the error in this control first
                    c.setErrors({'fieldMatcher': null});
                    //then check for validity in the other control
                    otherControl.updateValueAndValidity();
                }

                return null;
            }

            return { 'fieldMatcher': true };
        }
    }
}