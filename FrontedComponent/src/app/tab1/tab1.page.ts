import { Component } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { environment } from 'src/environments/environment';
import { ModalPopupPage } from '../modal-popup-ing/modal-popup.page';
import { ModalServiceService } from '../modal-service.service';
import { CategoryService } from '../services/Ingrediente/category.service';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})


export class Tab1Page {
  categorii:Array<any> = []
  imagesUrl = environment.imagesUrl
  itemsInCart: number;


  constructor(private categoriiService: CategoryService, private modalService:ModalServiceService) {

  }

  async ngOnInit(){
    this.categorii = await this.categoriiService.getCategoriiIngrediente()
    console.log("test init");

  }

  ionViewWillEnter(){
    var items = JSON.parse(localStorage.getItem('cart'))
    this.itemsInCart = Object.values(items).length;
  }

  openCart(){
     this.modalService.openCartModal()
  }
  async openCardModal(subcategorie){
    await this.modalService.openIngredientModal(subcategorie);
  }

//buttonActive: boolean = true;
}
