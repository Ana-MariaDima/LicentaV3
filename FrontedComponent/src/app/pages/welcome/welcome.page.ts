import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.page.html',
  styleUrls: ['./welcome.page.scss'],
})
export class WelcomePage implements OnInit {
  public test:String = "";
  constructor(private router: Router) { }


  navigateToLogin() {
    this.router.navigate(['/login']);
   // console.log(this.test)
  }

  ngOnInit() {
  }

}
