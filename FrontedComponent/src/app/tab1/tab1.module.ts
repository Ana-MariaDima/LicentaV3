import { IonicModule } from '@ionic/angular';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Tab1Page } from './tab1.page';
import { ExploreContainerComponentModule } from '../explore-container/explore-container.module';

import { Tab1PageRoutingModule } from './tab1-routing.module';
import { CategoryService } from '../services/Ingrediente/category.service';
import { Ng2SearchPipeModule } from 'ng2-search-filter';

@NgModule({
  imports: [
    IonicModule,
    CommonModule,
    FormsModule,
    ExploreContainerComponentModule,
    Tab1PageRoutingModule,
    Ng2SearchPipeModule
  ],
  providers:[CategoryService],
  declarations: [Tab1Page]
})
export class Tab1PageModule {}
