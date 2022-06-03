
import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/Authentication/auth.service';
import { User } from 'src/app/interfaces/user';


@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss']
})
export class LoginPage implements OnInit{

 validation:any ;
  public user: User ={
    username:'',
    password:''

  };
  public error:boolean | string=false;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {}

  validateInputs() {
    let username = this.user.username.trim();
    let password = this.user.password.trim();

    return (
      this.user.username &&
                    this.user.password &&
                    username.length > 0 &&
                    password.length > 0
    );
  }

  doLogin():void {

    this.error=false;

      this.authService.login(this.user).then((response: any) =>{

        if(response && response.token)
        {
          localStorage.setItem('token', response.token);

          this.router.navigateByUrl("/home")
        }else{
          console.log();
        }
      }).catch((failure)=>{
         if(!failure.error.message){
           console.warn(failure);
           return;
         }

           alert(failure.error.message)
      });

  }
  ngOnDestroy() {}
  ngOnChanges (){

  }
}
