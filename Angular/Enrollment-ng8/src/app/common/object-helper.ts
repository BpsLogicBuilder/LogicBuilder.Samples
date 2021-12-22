import { GroupDescriptor, CompositeFilterDescriptor, FilterDescriptor, filterBy, SortDescriptor } from "@progress/kendo-data-query";
import { IGroup } from "../stuctures/screens/i-group";
import { IFilterGroup } from "../stuctures/screens/i-filter-group";
import { IFilterDefinition } from "../stuctures/screens/i-filter-definition";
import { IFormItemSetting, abstractControlKind, IFormGroupSettings, IValidatorDescription, IFormGroupArraySettings, IFormControlSettings, IFormGroupData } from "../stuctures/screens/edit/i-edit-form-settings";
import { EntityType } from "../stuctures/screens/i-base-model";
import { DateService } from "./date.service";
import { FormBuilder, FormGroup, FormControl, FormArray, AbstractControl } from "@angular/forms";
import { ValidatorsManager } from "./validators-manager";
import { IInputForm, IInputRow, IInputColumn, IInputQuestion, IConditionGroup } from "../stuctures/screens/input-form/i-input-form";
import { ListManagerService } from "./list-manager.service";
import { ISort } from "../stuctures/screens/i-sort";
import { EditFormHelpers } from "./edit-form-helpers";
import { debounceTime } from "rxjs/operators";
import { Directives } from "./directives";

export class ObjectHelper {
    static getProperty(obj: any, fullName: string): any {
        let parts: string[] = fullName.split(".");
        if (parts.length === 1) {
            return obj[fullName];
        }
        else {
            let firstPart = parts[0];
            let remainder = fullName.substring(firstPart.length + 1);
            return ObjectHelper.getProperty(obj[firstPart], remainder)
        }
    }

    static getGroupDescriptor(group: IGroup): GroupDescriptor {
        return Object.assign({}, group);
    }

    static getGroupDescriptors(groups: IGroup[]): GroupDescriptor[] {
        return groups.map(g => ObjectHelper.getGroupDescriptor(g));
    }

    static getSortDescriptor(sort: ISort): SortDescriptor {
        return Object.assign({}, sort);
    }

    static getSortDescriptors(sorts: ISort[]): SortDescriptor[] {
        return sorts.map(s => ObjectHelper.getSortDescriptor(s));
    }

    static getAboutData(aggregateData: any[], dateService: DateService): any[] {
        let list: any[] = [];
        let field: string = "enrollmentDate";
        aggregateData.forEach(item => {
            if (item.hasOwnProperty("field") && item.field === field) {
                list.push({ "enrollmentDate": dateService.convertToDate(item.value), "count": item.aggregates.enrollmentDate.count });
            }
        });
        return list;
    }

    static getAggregateData(aggregateData: any[], dateService: DateService, fieldName: string, aggregate: string): any[] {
        let list: any[] = [];

        aggregateData.forEach(item => {
            if (item.hasOwnProperty("field") && item.field === fieldName) {
                list.push({ [fieldName]: dateService.convertToDate(item.value), aggregate: item.aggregates[fieldName][aggregate] });
            }
        });
        return list;
    }

    static FilterRequiresValueSource(filterGroup: IFilterGroup): boolean {
        let requiresValueSource: boolean = false;
        if (filterGroup.filters && filterGroup.filters.length) {
            filterGroup.filters.forEach(filter => {
                if (filter.valueSourceMember)
                {
                    requiresValueSource = true;
                }
            });
        }

        if (requiresValueSource) return true;

        if (filterGroup.filterGroups && filterGroup.filterGroups.length) {
            filterGroup.filterGroups.forEach(fg => {
                if (ObjectHelper.FilterRequiresValueSource(fg))
                    requiresValueSource = true;
            });
        }

        return requiresValueSource;
    }

    static getCompositeFilter(filterGroup: IFilterGroup, valueSource?: any): CompositeFilterDescriptor {
        let compositeFilter: CompositeFilterDescriptor = { logic: filterGroup.logic, filters: [] };
        if (filterGroup.filters && filterGroup.filters.length) {
            filterGroup.filters.forEach(filter => {
                let filterDescriptor: FilterDescriptor = valueSource && filter.valueSourceMember
                    ? {
                        field: filter.field,
                        operator: filter.operator,
                        value: ObjectHelper.getProperty(valueSource, filter.valueSourceMember)
                    }
                    : {
                        field: filter.field,
                        operator: filter.operator,
                        value: filter.value
                    };
                compositeFilter.filters.push(filterDescriptor);
            });
        }

        if (filterGroup.filterGroups && filterGroup.filterGroups.length) {
            filterGroup.filterGroups.forEach(fg => {
                compositeFilter.filters.push(ObjectHelper.getCompositeFilter(fg, valueSource));
            });
        }

        return compositeFilter;
    }

    static updateFilterValueFields(filterGroup: IFilterGroup, valueSource?: any): IFilterGroup {
        let convertedFilterGrroup: IFilterGroup = { logic: filterGroup.logic, filters: [] };
        if (filterGroup.filters && filterGroup.filters.length) {
            filterGroup.filters.forEach(filter => {
                let filterDescriptor: IFilterDefinition = valueSource && filter.valueSourceMember
                    ? {
                        field: filter.field,
                        operator: filter.operator,
                        value: ObjectHelper.getProperty(valueSource, filter.valueSourceMember)
                    }
                    : {
                        field: filter.field,
                        operator: filter.operator,
                        value: filter.value
                    };
                convertedFilterGrroup.filters.push(filterDescriptor);
            });
        }

        if (filterGroup.filterGroups && filterGroup.filterGroups.length) {
            filterGroup.filterGroups.forEach(fg => {
                convertedFilterGrroup.filterGroups.push(ObjectHelper.updateFilterValueFields(fg, valueSource));
            });
        }

        return convertedFilterGrroup;
    }

    static getArgumentsArray(obj: any): any[] {
        let args: any[] = [];
        Object.keys(obj).forEach(key => args.push(obj[key]));

        return args;
    }

    static getPatchObject(formGroup: FormGroup, fieldSettings: IFormItemSetting[], item: EntityType, dateService: DateService, fb: FormBuilder): any
    {
        let patchObject = Object.assign({}, item || {});
        fieldSettings.forEach(field =>
        {
            if (field.abstractControlType === abstractControlKind.formControl || field.abstractControlType === abstractControlKind.multiSelectFormControl && formGroup.controls[field.field])
            {
                if (item)
                {
                    patchObject[field.field] = field.type == 'date'
                        ? dateService.convertToDate(item[field.field])
                        : patchObject[field.field] = item[field.field];
                }
                else if ((<IFormControlSettings>field).unchangedValidationSetting)
                {
                    patchObject[field.field] = (<IFormControlSettings>field).unchangedValidationSetting.defaultValue;
                }
                else
                {
                    patchObject[field.field] = null;
                }
            }
            else if (item && field.abstractControlType === abstractControlKind.formGroup && formGroup.controls[field.field])
            {
                patchObject[field.field] = this.getPatchObject(<FormGroup>formGroup.controls[field.field], (<IFormGroupSettings>field).fieldSettings, item[field.field], dateService, fb);
            }
            else if (item && field.abstractControlType === abstractControlKind.formGroupArray)
            {
                if (item[field.field] && item[field.field].length && formGroup.controls[field.field])
                {
                    patchObject[field.field] = [];
                    item[field.field].forEach(element =>
                    {
                        let formArray: FormArray = <FormArray>formGroup.controls[field.field];
                        let fg: FormGroup = ObjectHelper.buildFormGroup((<IFormGroupArraySettings>field).fieldSettings, fb);

                        formArray.push(fg);
                        patchObject[field.field].push(this.getPatchObject(fg, (<IFormGroupArraySettings>field).fieldSettings, element, dateService, fb));
                    });
                }
                else
                {
                    patchObject[field.field] = [];
                }
            }
            else
            {
                patchObject[field.field] = null;
            }
        });

        return patchObject;
    }

    static getPatchObjectFromInputForm(formSettings: IInputForm, dateService: DateService): any {
        let patchObject = {};

        if (formSettings.rows) {
            formSettings.rows.forEach(row => {
                ObjectHelper.updatePatchObjectByRow(patchObject, row, dateService);
            });
        }

        return patchObject;
    }

    static updatePatchObjectByRow(patchObject: any, rowSetting: IInputRow, dateService: DateService) {
        if (rowSetting.columns) {
            rowSetting.columns.forEach(column => {
                ObjectHelper.updatePatchObjectByColumn(patchObject, column, dateService);
            });
        }
    }

    static updatePatchObjectByColumn(patchObject: any, columnSetting: IInputColumn, dateService: DateService) {
        if (columnSetting.rows) {
            columnSetting.rows.forEach(row => {
                ObjectHelper.updatePatchObjectByRow(patchObject, row, dateService);
            });
        }

        if (columnSetting.questions) {
            columnSetting.questions.forEach(question => {
                patchObject[question.variableId] = question.htmlType == 'date' 
                            ? dateService.convertToDate(question.currentValue) 
                            : question.currentValue;
            });
        }
    }

    static getFormControl(questionSetting: IInputQuestion) : FormControl {
        if (questionSetting.validationSetting) {
            if (questionSetting.validationSetting.validators) {
                let fn = ObjectHelper.getValidatorFunctions(questionSetting.validationSetting.validators);
                return new FormControl(questionSetting.validationSetting.defaultValue, fn);
            }
            else {
                return new FormControl(questionSetting.validationSetting.defaultValue);
            }
        }

        return new FormControl();
    }

    static findQuestionSettingByColumn(questionName: string, columnSetting: IInputColumn): IInputQuestion {
        let questionSetting: IInputQuestion = null;
        if (columnSetting.rows && columnSetting.rows.length) {
            for (let row of columnSetting.rows)
            {
                questionSetting = ObjectHelper.findQuestionSettingByRow(questionName, row);
                if (questionSetting)
                    return questionSetting;
            }
        }

        if (columnSetting.questions && columnSetting.questions.length) {
            for (let question of columnSetting.questions)
            {
                if (question.variableId == questionName)
                    return question;
            }
        }
        
        return null;
    }

    static findQuestionSettingByRow(questionName: string, rowSetting: IInputRow): IInputQuestion {
        let questionSetting: IInputQuestion = null;
        if (rowSetting.columns && rowSetting.columns.length) {
            for (let column of rowSetting.columns)
            {
                questionSetting = ObjectHelper.findQuestionSettingByColumn(questionName, column);
                if (questionSetting)
                    return questionSetting;
            }
        }
        
        return null;
    }

    static findQuestionSetting(questionName: string, formSettings: IInputForm): IInputQuestion {
        let questionSetting: IInputQuestion = null;
        if (formSettings.rows && formSettings.rows.length) {
            for (let row of formSettings.rows)
            {
                questionSetting = ObjectHelper.findQuestionSettingByRow(questionName, row);
                if (questionSetting)
                    return questionSetting;
            }
        }
        
        return null;
    }

    static buildFormGroup(fieldSettings: IFormItemSetting[], fb: FormBuilder): FormGroup {
        let controlsObject = {};
        fieldSettings.forEach(field => {
            if (field.abstractControlType === abstractControlKind.formControl || field.abstractControlType === abstractControlKind.multiSelectFormControl) {
                if (field.unchangedValidationSetting) {
                    controlsObject[field.field] = [field.unchangedValidationSetting.defaultValue];
                }
                else {
                    controlsObject[field.field] = [null];
                }
            }
            else if (field.abstractControlType === abstractControlKind.formGroup) {
                controlsObject[field.field] = ObjectHelper.buildFormGroup((<IFormGroupSettings>field).fieldSettings, fb);
            }
            else if (field.abstractControlType === abstractControlKind.formGroupArray) {//Need to add when the data loads - the number of items will depend on the data
                controlsObject[field.field] = fb.array([]);
            }
        });

        let formGroup: FormGroup = fb.group(controlsObject);
        //create formGroup without validation first so that the formGroup is available when creating validation functions
        //necessary for compare validators
        ObjectHelper.setEditFormValidators(fieldSettings, formGroup);
        return formGroup;
    }

    static setEditFormValidators(fieldSettings: IFormItemSetting[], formGroup: FormGroup)
    {
        fieldSettings.forEach(field =>
        {
            if (field.abstractControlType === abstractControlKind.formControl || field.abstractControlType === abstractControlKind.multiSelectFormControl)
            {
                if (field.unchangedValidationSetting)
                {
                    if (field.unchangedValidationSetting.validators)
                    {
                        let formControl: FormControl = <FormControl>formGroup.get(field.field);
                        formControl.clearValidators();
                        formControl.setValidators(ObjectHelper.getValidatorFunctions(field.unchangedValidationSetting.validators));
                        formControl.updateValueAndValidity();
                    }
                }
            }
            else if (field.abstractControlType === abstractControlKind.formGroup)
            {
                ObjectHelper.setEditFormValidators((<IFormGroupSettings>field).fieldSettings, <FormGroup>formGroup.get(field.field));
            }
            else if (field.abstractControlType === abstractControlKind.formGroupArray)
            {
                let formArray: FormArray = <FormArray>formGroup.get(field.field);
                formArray.controls.forEach(control => {
                    if (field.abstractControlType === abstractControlKind.formGroup)
                    {//Only Supporting formGroups as elements of FormArray
                        ObjectHelper.setEditFormValidators((<IFormGroupArraySettings>field).fieldSettings, <FormGroup>control);
                    }
                })
            }
        });
    }

    static getValidatorFunctions(validators: IValidatorDescription[]): any[] {
        let validatorFunctions: any = [];
        validators.forEach(validator => {
            validatorFunctions.push(ObjectHelper.getValidatorFunction(validator))
        });

        return validatorFunctions;
    }

    static getValidatorFunction(validator: IValidatorDescription): any {
        let validatorClass = ValidatorsManager.GetValidatorClass(validator.className);
        return validator.arguments
            ? validatorClass[validator.functionName].apply(validatorClass, ObjectHelper.getArgumentsArray(validator.arguments))
            : validatorClass[validator.functionName];
    }

    static getConditionsCompositeFilter(conditionGroup: IConditionGroup, valueSource?: any): CompositeFilterDescriptor {
        let compositeFilter: CompositeFilterDescriptor = { logic: conditionGroup.logic, filters: [] };
        if (conditionGroup.conditions && conditionGroup.conditions.length) {
            conditionGroup.conditions.forEach(filter => {
                let filterDescriptor: FilterDescriptor = valueSource && filter.rightVariable
                    ? {
                        field: filter.leftVariable.replace('.', '_'),
                        operator: filter.operator,
                        value: ObjectHelper.getProperty(valueSource, filter.rightVariable.replace('.', '_'))
                    }
                    : {
                        field: filter.leftVariable.replace('.', '_'),
                        operator: filter.operator,
                        value: filter.value
                    };
                compositeFilter.filters.push(filterDescriptor);
            });
        }

        if (conditionGroup.conditionGroups && conditionGroup.conditionGroups.length) {
            conditionGroup.conditionGroups.forEach(fg => {
                compositeFilter.filters.push(ObjectHelper.getCompositeFilter(fg, valueSource));
            });
        }

        return compositeFilter;
    }

    static getConditionsFieldsToWatch(conditionGroup: IConditionGroup, listManager: ListManagerService): string[] {
        let obj:any = {};
        let list: string[] = [];
        if (conditionGroup.conditions && conditionGroup.conditions.length) {
            conditionGroup.conditions.forEach(filter => {
                obj[filter.leftVariable.replace('.', '_')] = '';
                if (filter.rightVariable)
                    obj[filter.rightVariable.replace('.', '_')] = '';
            });
        }

        Object.keys(obj).forEach(key => 
        {
            list.push(key);
        });

        if (conditionGroup.conditionGroups && conditionGroup.conditionGroups.length) {
            conditionGroup.conditionGroups.forEach(cg => {
                list = listManager.mergeLists<string>(list, ObjectHelper.getConditionsFieldsToWatch(cg, listManager), []);
            });
        }

        
        return list;
    }

    static evaluateCondition(obj: any, condition: IConditionGroup): boolean
    {
        let filter: CompositeFilterDescriptor = ObjectHelper.getConditionsCompositeFilter(condition, obj);
        return filterBy([obj], filter).length > 0
    }
}