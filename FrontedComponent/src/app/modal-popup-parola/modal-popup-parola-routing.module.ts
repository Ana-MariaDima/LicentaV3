import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModalPopupParolaPage } from './modal-popup-parola.page';

const routes: Routes = [
  {
    path: '',
    component: ModalPopupParolaPage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModalPopupPageRoutingModule {}
