import { Injectable } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ModalPopupCartPage } from './modal-popup-cart/modal-popup-cart.page';
import { ModalPopupIngAll } from './modal-popup-ing-toate/modal-popup-all.page';
import { ModalPopupPage } from './modal-popup-ing/modal-popup.page';
import { ModalPopupParolaPage } from './modal-popup-parola/modal-popup-parola.page';
import { ModalPopupPageRet } from './modal-popup-ret/modal-popup-ret.page';

@Injectable({
  providedIn: 'root'
})
export class ModalServiceService {
  cart:Array<any> = []
  constructor(private modalController:ModalController) {
  }

  pushToCart(x){
    this.cart.push(x)
  }

  getCart(){
    return this.cart;
  }

  async openCartModal(){
    var modal = await this.modalController.create({
      component:ModalPopupCartPage,
      cssClass:"modalTest",
      componentProps: {
      }
    })
    console.log(modal)
    modal.present();
    const {data} = await modal.onWillDismiss();
    console.log("popup inchis dupa await!")
  }

 async openIngredienteAllModal()
{
  var modal = await this.modalController.create({
    component:ModalPopupIngAll,
    cssClass:"modalTest",
    componentProps: {
    }
  })
 // console.log(modal)
  modal.present();
  const {data} = await modal.onWillDismiss();
 // console.log("popup inchis dupa await!")
}


async openParolaModal(){
  var modal = await this.modalController.create({
    component:ModalPopupParolaPage,
    cssClass:"modalTest",
    componentProps: {
    }
  })
  console.log(modal)
  modal.present();
  const {data} = await modal.onWillDismiss();
  console.log("popup inchis dupa await!")
}

  async openRetetaModal(reteta, options){

    var modal = await this.modalController.create({
      component:ModalPopupPageRet,
      cssClass:"modalTest",
      componentProps: {
        'model_title':reteta.nume_reteta,
       'tipReteta': reteta.nume_Tip_Retete,
       'categorieReteta': reteta.nume_Categorie_Retete,
       'rating':reteta.rating,

       'user_rating': reteta.user_rating,
        'instructiuni':reteta.instructiuni ,
        'pahar':reteta.nume_pahar ,
        'poza':reteta.poza_reteta,
        'raiting': reteta.rating_retea,
        'ingrediente':reteta.retetaIngredient,
        ...options
      }
    })


    modal.present()


    const {data} = await modal.onWillDismiss();
    //console.log("modal returned data", data)

  }

 async openIngredientModal(subcategorie){
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


    const {data} = await modal.onWillDismiss();
    console.log("modal returned!", data);
  }
}
