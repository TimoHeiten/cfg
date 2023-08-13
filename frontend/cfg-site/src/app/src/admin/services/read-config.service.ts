import { Injectable } from '@angular/core';
import { ConfigValue } from '../model/configValue';
import { BehaviorSubject, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ConfigDataService {

  constructor(private http: HttpClient) { }

  allConfigValues$ : BehaviorSubject<ConfigValue[]> = new BehaviorSubject<ConfigValue[]>([]);
  currentKeySelected$ : BehaviorSubject<ConfigValue> = new BehaviorSubject<ConfigValue>({key: '', value: ''});

  getByValue(key: string) : void {
     this.http.get<ConfigValue>(`${environment.baseUrl}readConfig/${key}`)
                .pipe(tap(configValue => this.currentKeySelected$.next(configValue)),
                      catchError(this.handleError)
                )
              .subscribe();
  }

  all() : void {
    this.http.get<ConfigValue[]>(`${environment.baseUrl}readConfig`)
            .pipe(tap(configValues => this.allConfigValues$.next(configValues)),
                  catchError(this.handleError)
            )
             .subscribe();
  }

  create(newValue : ConfigValue) : void {
    this.http.post<ConfigValue>(`${environment.baseUrl}mutateConfig`, newValue)
              .pipe(
                tap(configValue => this.currentKeySelected$.next(configValue)),
                catchError(this.handleError))
              .subscribe();
  }

  update(newValue : ConfigValue) : void {
    this.http.put<ConfigValue>(`${environment.baseUrl}mutateConfig`, newValue)
              .pipe(
                tap(configValue => this.currentKeySelected$.next(configValue)),
                catchError(this.handleError))
              .subscribe();
  }

  delete(deleteThisKey : string) : void {
    this.http.delete<ConfigValue>(`${environment.baseUrl}mutateConfig/${deleteThisKey}`)
              .pipe(
                tap(_ => this.all()),
                catchError(this.handleError))
              .subscribe();
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
}
