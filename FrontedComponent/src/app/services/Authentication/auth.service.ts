import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../../interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl:string=environment.baseUrl;
  //poate sa difere
  private publicHeaders={
    headers: new HttpHeaders({
      'content-type':'application/json',
    }),

  };

  constructor(private http:HttpClient) { }

  login(data:User)
  {
   // console.log("creating user", data)
    return this.http.post(this.baseUrl+"Users/authenticate",
                          data,
                          this.publicHeaders)
  }
}
