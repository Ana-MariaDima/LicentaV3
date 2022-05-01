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
  @Input() raiting: any;
  @Input() liked: any;
  @Input() canReload: boolean;

  itemsInCart: Object[]=[];


  constructor(private modalController: ModalController, private  modalService: ModalServiceService, private reteteService: ReteteService) {}
  async PreopenCardModal(param){
    var reteta = await this.reteteService.getRetetaRandom()
    this.modalService.openRetetaModal(reteta, {canReload:this.canReload});
    console.log(this.modalService.getCart());
  }




  ngOnInit() {


  }
  onRateChange(event) {
    console.log('Your rate:', event);
  }

  async dismiss() {
    const close: string = "Modal Removed";
    return this.modalController.dismiss({customProp:"123"});


  }
  toggleLiked() {
    this.liked = !this.liked;
    this.reteteService.toggleLike(this.model_title);

  }
}
