import { Injectable } from '@angular/core';
import { ConfigValue } from '../model/configValue';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReadConfigService {

  constructor(private http: HttpClient) { }

  currentKeySelected$ : BehaviorSubject<ConfigValue> = new BehaviorSubject<ConfigValue>({key: '', value: ''});

  getByValue(key: string) : void {
     this.http.get<ConfigValue>(`${environment.baseUrl}readConfig/${key}`)
              .subscribe(
                configValue => this.currentKeySelected$.next(configValue), 
                e => console.error(e) // todo show error instead
              );
  }
}
