import { Component, OnInit } from '@angular/core';
import { UserToRegister } from 'src/app/interfaces/user-to-register';
import { Router } from '@angular/router';
import { RegisterService } from 'src/app/services/Register/register.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-singup',
  templateUrl: './singup.page.html',
  styleUrls: ['./singup.page.scss'],
})
export class SingupPage implements OnInit {
  public myForm!: FormGroup;
  error_messages : any;

  constructor(private fromBuilder:FormBuilder,private registerService: RegisterService, private router: Router) { }

  ngOnInit(): void {
    this.myForm=this.fromBuilder.group({
      firstName: ['',[Validators.required]],
      email:['',[Validators.required, Validators.email]],
      lastName:['',[Validators.required]],
      username:['',[Validators.required]],
      password:['',[Validators.required, Validators.minLength(8)]],
      sex:['', Validators.required],
      varsta:['', Validators.required],
      confirmpassword:['',[Validators.required, Validators.minLength(8)]]

    },
    {
      validators: this.password.bind(this)
    }


    );
  }
  password(formGroup: FormGroup) {
    const { value: password } = formGroup.get('password');
    const { value: confirmPassword } = formGroup.get('confirmpassword');
    return password === confirmPassword ? null : { passwordNotMatch: true };

  }



  public error:boolean | string=false;


  doRegister()
  {
    //console.log(this.myForm);


    if(this.myForm.valid)
    {
  console.log("Valid")
      this.registerService.register(this.myForm.value).then((result: any)=>{
       console.log(result)
        if(result.isSuccess){
          localStorage.setItem('token', result.token);
          console.log('token set before redirec')
          this.router.navigate(['home']);
        }else{
          alert("Sign up failed!\nUsername or email maight already exist. Try to login instead!");
        }
      }).catch((failure)=>{
        if(!failure.error.message){
          console.warn(failure);
          return;
        }

          alert(failure.error.message)
     });






    }
  }

  /*doRegister():void {
    this.error=false;
    console.log('SingUp Clicked', this.user);

    if(this.validateEmail(this.user.username))
    {
      this.registerService.register(this.user).subscribe((response: any) =>{
       // console.log(response);
        if(response )
        {
          console.log("yesss");
          //localStorage.setItem('token', response.token);
          this.router.navigate(['/tabs/tab1']);
        }
      })
    }
    else{
      this.error="Email is not valid";

    }*/

  }
/*
  validateEmail(email:string) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
  }*/








