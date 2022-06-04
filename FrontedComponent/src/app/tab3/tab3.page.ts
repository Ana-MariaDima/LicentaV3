import { Component } from '@angular/core';
import { ModalServiceService } from '../modal-service.service';
import { ReteteService } from '../services/Retete/retete.service';
import { Tab1ToateRetetelePageModule } from '../tab1-toate-retetele/tab1-toate-retetele.module';
import { Tab1ToateRetetelePage } from '../tab1-toate-retetele/tab1-toate-retetele.page';

@Component({
  selector: 'app-tab3',
  templateUrl: 'tab3.page.html',
  styleUrls: ['tab3.page.scss']
})
export class Tab3Page {

  constructor(private modalService:ModalServiceService, private reteteService: ReteteService ) {}
  async PreopenCardModal(){
    var reteta = await this.reteteService.getRetetaRandom();
     //var categorieReteta=await this.reteteService.getCategorieReteta(reteta.nume_reteta);
    console.log("reteta before modal open", reteta)
    this.modalService.openRetetaModal(reteta,{canReload:true});
   // console.log(this.modalService.getCart());
  }

  async PreopenCardModal_RetetaZilei(){
    var reteta = await this.reteteService.getRetetaZilei()
    console.log("reteta before modal open", reteta)
    this.modalService.openRetetaModal(reteta,{canReload:false});
   // console.log(this.modalService.getCart());
  }



}
