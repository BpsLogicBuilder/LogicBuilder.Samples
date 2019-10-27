import { IFlowState } from "./i-flow-state";
import { INavigationBar } from "./i-navigation-bar";
import { IScreenSettingsBase  } from "./screens/i-screen-settings-base";
import { ICommandButton } from "./i-command-button";

export interface IFlowSettings{
    userId: number;
    flowState: IFlowState;
    navigationBar: INavigationBar;
    screenSettings: IScreenSettingsBase;
}