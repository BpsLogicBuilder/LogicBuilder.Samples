import { Injectable } from '@angular/core';
import { EntityType } from '../stuctures/screens/i-base-model';
import { EntityStateType } from '../stuctures/screens/entity-state-type';
import { UntypedFormGroup, UntypedFormArray } from '@angular/forms';
import { IFormItemSetting, abstractControlKind, IGroupBoxSettings, IFormGroupSettings, IFormGroupArraySettings, IMultiSelectFormControlSettings } from '../stuctures/screens/edit/i-edit-form-settings';

@Injectable({
    providedIn: 'root'
})
export class ListManagerService {

    constructor() { }

    public updateFormEntityState(entityToSave: EntityType, originalEntity: EntityType, formGroup: UntypedFormGroup, fieldSettings: IFormItemSetting[], isInsert: boolean = false): EntityType {
        if (!formGroup.dirty) {
            entityToSave.entityState = EntityStateType.Unchanged;
            //The following should not be necessary because this code should not run on insert for nested form groups.
            // if (isInsert) {
            //     //Generic Repository will set entityState for all child objects to added
            //     entityToSave = null;
            // }
            // else {
            //     entityToSave.entityState = EntityStateType.Unchanged;
            // }
        }
        else if ((!originalEntity) || isInsert) {
            entityToSave.entityState = EntityStateType.Added;
            for (let setting of fieldSettings) {
                if (setting.abstractControlType === abstractControlKind.groupBox) {
                    this.updateFormEntityState(
                        entityToSave,
                        originalEntity,
                        formGroup,
                        (<IGroupBoxSettings>setting).fieldSettings
                    );
                }
                else if (setting.abstractControlType === abstractControlKind.formGroup) {

                    if (!formGroup.controls[setting.field]) {
                        entityToSave[setting.field] = null;
                        continue;
                    }
                    //Generic Repository will set entityState for all child objects to added
                    //May also need to check for value recursivly in case the original entity is being initialized for insert.
                    if (!formGroup.controls[setting.field].dirty) {
                        entityToSave[setting.field] = null;
                    }

                    if (setting.modelType && entityToSave[setting.field]) {
                        entityToSave[setting.field].typeFullName = setting.modelType;
                    }
                }
                else if (setting.abstractControlType === abstractControlKind.formGroupArray) {
                    if (!formGroup.controls[setting.field] || !formGroup.controls[setting.field].dirty) {
                        entityToSave[setting.field] = null;
                        continue;
                    }
                    //e.g. entityToSave[setting.field] is instructor.courses,  formGroup.controls[setting.field] is this.instructorForm.value.courses
                    if (entityToSave[setting.field] && entityToSave[setting.field].length) {
                        for (let i in entityToSave[setting.field]) {
                            entityToSave[setting.field][i].entityState = EntityStateType.Added;
                            entityToSave[setting.field][i].typeFullName = (<IFormGroupArraySettings>setting).arrayElementType;

                            //Still check for enity state on child items - if the root entity state === modified then
                            //the entity state for child items must be defined individually
                            entityToSave[setting.field][i] = this.updateFormEntityState(//e.g. entityToSave[setting.field] is instructor.courses
                                entityToSave[setting.field][i], //e.g. entityToSave[setting.field][i] is instructor.courses[i]
                                null,
                                <UntypedFormGroup>(<UntypedFormArray>formGroup.controls[setting.field]).at(parseInt(i)),
                                (<IFormGroupArraySettings>setting).fieldSettings
                            );
                        }
                    }
                }
            }
        }
        else {//entityToSave = instructor
            entityToSave.entityState = EntityStateType.Modified;
            for (let setting of fieldSettings) {
                if (setting.abstractControlType === abstractControlKind.groupBox) {
                    this.updateFormEntityState(
                        entityToSave,
                        originalEntity,
                        formGroup,
                        (<IGroupBoxSettings>setting).fieldSettings
                    );
                }
                else if (setting.abstractControlType == abstractControlKind.formGroup) {
                    if (!formGroup.controls[setting.field]) {
                        entityToSave[setting.field] = null;
                        continue;
                    }

                    entityToSave[setting.field] = Object.assign(
                        {},
                        originalEntity[setting.field],
                        formGroup.controls[setting.field].value
                    );
                    entityToSave[setting.field] = this.updateFormEntityState(
                        entityToSave[setting.field],// instructor.officeAssignment officeAssignment is a child entity
                        originalEntity[setting.field], //original instructor.officeAssignment
                        <UntypedFormGroup>formGroup.controls[setting.field], //child form group
                        (<IFormGroupSettings>setting).fieldSettings //child formGroup setting
                        //isInsert is always false at this point
                    );

                    if ((!(originalEntity && originalEntity[setting.field])) && setting.modelType && entityToSave[setting.field]) {
                        entityToSave[setting.field].typeFullName = setting.modelType;
                    }
                }
                else if (setting.abstractControlType == abstractControlKind.formGroupArray) {
                    if (!formGroup.controls[setting.field] || !formGroup.controls[setting.field].dirty) {
                        entityToSave[setting.field] = null;
                        continue;
                    }
                    //e.g. entityToSave[setting.field] is instructor.courses,  formGroup.controls[setting.field] is this.instructorForm.value.courses
                    if (entityToSave[setting.field] && entityToSave[setting.field].length) {
                        for (let i in entityToSave[setting.field]) {
                            if (originalEntity[setting.field]
                                && originalEntity[setting.field].length
                                && this.itemExists<EntityType>(entityToSave[setting.field][i], originalEntity[setting.field], (<IFormGroupArraySettings>setting).keyFields)) {

                                let formArray: UntypedFormArray = <UntypedFormArray>formGroup.controls[setting.field];
                                //let obj2 = formGroup.controls[setting.field][i];
                                entityToSave[setting.field][i] = Object.assign({}, originalEntity[setting.field][i], formArray.at(parseInt(i)).value);
                                entityToSave[setting.field][i] = this.updateFormEntityState(//e.g. entityToSave[setting.field] is instructor.courses
                                    entityToSave[setting.field][i], //e.g. entityToSave[setting.field][i] is instructor.courses[i]
                                    originalEntity[setting.field][i],
                                    <UntypedFormGroup>formArray.at(parseInt(i)),
                                    (<IFormGroupArraySettings>setting).fieldSettings
                                    //isInsert is always false at this point
                                );
                            }
                            else {
                                entityToSave[setting.field][i].entityState = EntityStateType.Added;
                                entityToSave[setting.field][i].typeFullName = (<IFormGroupArraySettings>setting).arrayElementType;

                                //Still check for enity state on child items - if the root entity state === modified then
                                //the entity state for child items must be defined individually
                                entityToSave[setting.field][i] = this.updateFormEntityState(//e.g. entityToSave[setting.field] is instructor.courses
                                    entityToSave[setting.field][i], //e.g. entityToSave[setting.field][i] is instructor.courses[i]
                                    null,
                                    <UntypedFormGroup>(<UntypedFormArray>formGroup.controls[setting.field]).at(parseInt(i)),
                                    (<IFormGroupArraySettings>setting).fieldSettings
                                );
                            }
                        }
                    }

                    if (originalEntity[setting.field] && originalEntity[setting.field].length) {
                        for (let i in originalEntity[setting.field]) {
                            if (entityToSave[setting.field]
                                && entityToSave[setting.field].length
                                && !this.itemExists<EntityType>(originalEntity[setting.field][i], entityToSave[setting.field] || [], (<IFormGroupArraySettings>setting).keyFields)) {
                                //Add the deleted field
                                entityToSave[setting.field][i] = originalEntity[setting.field][i];
                                //Set its entity state to deleted.
                                entityToSave[setting.field][i].entityState = EntityStateType.Deleted;
                            }
                        }
                    }
                }
                else if (setting.abstractControlType == abstractControlKind.multiSelectFormControl) {
                    if (!formGroup.controls[setting.field]) {
                        entityToSave[setting.field] = null;
                        continue;
                    }

                    //e.g. entityToSave[setting.field] is instructor.courses
                    //entityToSave[setting.field] = this.mergeLists(originalEntity[setting.field] || [], formGroup.controls[setting.field] ? formGroup.controls[setting.field].value : [], (<IMultiSelectFormControlSettings>setting).keyFields);
                    //Why was I merging the lists? Need to merge the lists to get the deleted values
                    if (entityToSave[setting.field] && entityToSave[setting.field].length) {
                        for (let i in entityToSave[setting.field]) {
                            if (originalEntity[setting.field]
                                && originalEntity[setting.field].length
                                && this.itemExists<EntityType>(entityToSave[setting.field][i], originalEntity[setting.field], (<IMultiSelectFormControlSettings>setting).keyFields)) {
                                entityToSave[setting.field][i].entityState = EntityStateType.Unchanged;
                            }
                            else {
                                entityToSave[setting.field][i].entityState = EntityStateType.Added;
                                entityToSave[setting.field][i].typeFullName = setting.multiSelectTemplate.modelType;
                            }
                        }
                    }

                    if (originalEntity[setting.field] && originalEntity[setting.field].length) {
                        for (let i in originalEntity[setting.field]) {
                            if (!this.itemExists<EntityType>(originalEntity[setting.field][i], entityToSave[setting.field] || [], (<IMultiSelectFormControlSettings>setting).keyFields)) {
                                //Add the deleted field
                                let newObject: any = originalEntity[setting.field][i];
                                newObject.entityState = EntityStateType.Deleted;
                                //Set its entity state to deleted.
                                entityToSave[setting.field].push(newObject);
                            }
                        }
                    }
                }
                else {
                    //I don't think we care if setting.abstractControlType = abstractControlKind.formControl
                }
            }
        }

        return entityToSave;
    }

    public itemExists<T>(item: T, arr: T[], matchprops: string[]): boolean {
        return ListManager.itemExists<T>(item, arr, matchprops);
    }

    public mergeLists<T>(arr1: T[], arr2: T[], matchprops: string[]): T[] {
        return ListManager.mergeLists<T>(arr1, arr2, matchprops);
    }

    public renameProperties<T>(arr1: T[], props1: string[], props2: string[]) {
        arr1.forEach(element => {
            for (let i in props1) {
                if (props1[i] !== props2[i]) {
                    Object.defineProperty
                        (
                        element,
                        props2[i],
                        Object.getOwnPropertyDescriptor(element, props1[i])
                        );
                    delete element[props1[i]];
                }
            }
        });

        return arr1;
    }
}

export class ListManager {
    static itemExists<T>(item: T, arr: T[], matchprops: string[]): boolean {
        if (!matchprops) {
            throw new Error("Key fields are required: (ListManagerService.itemExists)");
        }
        let found: boolean = false;
        for (let i in arr) {
            let allKeysMetch: boolean = true;
            for (let j in matchprops) {
                if (item[matchprops[j]] !== arr[i][matchprops[j]]) {
                    allKeysMetch = false;
                    break;
                }
            }
            if (allKeysMetch) {
                found = true;
                break;
            }
        }

        return found;
    }

    static mergeLists<T>(arr1: T[], arr2: T[], matchprops: string[]): T[] {
        let arr3: T[] = [];
        for (let i in arr1) {
            var shared = false;
            for (let j in arr2) {
                let allKeysMetch: boolean = true;
                if (matchprops && matchprops.length) {
                    for (let k in matchprops) {
                        if (arr2[j][matchprops[k]] != arr1[i][matchprops[k]]) {
                            allKeysMetch = false;
                            break;
                        }
                    }
                }
                else {
                    if (arr2[j] != arr1[i]) {
                        allKeysMetch = false;
                    }
                }

                if (allKeysMetch) {
                    shared = true;
                    break;
                }
            }

            if (!shared) arr3.push(arr1[i])
        }

        return arr3.concat(arr2);
    }

    static removeItems<T>(arr1: T[], arr2: T[], matchprops: string[]): T[] {
        let arr3: T[] = [];
        for (let i in arr1) {
            var shared = false;
            for (let j in arr2) {
                let allKeysMetch: boolean = true;
                if (matchprops && matchprops.length) {
                    for (let k in matchprops) {
                        if (arr2[j][matchprops[k]] != arr1[i][matchprops[k]]) {
                            allKeysMetch = false;
                            break;
                        }
                    }
                }
                else {
                    if (arr2[j] != arr1[i]) {
                        allKeysMetch = false;
                    }
                }

                if (allKeysMetch) {
                    shared = true;
                    break;
                }
            }

            if (!shared) arr3.push(arr1[i])
        }

        return arr3;
    }
}
