import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { ModalPopupPageRoutingModule } from './modal-popup-routing.module';

import { ModalPopupPage } from './modal-popup.page';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { Tab1Page } from '../tab1/tab1.page';
import { Tab1PageModule } from '../tab1/tab1.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    ModalPopupPageRoutingModule,
    Ng2SearchPipeModule,
    Tab1PageModule
  ],
  declarations: [ModalPopupPage]
})
export class ModalPopupPageModule {}
