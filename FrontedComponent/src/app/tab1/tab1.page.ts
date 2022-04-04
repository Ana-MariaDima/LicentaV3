import { Component } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { environment } from 'src/environments/environment';
import { ModalPopupPage } from '../modal-popup/modal-popup.page';
import { CategoryService } from '../services/Ingrediente/category.service';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss']
})


export class Tab1Page {
  categorii:Array<any> = []
  imagesUrl = environment.imagesUrl
  constructor(private categoriiService: CategoryService, private modalController: ModalController) {

  }

  async ngOnInit(){
    this.categorii = await this.categoriiService.getCategoriiIngrediente()
    console.log(this.categorii, "test");
  }

  async openCardModal(subcategorie){
    console.log("showing ",subcategorie)
    var modal = await this.modalController.create({
      component:ModalPopupPage,
      cssClass:"modalTest",
      componentProps: {
        'model_title':subcategorie.nume_Subcategorie_ingredient,
        'ingrediente':subcategorie.ingrediente
      }
    })

    modal.present()

    const {data} = await modal.onWillDismiss();0
    console.log("modal returned data", data)
  }

//buttonActive: boolean = true;
}
