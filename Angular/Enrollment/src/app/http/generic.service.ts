
import {throwError as observableThrowError,  Observable } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DataSourceRequestState, toDataSourceRequest } from '@progress/kendo-data-query';
import { IRequestDetails } from '../stuctures/screens/i-request-details';
import { DataRequest } from '../stuctures/screens/data-request';
import { tap, catchError } from 'rxjs/operators';
import { IPostModelRequest } from '../common/i-post-model-request';

@Injectable({
  providedIn: 'root'
})
export class GenericService {

  constructor(private _http: HttpClient, @Inject('baseUrl') private baseUrl) { }

  getItem(state: DataSourceRequestState, requestDetails: IRequestDetails): Observable<any> {
    let request: DataRequest = this.getDataRequest(state, requestDetails);
    return this._http.post<any>(`${this.baseUrl}${requestDetails.getUrl}`, JSON.stringify(request), this.getPostOptions()).pipe
      (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError)
      );
  }

  deleteItem(state: DataSourceRequestState, requestDetails: IRequestDetails): Observable<Response> {
    let request: DataRequest = this.getDataRequest(state, requestDetails);
    return this._http.post<Response>(`${this.baseUrl}${requestDetails.deleteUrl}`, JSON.stringify(request), this.getPostOptions()).pipe
      (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError)
      );
  }

  updateItem(state: DataSourceRequestState, requestDetails: IRequestDetails, item: any): Observable<Response> {
    let request: IPostModelRequest = this.getPostModelRequest(state, requestDetails, item);
    return this._http.post<Response>(`${this.baseUrl}${requestDetails.updateUrl}`, JSON.stringify(request), this.getPostOptions()).pipe
      (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError)
      );
  }

  insertItem(state: DataSourceRequestState, requestDetails: IRequestDetails, item: any): Observable<Response> {
    let request: IPostModelRequest = this.getPostModelRequest(state, requestDetails, item);
    return this._http.post<any>(`${this.baseUrl}${requestDetails.addUrl}`, JSON.stringify(request), this.getPostOptions()).pipe
      (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError)
      );
  }

  private getPostOptions() : { [key: string]: HttpHeaders } {
    return { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
  }

  private getPostModelRequest(state: DataSourceRequestState, requestDetails: IRequestDetails, item: any): IPostModelRequest {
    return {
      options: toDataSourceRequest(state),
      modelType: requestDetails.modelType,
      dataType: requestDetails.dataType,
      model: item
    };
  }

  private getDataRequest(state: DataSourceRequestState, requestDetails: IRequestDetails): DataRequest {
    return {
      options: toDataSourceRequest(state),
      modelType: requestDetails.modelType,
      dataType: requestDetails.dataType,
      includes: requestDetails.includes,
      selects: requestDetails.selects
    };
  }

  private handleError(error: Response) {
    console.error(JSON.stringify(error));
    return observableThrowError(error || 'Server error');
  }
}
