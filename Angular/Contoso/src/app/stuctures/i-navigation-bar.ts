import { INavigationMenuItem } from "./i-navigation-menu-item";

export interface INavigationBar {
    brandText: string;
    currentModule: number;
    menuItems: INavigationMenuItem[];
}