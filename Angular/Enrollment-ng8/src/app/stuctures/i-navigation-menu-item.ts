export interface INavigationMenuItem {
    targetModule: number;
    initialModule: string;
    text: string;
    active: boolean;
    subMenuItems: INavigationMenuItem[];
}