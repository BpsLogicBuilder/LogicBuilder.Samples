export interface ICommandButton {
    id: number;
    shortString: string;
    longString: string;
    cancel?: boolean;
    gridId?: number;
    gridCommandButton?: boolean;
    buttonIcon?: string;
    classString?: string;
}