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

    this.reteteliked = (await this.ReteteService.getLikedRetete()as Array<any>);
    console.log(this.reteteliked);


  this.reteteliked.forEach(reteta => {
    (this.ReteteService.getReteta(reteta.idReteta)).then((reteta)=>{

      reteta[0].liked = true;
      this.retete.push(reteta[0])
     // console.log("Retete", this.retete)

  });
});


    }


    async PreopenCardModal(param){
     // var reteta = await this.ReteteService.getRetetaRandom()
      //console.log("reteta before modal open", reteta)
      this.modalService.openRetetaModal(param,{canReload:false});
     // console.log(this.modalService.getCart());
    }

    async openRetetaModal(reteta){


      // if (reteta==null)
      // {
      //   var nr= this.retete.length;
      //   var randNumber = Math.random() * nr;
      //   reteta=this.retete[randNumber];
      // }

      //console.log("reteta from modal ",reteta);
      //console.log("reteta from modal +ing ",reteta.retetaIngredient);


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
          'liked':  reteta.liked
        }
      })

//console.log(reteta.liked)
      modal.present()
      const {data} = await modal.onWillDismiss();
     // console.log("modal returned data", data)

    }

}
