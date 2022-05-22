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
  theItems;
  searchTerm:any;
  constructor(private modalController: ModalController, private reteteService: ReteteService, private modalService:ModalServiceService) { }

  async ngOnInit() {
      var arrayIngrediente = localStorage.getItem('cart');

      console.log ('ingrediente',arrayIngrediente)
      if(arrayIngrediente){
        arrayIngrediente = JSON.parse(arrayIngrediente);
        this.retete = await this.reteteService.getReteteSugerate(Object.keys(arrayIngrediente));
        console.log("retete sugerate", this.retete)
      }
  }

  isInCart(ing){
    return !!this.theItems.find(ingInCart => ingInCart.nume_ingredient == ing.nume_ingredient);
  }

  ionViewWillEnter(){

    var items = JSON.parse(localStorage.getItem('cart'))
    if(items){
    this.itemsInCart = Object.values(items).length;
    this.theItems =Object.values(items)
    //console.log(items)
    }
  }

  async openCart(){
    console.log("open Cart")

    await this.modalService.openCartModal()
    this.modalController.dismiss()
    this.ionViewWillEnter();
  }

  async dismiss() {
    //const close: string = "Modal Removed";
     this.modalController.dismiss();
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
        'rating': reteta.rating,
        'ingrediente':reteta.retetaIngredient,
        'liked':  reteta.liked,
        'categorieReteta': reteta.nume_Categorie_Retete,
        'nr_likes': reteta.nr_likes,
        'user_rating':reteta.user_rating
      }
    })


    modal.present()
    var {data} = await modal.onWillDismiss();
    data = JSON.parse(data.payload);
    if(data.nr_likes!= reteta.nr_likes ){
      this.toggleLiked(reteta);
    }

    console.log("Modal closed !", data);
    //if(data.user_rating != reteta.user_rating)
    reteta.user_rating = data.user_rating;
    reteta.rating = data.rating;

    var idx = this.retete.findIndex(x=>{
      x.nume_reteta == reteta.nume_reteta
    });


  }

  onReview(reteta){

    return async (review)=>{
      var rezultat =  await this.submitReview(reteta.nume_reteta, review);
      reteta.user_rating = review;
      reteta.rating = rezultat.medie;
    }
  }

  async submitReview(reteta, review){
    var nouaMedie = await this.reteteService.submitReview(reteta, review);

    return nouaMedie
  }

  toggleLiked(reteta) {
    reteta.liked = !reteta.liked;
    if(reteta.liked ==true)
    reteta.nr_likes=reteta.nr_likes+1;
    else
    reteta.nr_likes=reteta.nr_likes-1;

    this.reteteService.toggleLike(reteta.nume_reteta);
  }

}
