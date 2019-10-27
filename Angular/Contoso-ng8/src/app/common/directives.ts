import { ObjectHelper } from "./object-helper";
import { AbstractControl, FormControl, FormGroup, FormBuilder, FormArray } from "@angular/forms";
import { formTypeEnum, IFormItemSetting, IValidatorDescription, IFormGroupSettings, IFormGroupArraySettings, abstractControlKind, IFormGroupData, IGroupSettings } from "../stuctures/screens/edit/i-edit-form-settings";
import { EditFormHelpers } from "./edit-form-helpers";
import { IDirectiveDescription, IInputQuestion, IDirective } from "../stuctures/screens/input-form/i-input-form";
import { IHandleInputDirectiveArgs, IHandleEditDirectiveArgs, IInputDirectiveFunctionArgs, IEditDirectiveFunctionArgs } from "../stuctures/screens/i-directive-function-args";

export class DirectivesManager
{
    public static GetDirectiveClass(className: string)
    {
        switch (className)
        {
            case "Directives":
                return Directives;
            default:
                return undefined;
        }
    }
}


export class Directives
{
    static watchFields(groupSettings: IGroupSettings, formGroup: FormGroup, conditionalDirectives: { [key: string]: IDirective[] }, formBuilder: FormBuilder)
    {
        if (!conditionalDirectives)
            return;

        formGroup.valueChanges.subscribe(value =>
        {
            Object.keys(conditionalDirectives).forEach(targetField =>
            {
                conditionalDirectives[targetField].forEach(directive =>
                {
                    let fieldsToWatch: string[] = ObjectHelper.getConditionsFieldsToWatch(directive.conditionGroup, formGroup.value);
                    fieldsToWatch.forEach(fieldBeingUpdated =>
                    {
                        if (!(fieldBeingUpdated in value))
                        {//Then field is not present so notify target field setting the new value to null
                            Directives.handleDirective({
                                directive: directive,
                                formGroup: formGroup,
                                groupSettings: groupSettings,
                                fieldBeingUpdated: fieldBeingUpdated,
                                newValue: null,
                                targetControlName: targetField,
                                targetControlFieldSetting: <IFormItemSetting>Directives.getFormControlSetting(groupSettings.fieldSettings, targetField),
                                conditionalDirectives: conditionalDirectives,
                                formType: formTypeEnum.editForm,
                                fb: formBuilder
                            });
                        }
                    });
                });
            });
        });

        Object.keys(conditionalDirectives).forEach(targetField =>
        {
            conditionalDirectives[targetField].forEach(directive =>
            {
                let fieldsToWatch: string[] = ObjectHelper.getConditionsFieldsToWatch(directive.conditionGroup, formGroup.value);
                fieldsToWatch.forEach(fieldBeingUpdated =>
                {
                    let control = formGroup.get(fieldBeingUpdated);
                    if (!control) return;

                    control.valueChanges
                        .subscribe(value =>
                        {
                            Directives.handleDirective({
                                directive: directive,
                                formGroup: formGroup,
                                groupSettings: groupSettings,
                                fieldBeingUpdated: fieldBeingUpdated,
                                newValue: value,
                                targetControlName: targetField,
                                targetControlFieldSetting: <IFormItemSetting>Directives.getFormControlSetting(groupSettings.fieldSettings, targetField),
                                conditionalDirectives: conditionalDirectives,
                                formType: formTypeEnum.editForm,
                                fb: formBuilder
                            });
                        });
                });
            });
        });

        for (let setting of groupSettings.fieldSettings)
        {
            if (setting.abstractControlType == abstractControlKind.formGroup && formGroup.controls[setting.field])
            {
                Directives.watchFields((<IFormGroupArraySettings>setting), <FormGroup>formGroup.controls[setting.field], (<IFormGroupSettings>setting).conditionalDirectives, formBuilder);
            }
            else if (setting.abstractControlType == abstractControlKind.formGroupArray && formGroup.controls[setting.field])
            {
                const formGroupArray: FormArray =  <FormArray>formGroup.controls[setting.field];
                if (formGroupArray.controls && formGroupArray.controls.length)
                {
                    formGroupArray.controls.forEach(control => {
                        let fg: FormGroup = <FormGroup>control;

                        Directives.watchFields((<IFormGroupArraySettings>setting), fg, (<IFormGroupArraySettings>setting).conditionalDirectives, formBuilder);
                    });
                }
            }
        }
    }

    static getFormControlSetting(fieldSettings: IFormItemSetting[], fieldName: string): IFormItemSetting
    {
        if (!(fieldSettings && fieldSettings.length))
            return null;

        for (let field of fieldSettings)
        {
            if (field.field === fieldName)
                    return field;
        }

        return null;
    }

    static handleDirective(args: IHandleInputDirectiveArgs | IHandleEditDirectiveArgs): void
    {
        let obj: any = {};
        obj[args.fieldBeingUpdated] = args.newValue;
        let formObject = Object.assign({}, args.formGroup.value);

        //need to update an object representing the item form with the new value before validating
        //because itemForm has not yet been updated at valueChanges.
        let conditionEvaluation: boolean = ObjectHelper.evaluateCondition(Object.assign({}, formObject, obj), args.directive.conditionGroup);

        if (args.formType == formTypeEnum.inputForm)
            {
                Directives.getDirectiveFunction({
                    directive: args.directive,
                    formGroup: args.formGroup,
                    fieldBeingUpdated: args.fieldBeingUpdated,
                    newValue: args.newValue,
                    targetControlName: args.targetControlName,
                    targetControlFieldSetting: <IInputQuestion>args.targetControlFieldSetting,
                    result: conditionEvaluation,
                    conditionalDirectives: args.conditionalDirectives,
                    formType: args.formType,
                    fb: args.fb
                  });
            }
            else if (args.formType === formTypeEnum.editForm)
            {
                Directives.getDirectiveFunction({
                    directive: args.directive,
                    formGroup: args.formGroup,
                    groupSettings: (<IEditDirectiveFunctionArgs>args).groupSettings,
                    fieldBeingUpdated: args.fieldBeingUpdated,
                    newValue: args.newValue,
                    targetControlName: args.targetControlName,
                    targetControlFieldSetting: <IFormItemSetting>args.targetControlFieldSetting,
                    result: conditionEvaluation,
                    conditionalDirectives: args.conditionalDirectives,
                    formType: args.formType,
                    fb: args.fb
                  });
            }

        
    }

    static hideIf(args: IInputDirectiveFunctionArgs | IEditDirectiveFunctionArgs)
    {
        if (args.result)
        {
            let formControl: AbstractControl = args.formGroup.get(args.targetControlName);
            if (!formControl)
                return;//should never happen

            args.formGroup.removeControl(args.targetControlName);
        }
        else
        {
            if (args.formGroup.get(args.targetControlName))
                return;

            if (args.formType == formTypeEnum.inputForm)
            {
                args.formGroup.addControl(args.targetControlName,ObjectHelper.getFormControl(<IInputQuestion>args.targetControlFieldSetting));
            }
            else if (args.formType === formTypeEnum.editForm)
            {
                let newControl: AbstractControl = EditFormHelpers.getAbstractControl(<IFormItemSetting>args.targetControlFieldSetting, args.fb, args.formGroup, (<IEditDirectiveFunctionArgs>args).groupSettings);
               
                args.formGroup.addControl(args.targetControlName,newControl);
            }


            Object.keys(args.conditionalDirectives).forEach(targetField =>
            {
                args.conditionalDirectives[targetField].forEach(directive =>
                {
                    if (ObjectHelper.getConditionsFieldsToWatch(directive.conditionGroup, args.formGroup.value).includes(args.targetControlName))
                    {
                        let control = args.formGroup.get(args.targetControlName);
                        control.valueChanges
                            .subscribe(value =>
                            {
                                if (args.formType == formTypeEnum.inputForm)
                                {
                                    Directives.handleDirective({
                                        directive: directive,
                                        formGroup: args.formGroup,
                                        fieldBeingUpdated: args.targetControlName,
                                        newValue: value,
                                        targetControlName: targetField,
                                        targetControlFieldSetting: <IInputQuestion>args.targetControlFieldSetting,
                                        conditionalDirectives: args.conditionalDirectives,
                                        formType: args.formType,
                                        fb: args.fb
                                    });
                                }
                                else if (args.formType === formTypeEnum.editForm)
                                {
                                    Directives.handleDirective({
                                        directive: directive,
                                        formGroup: args.formGroup,
                                        groupSettings: (<IEditDirectiveFunctionArgs>args).groupSettings,
                                        fieldBeingUpdated: args.targetControlName,
                                        newValue: value,
                                        targetControlName: targetField,
                                        targetControlFieldSetting: <IFormItemSetting>args.targetControlFieldSetting,
                                        conditionalDirectives: args.conditionalDirectives,
                                        formType: args.formType,
                                        fb: args.fb
                                    });
                                }


                            });
                    }
                });
            });
        }
    }

    static reloadIf(args: IInputDirectiveFunctionArgs | IEditDirectiveFunctionArgs)
    {
        if (args.result)
        {
            let formControl: AbstractControl = args.formGroup.get(args.targetControlName);
            if (!formControl)
                return;//should never happen

            formControl.reset();

            if (args.targetControlFieldSetting.dropDownTemplate)
                args.targetControlFieldSetting.dropDownTemplate.reload = String(args.newValue);
        }
    }

    static clearIf(args: IInputDirectiveFunctionArgs | IEditDirectiveFunctionArgs)
    {
        if (args.result)
        {
            let formControl: AbstractControl = args.formGroup.get(args.targetControlName);
            if (!formControl)
                return;//should never happen

            formControl.reset();

            if (args.targetControlFieldSetting.dropDownTemplate)
                args.targetControlFieldSetting.dropDownTemplate.reload = "";
        }
    }

    static disableIf(args: IInputDirectiveFunctionArgs | IEditDirectiveFunctionArgs)
    {
        let formControl: AbstractControl = args.formGroup.get(args.targetControlName);
        if (!formControl)
            return;//should never happen

        if (args.result)
            formControl.disable();
        else
            formControl.enable();
    }

    static validateIf(args: IInputDirectiveFunctionArgs | IEditDirectiveFunctionArgs, validators: string[])
    {
        let formControl: FormControl = <FormControl>args.formGroup.get(args.targetControlName);
        if (!formControl)
            return;//should never happen

        if (args.result)//restore validators if this condition is true
            Directives.AddValidators(args.formGroup, args.targetControlName, validators, args.targetControlFieldSetting);
        else
            Directives.RemoveValidators(formControl, validators, args.targetControlFieldSetting);
    }

    static hasValidator(control: FormControl, validator: string, questionSetting: IInputQuestion | IFormItemSetting): boolean
    {
        if (!(questionSetting.validationSetting && questionSetting.validationSetting.validators && questionSetting.validationSetting.validators.length))
            return false;

        let validators: IValidatorDescription[] = questionSetting.validationSetting.validators.filter(v => v.functionName === validator);

        return validators.length > 0;
    }

    static hasValidators(control: FormControl, validators: string[], questionSetting: IInputQuestion | IFormItemSetting): boolean
    {
        for (let validator of validators)
        {
            if (!Directives.hasValidator(control, validator, questionSetting))
                return false;
        }

        return true;
    }

    static getValidator(control: FormControl, validator: string, questionSetting: IInputQuestion | IFormItemSetting): IValidatorDescription
    {
        if (!(questionSetting.unchangedValidationSetting && questionSetting.unchangedValidationSetting.validators && questionSetting.unchangedValidationSetting.validators.length))
            return null;

        return questionSetting.unchangedValidationSetting.validators.find(v => v.functionName === validator);
    }

    static RemoveValidators(control: FormControl, validatorsToRemove: string[], questionSetting: IInputQuestion | IFormItemSetting)
    {
        if (!Directives.hasValidators(control, validatorsToRemove, questionSetting))
            return;

        if (questionSetting.validationSetting && questionSetting.unchangedValidationSetting.validators)
        {
            let validators: IValidatorDescription[] = questionSetting.validationSetting.validators.filter(v => !validatorsToRemove.includes(v.functionName));
            if (validators)
            {
                questionSetting.validationSetting.validators = validators;
                control.clearValidators();
                control.setValidators(ObjectHelper.getValidatorFunctions(validators));
                control.updateValueAndValidity();
                questionSetting.placeholder = '';
            }
        }
    }

    static AddValidators(formGroup: FormGroup, targetControlName: string, validatorsToAdd: string[], questionSetting: IInputQuestion | IFormItemSetting)
    {
        let formControl: FormControl = <FormControl>formGroup.get(targetControlName);
        if (Directives.hasValidators(formControl, validatorsToAdd, questionSetting))
            return;

        if (questionSetting.unchangedValidationSetting)
        {
            validatorsToAdd.forEach(v =>
            {
                if (!Directives.hasValidator(formControl, v, questionSetting))
                {
                    let newValidator: IValidatorDescription = Directives.getValidator(formControl, v, questionSetting);
                    if (newValidator)
                        questionSetting.validationSetting.validators.push(newValidator);
                }
            });

            formControl.clearValidators();
            formControl.setValidators(ObjectHelper.getValidatorFunctions(questionSetting.validationSetting.validators));
            formControl.updateValueAndValidity();
        }
    }

    static getDirectiveFunctions(directives: IDirectiveDescription[], args: IInputDirectiveFunctionArgs | IEditDirectiveFunctionArgs): any[]
    {
        let directiveFunctions: any = [];
        directives.forEach(directive =>
        {
            directiveFunctions.push(Directives.getDirectiveFunction(args))
        });

        return directiveFunctions;
    }

    static getDirectiveFunction(directiveArguments: IInputDirectiveFunctionArgs | IEditDirectiveFunctionArgs): any
    {
        const directive: IDirectiveDescription =  directiveArguments.directive.directiveDescription;
        let args: any[] = directive.arguments
            ? [directiveArguments].concat(ObjectHelper.getArgumentsArray(directive.arguments))
            : [directiveArguments];

        let directiveClass = DirectivesManager.GetDirectiveClass(directive.className);
        return directiveClass[directive.functionName].apply(directiveClass, args)
    }
}