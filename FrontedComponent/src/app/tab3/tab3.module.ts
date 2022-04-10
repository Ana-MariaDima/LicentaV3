import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Tab3Page } from './tab3.page';
import { ExploreContainerComponentModule } from '../explore-container/explore-container.module';

import { Tab3PageRoutingModule } from './tab3-routing.module';
import { Tab1ToateRetetelePage } from '../tab1-toate-retetele/tab1-toate-retetele.page';
import { Tab1ToateRetetelePageModule } from '../tab1-toate-retetele/tab1-toate-retetele.module';
import { ModalServiceService } from '../modal-service.service';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ExploreContainerComponentModule,
    RouterModule.forChild([{ path: '', component: Tab3Page }]),
    Tab3PageRoutingModule,
    Tab1ToateRetetelePageModule,

  ],
  providers:[ModalServiceService],
  declarations: [Tab3Page]
})
export class Tab3PageModule {}
