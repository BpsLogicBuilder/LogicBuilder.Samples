import { EntityStateType } from "./entity-state-type";

export interface IBaseModel {
    entityState: EntityStateType;
    typeFullName?: string;
    [x: string]: any;
}

export interface EntityType extends IBaseModel {
}