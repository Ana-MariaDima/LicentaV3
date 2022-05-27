import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ModalPopupIngAll } from './modal-popup-all.page';

const routes: Routes = [
  {
    path: '',
    component: ModalPopupIngAll
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ModalPopupPageRoutingModule {}
