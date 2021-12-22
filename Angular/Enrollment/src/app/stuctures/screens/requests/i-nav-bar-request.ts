import { TargetModuleType } from "../i-target-module-type";

export interface INavBarRequest {
    userId?: number;
    initialModuleName: string;
    targetModule: TargetModuleType;
}