import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ModalPopupPageRet } from '../modal-popup-ret/modal-popup-ret.page';
import { ModalServiceService } from '../modal-service.service';
import { ReteteService } from '../services/Retete/retete.service';

@Component({
  selector: 'app-sugestii',
  templateUrl: './sugestii.page.html',
  styleUrls: ['./sugestii.page.scss'],
})
export class SugestiiPage implements OnInit {
  retete:any[];
  itemsInCart;
  searchTerm:any;
  constructor(private modalController: ModalController, private reteteService: ReteteService, private modalService:ModalServiceService) { }

  async ngOnInit() {
      var arrayIngrediente = localStorage.getItem('cart');
      if(arrayIngrediente){
        arrayIngrediente = JSON.parse(arrayIngrediente);
        this.retete = await this.reteteService.getReteteSugerate(Object.keys(arrayIngrediente));
      }
  }

  ionViewWillEnter(){

    var items = JSON.parse(localStorage.getItem('cart'))
    if(items){
    this.itemsInCart = Object.values(items).length;
    console.log(this.itemsInCart)
    }
  }

  async openCart(){
    console.log("open Cart")
    await this.modalService.openCartModal()
    this.ionViewWillEnter();
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

  }


  toggleLiked(reteta) {
    reteta.liked = !reteta.liked;
    this.reteteService.toggleLike(reteta.nume_reteta);
  }

}
