import { Component, OnInit } from '@angular/core';
import { UiNotificationService } from '../common/ui-notification.service';
import { INavigationBar } from '../stuctures/i-navigation-bar';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
  animations: [
    trigger('itemState', [
      state('inactive', style({
        background: "inherit", color: "inherit"
      })),
      state('active',   style({
        background: "#4189C7", color: "white"
      })),
      transition('inactive => active', animate('0.2s 100ms ease-in')),
      transition('active => inactive', animate('0.2s 100ms ease-out'))
    ])
  ]
})
export class NavBarComponent implements OnInit {

  constructor(private _notificationService: UiNotificationService) {
    _notificationService.navBar.subscribe(nb => {
      this.navBar = nb;
    });
  }

  public navBar: INavigationBar;
  public isCollapsed: boolean = false;

  ngOnInit() {
  }

  menuItemClick(stage: number, mod: string) {
    console.log("menuItemClick");
    this._notificationService.navStart({
      initialModuleName: mod,
      targetModule: stage
    });
  }

  doNothing() {
    console.log("Do Nothing");
  }
}
