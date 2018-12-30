import { Component, OnInit, TemplateRef, ViewChild, Input } from '@angular/core';
import { UiNotificationService } from '../common/ui-notification.service';
import { ICommandButton } from '../stuctures/i-command-button';
import { IHtmlPageSettings, IContentTemplate, IMessageTemplate } from '../stuctures/screens/html/i-html-page-settings';
import { ViewTypeEnum } from '../stuctures/screens/i-view-type';

@Component({
  selector: 'app-html-page',
  templateUrl: './html-page.component.html',
  styleUrls: ['./html-page.component.css']
})
export class HtmlPageComponent implements OnInit {
  @ViewChild('welcomeTemplate') welcomeTemplate: TemplateRef<any>;
  @ViewChild('messageTemplate') messageTemplate: TemplateRef<any>;

  @Input() public settings: IHtmlPageSettings;
  @Input() public commandButtons: ICommandButton[];

  constructor(private _uiNotificationService: UiNotificationService) { }

  public getTemplate(templateName: string) {
    return this[templateName];
  }

  public getContentContext(contentTemplate: IContentTemplate) {
    return {
      $implicit: contentTemplate
    }
  }

  public getMessageContext(messageTemplate: IMessageTemplate) {
    return {
      $implicit: messageTemplate
    }
  }

  ngOnInit() {
  }

  navigateNext(button: ICommandButton) {
    this._uiNotificationService.navigateNext({
      viewType: ViewTypeEnum.Html,
      commandButtonRequest: { newSelection: button.shortString, cancel: button.cancel }
    });
  }

}
