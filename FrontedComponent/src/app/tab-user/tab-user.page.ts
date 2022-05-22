import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserService } from '../user.service';

@Component({
  selector: 'app-tab-user',
  templateUrl: './tab-user.page.html',
  styleUrls: ['./tab-user.page.scss'],
})
export class TabUserPage implements OnInit {

  userInfo = {
    firstName:"-",
    lastName:"-",
    interactions: 0,
    numeRetetaApreciata:"-",
    email:"-"
  }
  imageBase = environment.imagesUrl;
  userLoaded = false;
  constructor(private userService: UserService) { }
  ngOnInit(): void {

  }
  async ionViewWillEnter() {
   this.userInfo = (await this.userService.getUserInfo()) as any
   this.userLoaded = true;
   console.log(this.imageBase);
  }

  getLabel(){
    if(!this.userLoaded) return "-";

    if(this.userInfo.interactions < 10)
      return "Rookie"

    if(this.userInfo.interactions >= 10 && this.userInfo.interactions < 20)
      return "Junior"

    if(this.userInfo.interactions >= 20 && this.userInfo.interactions < 50)
      return "Senior"

    if(this.userInfo.interactions >= 50)
      return "Master"

    return "Unknown";
  }


  logOut(){
    //var token =localStorage.getItem('token');
    localStorage.removeItem("token");
  }
}
