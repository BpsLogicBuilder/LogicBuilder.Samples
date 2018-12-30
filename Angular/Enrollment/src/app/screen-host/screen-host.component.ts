import { Component, OnInit } from '@angular/core';
import { UiNotificationService } from '../common/ui-notification.service';
import { IScreenSettingsBase  } from '../stuctures/screens/i-screen-settings-base';
import { ViewTypeEnum } from '../stuctures/screens/i-view-type';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-screen-host',
  templateUrl: './screen-host.component.html',
  styleUrls: ['./screen-host.component.css'],
  // animations: [
  //   trigger('flyInOut', [
  //     state('in', style({transform: 'translateX(0)'})),
  //     transition('void => *', [
  //       style({transform: 'translateX(-100%)'}),
  //       animate(100)
  //     ]),
  //     transition('* => void', [
  //       animate(100, style({transform: 'translateX(100%)'}))
  //     ])
  //   ])
  // ]
  animations: [
  trigger('flyIn', [
    state('in', style({opacity: 1, transform: 'translateX(0)'})),
    state('void', style({opacity: 1, transform: 'scale(0)'})),
    transition('void => *', [
      style({
        opacity: 0,
        transform: 'translateX(100%)'
      }),
      animate('0.3s ease-in')
    ]),
    // transition('* => void', [
    //   //animate(100, style({transform: 'translateX(100%)'}))
    //   //animate('0.3s ease-out', style({transform: 'scale(0)'}))
    //   style({
    //     opacity: 1,
    //     transform: 'scale(1)'
    //   }),
    //   animate('0.3s ease-out')
    // ])
  ]),
  trigger('jumpIn', [
    state('in', style({opacity: 1, transform: 'scale(1)'})),
    state('void', style({opacity: 1, transform: 'scale(0)'})),
    transition('void => *', [
      style({
        opacity: 0,
        transform: 'scale(0)'
      }),
      animate('1.5s 0.3s ease-in')
    ]),
    transition('* => void', [
      //animate(100, style({transform: 'translateX(100%)'}))
      //animate('0.3s ease-out', style({transform: 'scale(0)'}))
      style({
        opacity: 1,
        transform: 'scale(1)'
      }),
      animate('0.6s ease-out')
    ])
  ])
]
})
export class ScreenHostComponent implements OnInit {

  constructor(private _notificationService: UiNotificationService) { 
    _notificationService.screenSettings.subscribe(screen => {
      if (!(screen.validationResults && screen.validationResults.length))
      {
        this.screenSettings = null;
        let screenHost = this;
        setTimeout(function () {
          screenHost.screenSettings = screen;
        }, 10);
      }
      else
      {
        this.screenSettings = screen;
      }

      console.log("DialogFromSubscribe:" + JSON.stringify(screen));
   });
  }

  public viewType = ViewTypeEnum;
  public screenSettings: IScreenSettingsBase;
  ngOnInit() {
    this._notificationService.start();
  }

}
