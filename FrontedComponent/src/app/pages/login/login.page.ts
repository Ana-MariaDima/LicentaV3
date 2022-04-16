
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


  public user: User ={
    username:'',
    password:''

  };
  public error:boolean | string=false;



  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {}

  validateInputs() {
    //console.log(this.postData);
    let username = this.user.username.trim();
    let password = this.user.password.trim();
    return (
      this.user.username &&
      this.user.password &&
      username.length > 0 &&
      password.length > 0
    );
  }

 /* loginAction() {
    if (this.validateInputs()) {
      this.authService.login(this.user).subscribe(
        (res: any) => {
          if (res.userData) {
            // Storing the User data.
            this.storageService
              .store(AuthConstants.AUTH, res.userData)
              .then(res => {
                this.router.navigate(['home']);
              });
          } else {
            this.toastService.presentToast('Incorrect username and password.');
          }
        },
        (error: any) => {
          this.toastService.presentToast('Network Issue.');
        }
      );
    } else {
      this.toastService.presentToast(
        'Please enter email/username or password.'
      );
    }
  }*/
  doLogin():void {
    this.error=false;
    //console.log('Login Clicked', this.user);

    // if(this.validateEmail(this.user.username))
    // {
     // console.log('Login Clicked2');
      this.authService.login(this.user).subscribe((response: any) =>{
        //console.log(response);
        if(response && response.token)
        {
          localStorage.setItem('token', response.token);
          //this.router.navigate(['/home'],{replaceUrl:true});
          this.router.navigateByUrl("/home")
        }
      })
    // }
    // else{
    //   this.error="Email is not valid";

    // }

  }

  validateEmail(email:string) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }








  ngOnDestroy() {}
  ngOnChanges (){ }
}
