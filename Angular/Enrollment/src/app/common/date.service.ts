import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class DateService {

    constructor() { }

    convertToDate(e): Date {
        if (!e)
            return null;
            
        var parts = e.split('-');

        if (parts.length > 2 && !isNaN(parseInt(parts[0])) && !isNaN(parseInt(parts[1])) && !isNaN(parseInt(parts[2]))) {
            let year = parseInt(parts[0]);
            let month = parseInt(parts[1]);
            let day = parseInt(parts[2]);

            return new Date(year, month - 1, day);
        }
    }
}
