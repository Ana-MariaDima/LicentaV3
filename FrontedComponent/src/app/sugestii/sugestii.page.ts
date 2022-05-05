import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ModalPopupPageRet } from '../modal-popup-ret/modal-popup-ret.page';

@Component({
  selector: 'app-sugestii',
  templateUrl: './sugestii.page.html',
  styleUrls: ['./sugestii.page.scss'],
})
export class SugestiiPage implements OnInit {
  retete:[]
  searchTerm:any;
  constructor(private modalController: ModalController) { }

  ngOnInit() {
  }



  async openRetetaModal(reteta){
    console.log("Reteta deschisa", reteta)


    var modal = await this.modalController.create({
      component:ModalPopupPageRet,
      cssClass:"modalTest",
      componentProps: {
        'model_title':reteta.nume_reteta,
       'tipReteta': reteta.nume_Tip_Retete,
        'instructiuni':reteta.instructiuni ,
        'pahar':reteta.nume_pahar ,
        'poza':reteta.poza_reteta,
        'raiting': reteta.rating_retea,
        'ingrediente':reteta.retetaIngredient,
        'liked':  reteta.liked,
        'categorieReteta': reteta.nume_Categorie_Retete
      }
    })


    modal.present()
    const {data} = await modal.onWillDismiss();
   // console.log("modal returned data", data)

  }

}
