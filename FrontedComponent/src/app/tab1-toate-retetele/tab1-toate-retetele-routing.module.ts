import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { Tab1ToateRetetelePage } from './tab1-toate-retetele.page';

const routes: Routes = [
  {
    path: '',
    component: Tab1ToateRetetelePage
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class Tab1ToateRetetelePageRoutingModule {}
