import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UrlsService {

  constructor() { }

  crudUrl: string = 'http://localhost:7878';
  gridUrl: string = 'http://localhost:12055';
  workflowUrl: string = 'http://localhost:12479';
}
