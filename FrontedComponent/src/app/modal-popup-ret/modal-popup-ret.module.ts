import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModalPopupPageRoutingModule } from './modal-popup-ret-routing.module';

import { ModalPopupPageRet } from './modal-popup-ret.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModalPopupPageRoutingModule
  ],
  declarations: [ModalPopupPageRet]
})
export class ModalPopupPageRetModule {}
