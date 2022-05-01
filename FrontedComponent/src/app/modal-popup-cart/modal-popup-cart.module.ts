import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModalPopupCartPageRoutingModule } from './modal-popup-cart-routing.module';

import { ModalPopupCartPage } from './modal-popup-cart.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModalPopupCartPageRoutingModule
  ],
  declarations: [ModalPopupCartPage]
})
export class ModalPopupCartPageModule {}
