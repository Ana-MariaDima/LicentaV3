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
  error_messages: any;

  constructor(private fromBuilder: FormBuilder, private registerService: RegisterService, private router: Router) { }

  ngOnInit(): void {
    this.myForm = this.fromBuilder.group({
      firstName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      lastName: ['', [Validators.required]],
      username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      sex: ['', Validators.required],
      varsta: ['', Validators.required],
      confirmpassword: ['', [Validators.required, Validators.minLength(8)]]

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

  public error: boolean | string = false;


  doRegister() {


    if (this.myForm.valid) {
      //console.log("Valid")
      this.registerService.register(this.myForm.value).then((result: any) => {
        if (result.isSuccess) {
          localStorage.setItem('token', result.token);
          this.router.navigate(['home']);
        } else {
          alert("Inregistrare eșuată!\Nume de utilizator sau e-mail deja existent.  Încercați să vă logați!");
        }
      }).catch((failure) => {
        if (!failure.error.message) {
          console.warn(failure);
          return;
        }

        alert(failure.error.message)
      });

    }
  }
}
