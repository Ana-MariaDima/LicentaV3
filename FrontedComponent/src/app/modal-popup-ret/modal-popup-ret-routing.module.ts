import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModalPopupPageRet } from './modal-popup-ret.page';
import { IonicRatingModule } from 'ionic4-rating';

const routes: Routes = [
  {
    path: '',
    component: ModalPopupPageRet
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes),IonicRatingModule],
  exports: [RouterModule],
})
export class ModalPopupPageRoutingModule {}
