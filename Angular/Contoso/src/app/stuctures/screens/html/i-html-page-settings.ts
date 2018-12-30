export interface IHtmlPageSettings {
    contentTemplate?: IContentTemplate;
    messageTemplate?: IMessageTemplate;
}

export interface IContentTemplate {
    templateName: string;
    title: string;
}

export interface IMessageTemplate {
    templateName: string;
    caption: string;
    message: string;
}