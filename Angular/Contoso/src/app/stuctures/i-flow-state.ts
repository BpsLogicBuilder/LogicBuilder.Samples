export interface IFlowState {
    driver: string;
    selection: string;
    callingModuleDriverStack: string[];
    callingModuleStack: string[];
    moduleBeginName: string;
    moduleEndName: string;
}