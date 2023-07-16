
import {throwError as observableThrowError,  Observable } from 'rxjs';
import { Injectable, Inject } from '@angular/core';
import { ProgressService } from '../common/progress.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DataSourceRequestState, toDataSourceRequest, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { IGridRequestDetails } from '../stuctures/screens/i-request-details';
import { IGridResult } from '../stuctures/screens/grid/i-grid-result';
import { KendoGridDataRequest } from '../stuctures/screens/kendo-grid-data-request';
import { tap, map, catchError } from 'rxjs/operators';
import { UrlsService } from '../http/urls.service'

@Injectable({
  providedIn: 'root'
})
export class GridService {

  constructor(private _http: HttpClient, private progressService: ProgressService, _urls: UrlsService) { 
    this.baseUrl = _urls.gridUrl;
  }

  private baseUrl: string;

  public fetch(state: DataSourceRequestState, requestDetails: IGridRequestDetails): Observable<IGridResult> {
    const hasGroups = state.group && state.group.length;
    let request: KendoGridDataRequest = {
      options: toDataSourceRequest(state),
      modelType: requestDetails.modelType,
      dataType: requestDetails.dataType,
      selectExpandDefinition: requestDetails.selectExpandDefinition
    };

    return this._http
      .post<IGridResult>(`${this.baseUrl}${requestDetails.dataSourceUrl}`, JSON.stringify(request), this.getPostOptions())
      .pipe
      (
      tap(({ data, total, aggregateResults }: any) => {
        // console.log("Data: " + JSON.stringify(data));
        // console.log("Total: " + JSON.stringify(total));
        // console.log("AggregateResults: " + JSON.stringify(aggregateResults));
      }),
      map(({ data, total, aggregateResults }: any) => // process the response
        (<IGridResult>{
          //if there are groups convert them to compatible format
          data: hasGroups ? translateDataSourceResultGroups(data) : data,
          total: total,
          aggregateResult: this.progressService.translateAggregateResults(aggregateResults)
        })),
      catchError(this.handleError)
      );
  }

  private getPostOptions() : { [key: string]: HttpHeaders } {
    return { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
  }

  private handleError(error: Response) {
    //console.error(JSON.stringify(error));
    return observableThrowError(error || 'Server error');
  }
}
