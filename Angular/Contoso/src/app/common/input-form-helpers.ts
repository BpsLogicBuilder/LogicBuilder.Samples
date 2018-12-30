import { IInputQuestion, IInputForm, IInputRow, IInputColumn } from "../stuctures/screens/input-form/i-input-form";
import { ObjectHelper } from "./object-helper";
import { FormControl, FormGroup, FormBuilder } from "@angular/forms";
import { ListManager } from "./list-manager.service";
import { EntityStateType } from "../stuctures/screens/entity-state-type";

export class InputFormHelper
{
    static AddRowFieldSettings(controlsObject: any, rowSetting: IInputRow)
    {
        if (rowSetting.columns)
        {
            rowSetting.columns.forEach(column =>
            {
                InputFormHelper.AddColumnFieldSettings(controlsObject, column);
            });
        }
    }

    static AddColumnFieldSettings(controlsObject: any, columnSetting: IInputColumn)
    {
        if (columnSetting.rows)
        {
            columnSetting.rows.forEach(row =>
            {
                InputFormHelper.AddRowFieldSettings(controlsObject, row);
            });
        }

        if (columnSetting.questions)
        {
            columnSetting.questions.forEach(question =>
            {
                InputFormHelper.AddQuestionFieldSettings(controlsObject, question);
            });
        }
    }

    static AddQuestionFieldSettings(controlsObject: any, questionSetting: IInputQuestion)
    {
        if (questionSetting.validationSetting)
        {
            // if (questionSetting.validationSetting.validators) {
            //     let fn = ObjectHelper.getValidatorFunctions(questionSetting.validationSetting.validators);
            //     controlsObject[questionSetting.variableId] = [questionSetting.validationSetting.defaultValue, fn];
            // }
            // else {
            //     controlsObject[questionSetting.variableId] = [questionSetting.validationSetting.defaultValue];
            // }

            //create controls without validation first so that the formGroup is available when creating validation functions
            controlsObject[questionSetting.variableId] = [questionSetting.validationSetting.defaultValue];
        }
        else
        {
            controlsObject[questionSetting.variableId] = [null];
        }
    }

    static buildInputFormGroup(controlsObject: any, formSettings: IInputForm, fb: FormBuilder): FormGroup
    {

        if (formSettings.rows)
        {
            formSettings.rows.forEach(row =>
            {
                InputFormHelper.AddRowFieldSettings(controlsObject, row);
            });
        }

        let formGroup: FormGroup = fb.group(controlsObject);
        //create formGroup without validation first so that the formGroup is available when creating validation functions
        InputFormHelper.setInputFormValidators(formGroup, formSettings);
        return formGroup;
    }

    static setInputFormValidators(formGroup: FormGroup, formSettings: IInputForm): IInputForm
    {
        if (formSettings.rows)
        {
            formSettings.rows.forEach(row =>
            {
                InputFormHelper.setInputRowValidators(formGroup, row);
            });
        }

        return formSettings;
    }

    static setInputRowValidators(formGroup: FormGroup, rowSetting: IInputRow)
    {
        if (rowSetting.columns)
        {
            rowSetting.columns.forEach(column =>
            {
                InputFormHelper.setInputColumnValidators(formGroup, column);
            });
        }
    }

    static setInputColumnValidators(formGroup: FormGroup, columnSetting: IInputColumn)
    {
        if (columnSetting.rows)
        {
            columnSetting.rows.forEach(row =>
            {
                InputFormHelper.setInputRowValidators(formGroup, row);
            });
        }

        if (columnSetting.questions)
        {
            columnSetting.questions.forEach(question =>
            {
                if (question.unchangedValidationSetting)
                {

                    let formControl: FormControl = <FormControl>formGroup.get(question.variableId);
                    formControl.clearValidators();
                    formControl.setValidators(ObjectHelper.getValidatorFunctions(question.unchangedValidationSetting.validators));
                    formControl.updateValueAndValidity();
                }
            });
        }
    }

    static updateInputFormFromPatchObject(patchObject: any, formSettings: IInputForm): IInputForm
    {

        if (formSettings.rows)
        {
            formSettings.rows.forEach(row =>
            {
                InputFormHelper.updateRowFromPatchObject(patchObject, row);
            });
        }

        return formSettings;
    }

    static updateRowFromPatchObject(patchObject: any, rowSetting: IInputRow)
    {
        if (rowSetting.columns)
        {
            rowSetting.columns.forEach(column =>
            {
                InputFormHelper.updateColumnFromPatchObject(patchObject, column);
            });
        }
    }

    static updateColumnFromPatchObject(patchObject: any, columnSetting: IInputColumn)
    {
        if (columnSetting.rows)
        {
            columnSetting.rows.forEach(row =>
            {
                InputFormHelper.updateRowFromPatchObject(patchObject, row);
            });
        }

        if (columnSetting.questions)
        {
            columnSetting.questions.forEach(question =>
            {
                if (question.multiSelectTemplate)
                {
                    if (patchObject[question.variableId] && patchObject[question.variableId].length)
                    {
                        for (let i in patchObject[question.variableId])
                        {
                            if (question.currentValue
                                && question.currentValue.length
                                && ListManager.itemExists<any>(patchObject[question.variableId][i], question.currentValue, [question.multiSelectTemplate.valueField]))
                            {
                                patchObject[question.variableId][i].entityState = EntityStateType.Unchanged;
                            }
                            else
                            {
                                patchObject[question.variableId][i].entityState = EntityStateType.Added;
                                patchObject[question.variableId][i].typeFullName = question.multiSelectTemplate.modelType;
                            }
                        }
                    }

                    if (question.currentValue && question.currentValue.length)
                    {
                        for (let i in question.currentValue)
                        {
                            if (!ListManager.itemExists<any>(question.currentValue[i], patchObject[question.variableId] || [], [question.multiSelectTemplate.valueField]))
                            {
                                //Add the deleted field
                                let newObject: any = question.currentValue[i];
                                newObject.entityState = EntityStateType.Deleted;
                                //Set its entity state to deleted.
                                patchObject[question.variableId].push(newObject);
                            }
                        }
                    }
                    question.currentValue = patchObject[question.variableId];
                }
                else
                {
                    question.currentValue = patchObject[question.variableId];
                }
            });
        }
    }
}