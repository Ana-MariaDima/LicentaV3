import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
  perfectMatch:any = undefined;
  showChecks = true;
  forYou = false;



  constructor(private route: ActivatedRoute, private modalController: ModalController, private reteteService: ReteteService, private modalService:ModalServiceService) {
      //[queryParams]="{property:value}"
      this.route.queryParams.subscribe(async params=>{
        this.setDataSource(params);
      });


   }

  async setDataSource(params){
    switch(params['dataSource']){
      case 'Pahar':{
        this.retete=[];
        this.perfectMatch = params['perfectMatch'];
        var arrayIngrediente = localStorage.getItem('cart');
        console.log(arrayIngrediente);
        if(arrayIngrediente){
          arrayIngrediente = JSON.parse(arrayIngrediente);
          this.retete = await this.reteteService.getReteteSugerate(Object.keys(arrayIngrediente), this.perfectMatch == 'false'?false: true);
          console.log("retete sugerate", this.retete)
        }
      }break;

      case 'AltoraLePlace':{
        this.retete=[];
        this.showChecks = false;
        var reteteId = await this.reteteService.getRecommandations();
        console.log("Reteteid",reteteId);
        this.retete = [];
        for(let index in reteteId){
          var reteta = await this.reteteService.getReteta(reteteId[index]);
          this.retete.push(reteta[0]);
        }
      }break;

      case 'PentruTine':{
        this.showChecks = false;
        var arrayIngrediente = '';
        this.retete=[];
        var reteteApreciate = await this.reteteService.getLikedRetete();
        console.log(reteteApreciate);
        var frecventaIngrediente = {};

        for(let x in reteteApreciate){
          var retetaFull = (await this.reteteService.getReteta(reteteApreciate[x].idReteta))[0];
          console.log(retetaFull);
          retetaFull.retetaIngredient.forEach(ing=>{
            frecventaIngrediente[ing.idIngredient] = (frecventaIngrediente[ing.idIngredient]?frecventaIngrediente[ing.idIngredient]:0)+1;
          });
        }

        var ingrediente = Object.keys(frecventaIngrediente);
        ingrediente.sort((a,b)=>{
            return frecventaIngrediente[b] - frecventaIngrediente[a];
        });

        ingrediente = ingrediente.slice(0, 3)
        console.log(ingrediente, frecventaIngrediente);

        if(ingrediente.length > 0){
          this.retete = await this.reteteService.getReteteSugerate(ingrediente, false);
          console.log("retete sugerate", this.retete)

         // this.retete=  this.retete.filter(item =>item.includes(reteteApreciate) );
            //reteteApreciate.find(test => test.idReteta !== item.id));

           // this.retete= this.retete.filter(a => reteteApreciate.some(b => b.idReteta === a.id));
          this.retete = this.retete.filter(item => !reteteApreciate.map(x=>x.idReteta).includes(item.id) );

          // this.retete=this.retete.
          console.log("retete sugerate", this.retete)
        }else{
          this.retete = [];
          this.forYou=true;

        }
      }break;
    }
  }

  async ngOnInit() {

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
