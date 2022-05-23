import { Component } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ModalServiceService } from '../modal-service.service';
import { ReteteService } from '../services/Retete/retete.service';
import { ModalPopupPageRet } from '../modal-popup-ret/modal-popup-ret.page';
@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss']
})
export class Tab2Page {


  constructor(private modalService:ModalServiceService,private ReteteService: ReteteService,private modalController: ModalController) { }
  retete:Array<any> = [];
  reteteliked:Array<any> = [];



  async ngOnInit() {

    //await this.fetchReteteApreciate();

  }

  async fetchReteteApreciate(){
    this.retete = [];
    this.reteteliked = (await this.ReteteService.getLikedRetete()as Array<any>);
    console.log("fetchReteteApreciate ",)
    this.reteteliked.forEach(reteta => {
        (this.ReteteService.getReteta(reteta.idReteta)).then(async (reteta)=>{
          reteta[0].liked = true;
          console.log(reteta);
          //reteta[0].user_rating = await this.ReteteService.getReviewReteta(reteta[0].nume_reteta);
          this.retete.push(reteta[0])
      });
    });

    //console.log(this.retete)

  }
  async ionViewWillEnter(){
    await this.fetchReteteApreciate();


  }


    async PreopenCardModal(param){
     // var reteta = await this.ReteteService.getRetetaRandom()
      //console.log("reteta before modal open", reteta)
      this.modalService.openRetetaModal(param,{canReload:false});
     // console.log(this.modalService.getCart());
    }
   async toggleLiked(reteta){
      reteta.liked = !reteta.liked;
      await this.ReteteService.toggleLike(reteta.nume_reteta);

      if(reteta.liked ==true)
      reteta.nr_likes=reteta.nr_likes+1;
      else{
        await this.fetchReteteApreciate();
        (window as any).EventSystem.trigger("retetaUnliked", reteta);

        reteta.nr_likes=reteta.nr_likes-1;
      }
    }





    async openRetetaModal(reteta){
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
      if(data.likedState != reteta.liked ){
        await this.fetchReteteApreciate();
        //this.toggleLiked(reteta);
        console.log("Emitting Event");
        (window as any).EventSystem.trigger("retetaUnliked", reteta);
      }

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
        (window as any).EventSystem.trigger("retetaReviewed>>reteta="+reteta.nume_reteta, reteta);

      }
    }

    async submitReview(reteta, review){
      var nouaMedie = await this.ReteteService.submitReview(reteta, review);

      return nouaMedie
    }


}
