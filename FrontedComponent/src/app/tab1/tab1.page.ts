import { Component } from '@angular/core';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})


export class Tab1Page {
  categorii:Array<any> = []
  constructor(private categoriiService: CategoryService) {

  }

  ngOnInit(){
    this.categoriiService.getCategoriiIngrediente()
    .then((results)=>{
      console.log(results);
      this.categorii = results;
    });
  }

buttonActive: boolean = true;
}
