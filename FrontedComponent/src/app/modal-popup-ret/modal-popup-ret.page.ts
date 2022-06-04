import { Component, OnInit, Input } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ModalServiceService } from '../modal-service.service';
import { ReteteService } from '../services/Retete/retete.service';


@Component({
  selector: 'app-modal-popup',
  templateUrl: './modal-popup-ret.page.html',
  styleUrls: ['./modal-popup-ret.page.scss'],
})
export class ModalPopupPageRet implements OnInit {
  @Input() model_title: string;
  @Input() tipReteta: any;
  @Input() categorieReteta: any;
  @Input() ingrediente: any[];
  @Input() instructiuni: any;
  @Input() pahar: any;
  @Input() poza:any;
  @Input() rating: any;
  @Input() liked: any;
  @Input() canReload: boolean;
  @Input() nr_likes: any;
  @Input() user_rating: any;



  itemsInCart: Object[]=[];
   instructiuniSplit:any[];


  constructor(private modalController: ModalController, private  modalService: ModalServiceService, private reteteService: ReteteService) {}
  async PreopenCardModal(param){
    var reteta = await this.reteteService.getRetetaRandom();
    console.log("retetaRandom",reteta)
    this.modalService.openRetetaModal(reteta, {canReload:this.canReload});
    console.log(this.modalService.getCart());
  }




  ngOnInit() {
     this.instructiuniSplit= this.instructiuni.split(".").filter (i=> i!="");
    //console.log(instructiuniSplit)

  }
  onRateChange(event) {
    console.log('Your rate:', event);
  }

  async dismiss() {
    const close: string = "Modal Removed";
    console.log("Modal dismissed", this.nr_likes)
    var payload = JSON.stringify({likedState:this.liked, nr_likes:this.nr_likes, user_rating:this.user_rating, rating:this.rating});
    return this.modalController.dismiss({payload: payload}); //{likedState:this.liked, nr_likes:this.nr_likes, user_rating:this.user_rating, rating:this.rating}
  }
  toggleLiked() {
    this.liked = !this.liked;


    if(this.liked ==true)
    this.nr_likes=this.nr_likes+1;
    else
    this.nr_likes=this.nr_likes-1;

    this.reteteService.toggleLike(this.model_title);

  }

  // onReview(){

  //   return async (review)=>{
  //     var rezultat =  await this.submitReview(this.model_title, review);
  //     this.rating = rezultat.medie;
  //     this.user_rating = review;
  //   }
  // }

  // async submitReview(reteta, review){
  //   var nouaMedie = await this.reteteService.submitReview(reteta, review);

  //   return nouaMedie
  // }

}
