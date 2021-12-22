
import {throwError as observableThrowError,  Observable } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { IFlowSettings } from '../stuctures/i-flow-settings';
import { IRequestsBase } from '../stuctures/screens/requests/i-requests-base';
import { INavBarRequest } from '../stuctures/screens/requests/i-nav-bar-request';


@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private _http: HttpClient, @Inject('baseUrl') private baseUrl) { }

  start(): Observable<IFlowSettings> {
    return this._http.post<IFlowSettings>(`${this.baseUrl}/api/flow/Start`, JSON.stringify({}), this.getPostOptions()).pipe
      (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError)
      );
  }

  navStart(request: INavBarRequest): Observable<IFlowSettings> {
    return this._http.post<IFlowSettings>(`${this.baseUrl}/api/flow/NavStart`, JSON.stringify(request), this.getPostOptions()).pipe
      (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError)
      );
  }

  navigateNext(request: IRequestsBase): Observable<IFlowSettings> {
    return this._http.post<IFlowSettings>(`${this.baseUrl}/api/flow/Next`, JSON.stringify(request), this.getPostOptions()).pipe
      (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError)
      );
  }

  private getPostOptions() : { [key: string]: HttpHeaders } {
    return { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
  }

  private handleError(error: Response) {
    console.error(JSON.stringify(error));
    return observableThrowError(error || 'Server error');
  }
}
