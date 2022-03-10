import { Component, OnInit, Input } from '@angular/core';
import { ModalController } from '@ionic/angular';
@Component({
  selector: 'app-modal-popup',
  templateUrl: './modal-popup.page.html',
  styleUrls: ['./modal-popup.page.scss'],
})
export class ModalPopupPage implements OnInit {
  @Input() model_title: string;
  @Input() subcategorie: any;
  @Input() ingrediente: any;

  constructor(
    private modalController: ModalController,
  ) { }
  ngOnInit() {

    console.log(this.ingrediente,"from modal")
  }

  async dismiss() {
    const close: string = "Modal Removed";
    return this.modalController.dismiss({customProp:"123"});


  }
}
