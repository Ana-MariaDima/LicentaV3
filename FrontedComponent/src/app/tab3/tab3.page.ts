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
  async PreopenCardModal(param){
    var reteta = await this.reteteService.getRetetaRandom()
    this.modalService.openRetetaModal(reteta,{canReload:true});
    console.log(this.modalService.getCart());
  }


}
