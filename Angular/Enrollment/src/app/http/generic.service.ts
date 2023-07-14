
import {throwError as observableThrowError,  Observable } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IFormRequestDetails, IRequestDetails } from '../stuctures/screens/i-request-details';
import { tap, map, catchError } from 'rxjs/operators';
import { IDeleteItemServiceRequest } from '../stuctures/requests/i-delete-item-service-request';
import { IGetItemServiceRequest } from '../stuctures/requests/i-get-item-service-request';
import { IGetListServiceRequest } from '../stuctures/requests/i-get-list-service-request';
import { IInsertItemServiceRequest } from '../stuctures/requests/i-insert-item-service-request';
import { IUpdateItemServiceRequest } from '../stuctures/requests/i-update-item-service-request';
import { IBaseModel } from '../stuctures/screens/i-base-model'
import { UrlsService } from '../http/urls.service'

@Injectable({
  providedIn: 'root'
})
export class GenericService {

  constructor(private _http: HttpClient, _urls: UrlsService) { 
    this.baseUrl = _urls.crudUrl;
  }

  private baseUrl: string;

  getItem(requestDetails: IFormRequestDetails): Observable<any> {
    let request: IGetItemServiceRequest = this.getGetItemRequest(requestDetails);
    return this._http.post<any>(new URL(requestDetails.getUrl, this.baseUrl).href, JSON.stringify(request), this.getPostOptions()).pipe
      (
        tap(({ entity, success, errorMessages }: any) => 
        {
          console.log("entity: " + JSON.stringify(entity));
          console.log("success: " + JSON.stringify(success));
          console.log("errorMessages: " + JSON.stringify(errorMessages));
        }
      ),
      map(({ entity }: any) => entity),
      catchError(this.handleError)
      );
  }

  getList(requestDetails: IRequestDetails, fieldsSelector: any): Observable<any> {
    let request: IGetListServiceRequest = this.getGetListRequest(requestDetails, fieldsSelector);
    return this._http.post<any>(new URL(requestDetails.dataSourceUrl, this.baseUrl).href, JSON.stringify(request), this.getPostOptions()).pipe
      (
        tap(({ list, success, errorMessages }: any) => 
        {
          console.log("list: " + JSON.stringify(list));
          console.log("success: " + JSON.stringify(success));
          console.log("errorMessages: " + JSON.stringify(errorMessages));
        }
      ),
      map(({ list }: any) => list),
      catchError(this.handleError)
      );
  }

  deleteItem(item: IBaseModel, requestDetails: IFormRequestDetails): Observable<Response> {
    let request: IDeleteItemServiceRequest = this.getDeleteItemRequest(item);
    return this._http.post<Response>(new URL(requestDetails.deleteUrl, this.baseUrl).href, JSON.stringify(request), this.getPostOptions()).pipe
      (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError)
      );
  }

  updateItem(item: IBaseModel, requestDetails: IFormRequestDetails): Observable<Response> {
    let request: IUpdateItemServiceRequest = this.getUpdateItemRequest(item);
    return this._http.post<Response>(new URL(requestDetails.updateUrl, this.baseUrl).href, JSON.stringify(request), this.getPostOptions()).pipe
      (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError)
      );
  }

  insertItem(item: IBaseModel, requestDetails: IFormRequestDetails): Observable<Response> {
    let request: IInsertItemServiceRequest = this.getInsertItemRequest(item);
    return this._http.post<any>(new URL(requestDetails.addUrl, this.baseUrl).href, JSON.stringify(request), this.getPostOptions()).pipe
      (
      tap(data => console.log(JSON.stringify(data))),
      catchError(this.handleError)
      );
  }

  private getPostOptions() : { [key: string]: HttpHeaders } {
    return { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
  }

  private getGetItemRequest(requestDetails: IFormRequestDetails): IGetItemServiceRequest {
    return {
      modelType: requestDetails.modelType,
      dataType: requestDetails.dataType,
      selectExpandDefinition: requestDetails.selectExpandDefinition,
      filter: requestDetails.filter
    };
  }

  private getGetListRequest(requestDetails: IRequestDetails, fieldsSelector: any): IGetListServiceRequest {
    return {
      modelType: requestDetails.modelType,
      dataType: requestDetails.dataType,
      modelReturnType: requestDetails.modelReturnType,
      dataReturnType: requestDetails.dataReturnType,
      selectExpandDefinition: requestDetails.selectExpandDefinition,
      selector: fieldsSelector
    };
  }

  private getDeleteItemRequest(item: any): IDeleteItemServiceRequest {
    return {
      entity: item
    };
  }

  private getInsertItemRequest(item: any): IInsertItemServiceRequest {
    return {
      entity: item
    };
  }

  private getUpdateItemRequest(item: any): IUpdateItemServiceRequest {
    return {
      entity: item
    };
  }

  private handleError(error: Response) {
    console.error(JSON.stringify(error));
    return observableThrowError(error || 'Server error');
  }
}
