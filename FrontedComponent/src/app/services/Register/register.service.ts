import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserToRegister } from '../../interfaces/user-to-register';
import { HttpClient, HttpHeaders } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private baseUrl:string=environment.baseUrl;


  private publicHeaders={
    headers: new HttpHeaders({
      'content-type':'application/json',
    }),

  };

  constructor(private http:HttpClient) { }
  register(data:UserToRegister)
  {
    console.log("creating user", data);
    return this.http.post(this.baseUrl+"Users/create",
                          data,
                          this.publicHeaders).toPromise()
  }
}
