import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ReteteService } from '../services/Retete/retete.service';

@Component({
  selector: 'app-tab1-toate-retetele',
  templateUrl: './tab1-toate-retetele.page.html',
  styleUrls: ['./tab1-toate-retetele.page.scss'],
})
export class Tab1ToateRetetelePage  {

  constructor(private ReteteService: ReteteService) { }
  retete:Array<any> = []
  tip:Array<any> = []
  async ngOnInit() {
    environment.baseUrl
    this.retete = await this.ReteteService.getRetete();


    console.log(this.retete)
    // .then((results)=>{
    //  console.log(results);
    //   this.retete = results;
    // });
  }
  }
