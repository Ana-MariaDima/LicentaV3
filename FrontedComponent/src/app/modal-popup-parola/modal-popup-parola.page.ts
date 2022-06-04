import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators,FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalController } from '@ionic/angular';
import { ModalServiceService } from '../modal-service.service';
import { RegisterService } from '../services/Register/register.service';

@Component({
  selector: 'app-modal-popup-parola',
  templateUrl: './modal-popup-parola.page.html',
  styleUrls: ['./modal-popup-parola.page.scss'],
})

  export class ModalPopupParolaPage implements OnInit {
    public myForm!: FormGroup;
    error_messages: any;

  constructor(private modalService:ModalServiceService, private fromBuilder: FormBuilder, private modalController: ModalController ,private registerService: RegisterService, private router: Router) { }

  ngOnInit(): void {
    this.myForm = this.fromBuilder.group({
      Oldpassword: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmpassword: ['', [Validators.required, Validators.minLength(8)]]

     } ,
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


  async dismiss() {
    const close: string = "Modal Removed";
    return this.modalController.dismiss({customProp:"123"});
  }

  doUpdate() {
//console.log("doUpdate", this.myForm.valid, this.myForm);

    if (this.myForm.valid) {
      console.log("Valid")
      console.log(this.myForm.value);
      var Token=localStorage.getItem('token').toString();
      console.log("token", Token);


      this.registerService.update(this.myForm.value, Token).then((result: any) => {
        if (result.isSuccess) {
          localStorage.setItem('token', result.token);
          this.router.navigate(['home']);
          this.dismiss();

        } else {
          alert("Schimbarea parolei a eșuat!\nVechea parola nu este cea corectă.");
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
