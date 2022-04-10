import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { environment } from 'src/environments/environment';
import { ModalPopupPage } from '../modal-popup-ing/modal-popup.page';
import { ModalPopupPageRet } from '../modal-popup-ret/modal-popup-ret.page';
import { ReteteService } from '../services/Retete/retete.service';

@Component({
  selector: 'app-tab1-toate-retetele',
  templateUrl: './tab1-toate-retetele.page.html',
  styleUrls: ['./tab1-toate-retetele.page.scss'],
})
export class Tab1ToateRetetelePage  {

  constructor(private ReteteService: ReteteService,private modalController: ModalController) { }
  retete:Array<any> = []
  tip:Array<any> = []
  currentPage:number = 1
  recordsPerPage:number = 30;
  hasReachedEnd:boolean = false;
  async ngOnInit() {
    this.currentPage = 1;
    this.retete = [];
    environment.baseUrl
    this.retete = (await this.ReteteService.getRetete(this.currentPage, this.recordsPerPage));
    //console.log(this.retete)

    this.hasReachedEnd = this.retete.length == 0;
  }

  async openCardModal(reteta){
     //await Tab1ToateRetetelePage.openRetetaModal(reteta);
  }

  async openRetetaModal(reteta){


    // if (reteta==null)
    // {
    //   var nr= this.retete.length;
    //   var randNumber = Math.random() * nr;
    //   reteta=this.retete[randNumber];
    // }

    console.log("reteta from modal ",reteta);
    console.log("reteta from modal +ing ",reteta.retetaIngredient);


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


    modal.present()
    const {data} = await modal.onWillDismiss();
    console.log("modal returned data", data)

  }

  async lazyLoadRetete(){
      if(this.hasReachedEnd) return;

      this.currentPage+=1;
      var noiRetete = (await this.ReteteService.getRetete(this.currentPage, this.recordsPerPage));

      if(noiRetete.length > 0){
        this.retete = this.retete.concat(noiRetete)
      }else{
        this.hasReachedEnd = true;
      }
  }
}

