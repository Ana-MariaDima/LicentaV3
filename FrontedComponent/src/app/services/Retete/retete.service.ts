import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReteteService {

  constructor(private http:HttpClient) { }

    getRetete():Promise<any>{
      return this.http.get(environment.baseUrl+"Retete").toPromise();
    }

}
