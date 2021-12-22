import { Injectable } from '@angular/core';
import { AggregateResult } from '@progress/kendo-data-query';

@Injectable({
  providedIn: 'root'
})
export class ProgressService {

  constructor() { }

  private set(field, target, value) {
    target[field] = value;
    return target;
  }

  public translateAggregateResults(data: any[]): AggregateResult {
    return (
      (data || []).reduce((acc, x) => this.set(x.Member || x.member, acc, this.set((x.AggregateMethodName || x.aggregateMethodName).toLowerCase(), acc[x.Member || x.member] || {}, x.Value || x.value)), {})
    );
  }
}