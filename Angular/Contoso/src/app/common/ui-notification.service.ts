import { Injectable } from '@angular/core';
import { SettingsService } from '../http/settings.service';
import { INavigationBar } from '../stuctures/i-navigation-bar';
import { Subject } from 'rxjs';
import { IScreenSettingsBase } from '../stuctures/screens/i-screen-settings-base';
import { IFlowState } from '../stuctures/i-flow-state';
import { IRequestsBase } from '../stuctures/screens/requests/i-requests-base';
import { INavBarRequest } from '../stuctures/screens/requests/i-nav-bar-request';

@Injectable({
  providedIn: 'root'
})
export class UiNotificationService {

  constructor(private _settingsService: SettingsService) { 
    this.navBar =  new Subject<INavigationBar>();
    this.screenSettings =  new Subject<IScreenSettingsBase>();
  }

  public navBar: Subject<INavigationBar>;
  public screenSettings: Subject<IScreenSettingsBase>;
  public flowState: IFlowState;
  public errorMessage: any;

  public start(): void {
    this._settingsService.start().subscribe(
      itm => {
        this.navBar.next(itm.navigationBar);
        this.screenSettings.next(itm.screenSettings);
        this.flowState = itm.flowState;
      },
      error => this.errorMessage = <any>error);
  }

  public navStart(request: INavBarRequest): void {
    this._settingsService.navStart(request).subscribe(
      itm => {
        this.navBar.next(itm.navigationBar);
        this.screenSettings.next(itm.screenSettings);
        this.flowState = itm.flowState;
      },
      error => this.errorMessage = <any>error);
  }

  public navigateNext(request: IRequestsBase): void {
    request.flowState = this.flowState;
    this._settingsService.navigateNext(request).subscribe(
      itm => {
        if (itm.screenSettings.validationResults && itm.screenSettings.validationResults.length)
        {
          this.screenSettings.next(itm.screenSettings);
        }
        else
        {
          this.navBar.next(itm.navigationBar);
          this.screenSettings.next(itm.screenSettings);
          this.flowState = itm.flowState;
        }
      },
      error => this.errorMessage = <any>error);
  }
}
