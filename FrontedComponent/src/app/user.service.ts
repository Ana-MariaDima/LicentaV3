import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }

  async getUserInfo(){
    return (await this.http.post(environment.baseUrl+"Users/GetUserInfo",{Token:localStorage.getItem('token')}).toPromise());
  }
}
